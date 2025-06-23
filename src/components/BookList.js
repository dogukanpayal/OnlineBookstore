import React from 'react';
import BookCard from './BookCard';
import styles from './BookList.module.css';

const BookList = ({ books = [], nextPageExists = false, onLoadMore }) => {
  return (
    <div className={styles.bookListContainer}>
      <h2 className={styles.bookListTitle}>
        {books.length > 0 ? `Found ${books.length} Books` : 'Available Books'}
      </h2>
      {books.length === 0 && (
        <p className={styles.noBooksMessage}>
          No results found.
        </p>
      )}
      <div className={styles.bookGrid}>
        {books.map((book) => (
          <BookCard 
            key={book.key}
            title={book.title}
            author={book.author_name}
            coverId={book.cover_i}
            price={book.price}
            keyProp={book.key}
          />
        ))}
      </div>
      {nextPageExists && (
        <div className={styles.loadMoreContainer}>
          <button 
            onClick={onLoadMore}
            className={styles.loadMoreButton}
          >
            Daha Fazla Yükle
          </button>
        </div>
      )}
    </div>
  );
};

export default BookList; 