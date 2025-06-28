import { useState, useEffect, useCallback } from 'react';

const useBooks = (searchQuery, page = 1) => {
  const [books, setBooks] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [hasMore, setHasMore] = useState(false);
  const [currentPage, setCurrentPage] = useState(page);

  useEffect(() => {
    // Don't fetch if search query is empty
    if (!searchQuery || searchQuery.trim() === '') {
      setBooks([]);
      setLoading(false);
      setError(null);
      setHasMore(false);
      setCurrentPage(1);
      return;
    }

    const fetchBooks = async () => {
      setLoading(true);
      setError(null);
      
      try {
        const response = await fetch(
          `https://openlibrary.org/search.json?q=${encodeURIComponent(searchQuery)}&page=${currentPage}`
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
        if (currentPage === 1) {
          setBooks(mappedBooks);
        } else {
          setBooks(prevBooks => [...prevBooks, ...mappedBooks]);
        }
        
        // Check if more results exist
        setHasMore(data.docs.length > 0);
      } catch (err) {
        setError(err.message);
        setBooks([]);
      } finally {
        setLoading(false);
      }
    };

    fetchBooks();
  }, [searchQuery, currentPage]);

  const loadMore = useCallback(() => {
    setCurrentPage(prevPage => prevPage + 1);
  }, []);

  return { 
    books, 
    loading, 
    error, 
    loadMore, 
    hasMore 
  };
};

export default useBooks; 