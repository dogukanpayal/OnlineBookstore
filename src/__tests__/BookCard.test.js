import React from 'react';
import { render, screen } from '@testing-library/react';
import { BrowserRouter } from 'react-router-dom';
import { CartProvider } from '../context/CartContext';
import BookCard from '../components/BookCard';

const mockProps = {
  title: 'Test Book',
  author: 'Test Author',
  coverId: 12345,
  price: 25.99,
  keyProp: 'test-key'
};

// Mock the CartContext
jest.mock('../context/CartContext', () => ({
  useCart: () => ({
    addToCart: jest.fn()
  }),
  CartProvider: ({ children }) => <div data-testid="cart-provider">{children}</div>
}));

const renderWithProviders = (component) => {
  return render(
    <BrowserRouter>
      <CartProvider>
        {component}
      </CartProvider>
    </BrowserRouter>
  );
};

describe('BookCard', () => {
  test('renders book title', () => {
    renderWithProviders(<BookCard {...mockProps} />);
    expect(screen.getByText('Test Book')).toBeInTheDocument();
  });

  test('renders author name', () => {
    renderWithProviders(<BookCard {...mockProps} />);
    expect(screen.getByText('by Test Author')).toBeInTheDocument();
  });

  test('renders price', () => {
    renderWithProviders(<BookCard {...mockProps} />);
    expect(screen.getByText('₺25.99')).toBeInTheDocument();
  });

  test('renders book image with correct src', () => {
    renderWithProviders(<BookCard {...mockProps} />);
    const image = screen.getByAltText('Cover for Test Book by Test Author');
    expect(image).toHaveAttribute('src', 'https://covers.openlibrary.org/b/id/12345-M.jpg');
  });

  test('renders fallback image when no coverId', () => {
    const propsWithoutCover = { ...mockProps, coverId: null };
    renderWithProviders(<BookCard {...propsWithoutCover} />);
    const image = screen.getByAltText('Cover for Test Book by Test Author');
    expect(image).toHaveAttribute('src', 'https://via.placeholder.com/150x200?text=No+Cover');
  });

  test('renders Add to Cart button', () => {
    renderWithProviders(<BookCard {...mockProps} />);
    expect(screen.getByTestId('add-to-cart-button')).toBeInTheDocument();
  });
}); 