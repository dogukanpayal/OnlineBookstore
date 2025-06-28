import React, { useState, useEffect, useMemo } from 'react';
import debounce from 'lodash.debounce';
import styles from './SearchBar.module.css';

const SearchBar = ({ onSearch }) => {
  const [input, setInput] = useState('');

  // Create debounced search function with useMemo
  const debouncedSearch = useMemo(
    () => debounce((searchTerm) => {
      onSearch(searchTerm);
    }, 500),
    [onSearch]
  );

  // Cleanup debounced function on unmount
  useEffect(() => {
    return () => {
      debouncedSearch.cancel();
    };
  }, [debouncedSearch]);

  const handleSubmit = (e) => {
    e.preventDefault();
    const searchTerm = input.trim() || 'javascript';
    onSearch(searchTerm);
    setInput(searchTerm);
  };

  const handleChange = (e) => {
    const value = e.target.value;
    setInput(value);
    if (value.trim()) {
      debouncedSearch(value);
    }
  };

  return (
    <div className={styles.searchBarContainer}>
      <form onSubmit={handleSubmit} className={styles.searchForm} role="search">
        <label htmlFor="search-input" className="sr-only">
          Search for books
        </label>
        <input
          id="search-input"
          type="text"
          placeholder="Search for books..."
          value={input}
          onChange={handleChange}
          className={styles.searchInput}
          data-testid="search-input"
          aria-describedby="search-description"
        />
        <span id="search-description" className="sr-only">
          Press enter to search or type to see suggestions
        </span>
        <button 
          type="submit" 
          className={styles.searchButton} 
          data-testid="search-button"
          aria-label="Search books"
        >
          Search
        </button>
      </form>
    </div>
  );
};

export default SearchBar; 