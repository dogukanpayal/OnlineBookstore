import React from 'react';
import styles from './FilterSortBar.module.css';

const FilterSortBar = ({ 
  filterAuthor, 
  setFilterAuthor, 
  sortField, 
  setSortField, 
  sortOrder, 
  setSortOrder 
}) => {
  return (
    <div className={styles.filterSortBar} role="toolbar" aria-label="Book filtering and sorting controls">
      <div className={styles.filterGroup}>
        <label htmlFor="authorFilter" className={styles.label}>
          Yazar Filtresi:
        </label>
        <input
          id="authorFilter"
          type="text"
          value={filterAuthor}
          onChange={(e) => setFilterAuthor(e.target.value)}
          placeholder="Yazar adı ile filtrele..."
          className={styles.input}
          aria-describedby="author-filter-help"
        />
        <span id="author-filter-help" className="sr-only">
          Type an author name to filter books by that author
        </span>
      </div>

      <div className={styles.filterGroup}>
        <label htmlFor="sortField" className={styles.label}>
          Sıralama Alanı:
        </label>
        <select
          id="sortField"
          value={sortField}
          onChange={(e) => setSortField(e.target.value)}
          className={styles.select}
          aria-label="Select field to sort books by"
        >
          <option value="title">Başlık</option>
          <option value="author">Yazar</option>
          <option value="publish_date">Yayın Tarihi</option>
        </select>
      </div>

      <div className={styles.filterGroup}>
        <fieldset className={styles.sortOrderFieldset}>
          <legend className={styles.label}>
            Sıralama Yönü:
          </legend>
          <div className={styles.toggleContainer} role="radiogroup" aria-label="Sort order">
            <button
              type="button"
              onClick={() => setSortOrder('asc')}
              className={`${styles.toggleButton} ${sortOrder === 'asc' ? styles.active : ''}`}
              aria-pressed={sortOrder === 'asc'}
              aria-label="Sort ascending (A to Z)"
            >
              A-Z
            </button>
            <button
              type="button"
              onClick={() => setSortOrder('desc')}
              className={`${styles.toggleButton} ${sortOrder === 'desc' ? styles.active : ''}`}
              aria-pressed={sortOrder === 'desc'}
              aria-label="Sort descending (Z to A)"
            >
              Z-A
            </button>
          </div>
        </fieldset>
      </div>
    </div>
  );
};

export default FilterSortBar; 