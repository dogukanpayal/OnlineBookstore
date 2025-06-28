import React from 'react';
import BookCard from './BookCard';
import styles from './BookList.module.css';

const BookList = ({ 
  books = [], 
  hasMore = false, 
  onLoadMore,
  filterAuthor = '',
  sortField = 'title',
  sortOrder = 'asc'
}) => {
  // Filter books by author
  const filteredBooks = books.filter(book => {
    if (!filterAuthor) return true;
    const authors = Array.isArray(book.author_name) ? book.author_name : [];
    return authors.some(author => 
      author.toLowerCase().includes(filterAuthor.toLowerCase())
    );
  });

  // Sort books
  const sortedBooks = [...filteredBooks].sort((a, b) => {
    let aValue, bValue;

    switch (sortField) {
      case 'title':
        aValue = a.title || '';
        bValue = b.title || '';
        break;
      case 'author':
        aValue = a.author_name ? a.author_name[0] || '' : '';
        bValue = b.author_name ? b.author_name[0] || '' : '';
        break;
      case 'publish_date':
        aValue = a.first_publish_year || 0;
        bValue = b.first_publish_year || 0;
        break;
      default:
        aValue = a.title || '';
        bValue = b.title || '';
    }

    if (typeof aValue === 'string' && typeof bValue === 'string') {
      aValue = aValue.toLowerCase();
      bValue = bValue.toLowerCase();
    }

    if (sortOrder === 'asc') {
      return aValue > bValue ? 1 : -1;
    } else {
      return aValue < bValue ? 1 : -1;
    }
  });

  return (
    <div className={styles.bookListContainer}>
      <h2 className={styles.bookListTitle}>
        {sortedBooks.length > 0 ? `Found ${sortedBooks.length} Books` : 'Available Books'}
      </h2>
      {sortedBooks.length === 0 && (
        <div className={styles.noBooksMessage}>
          <div className={styles.noBooksIcon}>📚</div>
          <p>
            {filterAuthor ? 'No books found for this author.' : 'No results found.'}
          </p>
          <p className={styles.noBooksSuggestion}>
            Try adjusting your search terms or filters.
          </p>
        </div>
      )}
      <div className={styles.bookGrid}>
        {sortedBooks.map((book) => (
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
      {hasMore && (
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