import React from 'react';
import { Link } from 'react-router-dom';
import { useCart } from '../context/CartContext';
import styles from './BookCard.module.css';

const BookCard = ({ title, author, coverId, price, keyProp }) => {
  const { addToCart } = useCart();
  
  // Build image URL using OpenLibrary cover API or fallback
  const imageUrl = coverId 
    ? `https://covers.openlibrary.org/b/id/${coverId}-M.jpg`
    : 'https://via.placeholder.com/150x200?text=No+Cover';

  const handleAddToCart = (e) => {
    e.preventDefault();
    e.stopPropagation();
    addToCart({ 
      id: keyProp, 
      title, 
      author: Array.isArray(author) ? author[0] : author, 
      coverId, 
      price 
    });
  };

  const authorName = Array.isArray(author) ? author[0] : author;

  return (
    <Link to={`/book/${keyProp}`} className={styles.bookCardLink} data-testid="book-card">
      <div className={styles.bookCard}>
        <div className={styles.bookImageContainer}>
          <img 
            src={imageUrl} 
            alt={`Cover for ${title} by ${authorName}`}
            className={styles.bookImage}
            onError={(e) => {
              e.target.src = 'https://via.placeholder.com/150x200?text=No+Image';
            }}
          />
        </div>
        <div className={styles.bookInfo}>
          <h3 className={styles.bookTitle}>{title}</h3>
          <p className={styles.bookAuthor}>by {authorName}</p>
          <p className={styles.bookPrice}>₺{price}</p>
          <button 
            onClick={handleAddToCart}
            className={styles.addToCartButton}
            data-testid="add-to-cart-button"
            aria-label={`Add ${title} to cart`}
          >
            Add to Cart
          </button>
        </div>
      </div>
    </Link>
  );
};

export default BookCard; 