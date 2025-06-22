import React, { useState, useEffect, useCallback } from 'react';
import debounce from 'lodash.debounce';
import styles from './SearchBar.module.css';

const SearchBar = ({ onSearch }) => {
  const [input, setInput] = useState('');

  // Create debounced search function
  const debouncedSearch = useCallback(
    debounce((searchTerm) => {
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
    onSearch(input);
  };

  const handleChange = (e) => {
    const value = e.target.value;
    setInput(value);
    debouncedSearch(value);
  };

  return (
    <div className={styles.searchBarContainer}>
      <form onSubmit={handleSubmit} className={styles.searchForm}>
        <input
          type="text"
          placeholder="Search for books..."
          value={input}
          onChange={handleChange}
          className={styles.searchInput}
        />
        <button type="submit" className={styles.searchButton}>
          Search
        </button>
      </form>
    </div>
  );
};

export default SearchBar; 