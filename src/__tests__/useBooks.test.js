import { renderHook, waitFor } from '@testing-library/react';
import useBooks from '../hooks/useBooks';

// Mock fetch globally
global.fetch = jest.fn();

describe('useBooks', () => {
  beforeEach(() => {
    fetch.mockClear();
  });

  test('should start with loading state', () => {
    const { result } = renderHook(() => useBooks('test'));
    
    expect(result.current.loading).toBe(true);
    expect(result.current.books).toEqual([]);
    expect(result.current.error).toBe(null);
  });

  test('should fetch books successfully', async () => {
    const mockBooks = {
      docs: [
        {
          key: '1',
          title: 'Test Book 1',
          author_name: ['Test Author 1'],
          cover_i: 12345,
          first_publish_year: 2020
        },
        {
          key: '2',
          title: 'Test Book 2',
          author_name: ['Test Author 2'],
          cover_i: 67890,
          first_publish_year: 2021
        }
      ],
      numFound: 2,
      start: 0
    };

    fetch.mockResolvedValueOnce({
      ok: true,
      json: async () => mockBooks
    });

    const { result } = renderHook(() => useBooks('test'));

    await waitFor(() => {
      expect(result.current.loading).toBe(false);
    });

    expect(result.current.books).toHaveLength(2);
    expect(result.current.books[0].title).toBe('Test Book 1');
    expect(result.current.books[0].price).toBeDefined();
    expect(result.current.error).toBe(null);
  });

  test('should handle fetch error', async () => {
    fetch.mockRejectedValueOnce(new Error('Network error'));

    const { result } = renderHook(() => useBooks('test'));

    await waitFor(() => {
      expect(result.current.loading).toBe(false);
    });

    expect(result.current.error).toBe('Network error');
    expect(result.current.books).toEqual([]);
  });

  test('should handle HTTP error response', async () => {
    fetch.mockResolvedValueOnce({
      ok: false,
      status: 404
    });

    const { result } = renderHook(() => useBooks('test'));

    await waitFor(() => {
      expect(result.current.loading).toBe(false);
    });

    expect(result.current.error).toBe('HTTP error! status: 404');
    expect(result.current.books).toEqual([]);
  });

  test('should not fetch when query is empty', () => {
    renderHook(() => useBooks(''));
    
    expect(fetch).not.toHaveBeenCalled();
  });
}); 