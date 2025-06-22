import React from 'react';
import { useCart } from '../contexts/CartContext';
import styles from './BookCard.module.css';

const BookCard = ({ title, author, coverId }) => {
  const { addToCart } = useCart();
  
  // Build image URL using OpenLibrary cover API or fallback
  const imageUrl = coverId 
    ? `https://covers.openlibrary.org/b/id/${coverId}-M.jpg`
    : 'https://via.placeholder.com/150x200?text=No+Cover';

  const handleAddToCart = () => {
    addToCart({
      id: coverId || `book-${Date.now()}`, // Use coverId as id, or generate unique id
      title,
      author,
      coverId
    });
  };

  return (
    <div className={styles.bookCard}>
      <div className={styles.bookImageContainer}>
        <img 
          src={imageUrl} 
          alt={title} 
          className={styles.bookImage}
          onError={(e) => {
            e.target.src = 'https://via.placeholder.com/150x200?text=No+Image';
          }}
        />
      </div>
      <div className={styles.bookInfo}>
        <h3 className={styles.bookTitle}>{title}</h3>
        <p className={styles.bookAuthor}>by {author}</p>
        <button 
          className={styles.addToCartButton}
          onClick={handleAddToCart}
        >
          Add to Cart
        </button>
      </div>
    </div>
  );
};

export default BookCard; 