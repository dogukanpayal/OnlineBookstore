import { useState, useEffect } from 'react';

const useBooks = (searchQuery, page = 1) => {
  const [books, setBooks] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [nextPageExists, setNextPageExists] = useState(false);

  useEffect(() => {
    // Don't fetch if search query is empty
    if (!searchQuery || searchQuery.trim() === '') {
      setBooks([]);
      setLoading(false);
      setError(null);
      setNextPageExists(false);
      return;
    }

    const fetchBooks = async () => {
      setLoading(true);
      setError(null);
      
      try {
        const response = await fetch(
          `https://openlibrary.org/search.json?q=${encodeURIComponent(searchQuery)}&page=${page}`
        );
        
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        
        const data = await response.json();
        
        // Parse response.docs and map to our desired format with random price
        const mappedBooks = data.docs.map(doc => ({
          key: doc.key,
          title: doc.title,
          author_name: doc.author_name ? doc.author_name[0] : 'Unknown Author',
          cover_i: doc.cover_i,
          first_publish_year: doc.first_publish_year,
          price: Math.floor(Math.random() * 50) + 10 // Random price between 10-60 TL
        }));
        
        // If it's page 1, replace books, otherwise append
        if (page === 1) {
          setBooks(mappedBooks);
        } else {
          setBooks(prevBooks => [...prevBooks, ...mappedBooks]);
        }
        
        // Check if next page exists (OpenLibrary returns 100 results per page)
        setNextPageExists(data.docs.length === 100);
      } catch (err) {
        setError(err.message);
        setBooks([]);
      } finally {
        setLoading(false);
      }
    };

    fetchBooks();
  }, [searchQuery, page]);

  return { books, loading, error, nextPageExists };
};

export default useBooks; 