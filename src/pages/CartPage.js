import React from 'react';
import { useCart } from '../context/CartContext';
import styles from './CartPage.module.css';

const CartPage = () => {
  const { cartItems, removeFromCart, clearCart } = useCart();

  const handleClearCart = () => {
    if (window.confirm('Sepeti temizlemek istediğinizden emin misiniz?')) {
      clearCart();
    }
  };

  const handleCheckout = () => {
    alert('Siparişiniz alındı!');
  };

  // Compute total price using item.price
  const totalPrice = cartItems.reduce((total, item) => {
    return total + (item.price * item.quantity);
  }, 0);

  if (cartItems.length === 0) {
    return (
      <div className={styles.cartContainer}>
        <h1 className={styles.cartTitle}>Sepetim</h1>
        <div className={styles.emptyCart}>
          <p>Sepetiniz boş.</p>
          <p>Kitap eklemek için ana sayfaya dönün.</p>
        </div>
      </div>
    );
  }

  return (
    <div className={styles.cartContainer}>
      <div className={styles.cartHeader}>
        <h1 className={styles.cartTitle}>Sepetim</h1>
        <button 
          onClick={handleClearCart}
          className={styles.clearCartButton}
        >
          Sepeti Temizle
        </button>
      </div>
      
      <div className={styles.cartItems}>
        {cartItems.map((item) => (
          <div key={item.id} className={styles.cartItem}>
            <div className={styles.itemImage}>
              <img 
                src={item.coverId 
                  ? `https://covers.openlibrary.org/b/id/${item.coverId}-M.jpg`
                  : 'https://via.placeholder.com/80x120?text=No+Cover'
                }
                alt={item.title}
                onError={(e) => {
                  e.target.src = 'https://via.placeholder.com/80x120?text=No+Image';
                }}
              />
            </div>
            
            <div className={styles.itemDetails}>
              <h3 className={styles.itemTitle}>{item.title}</h3>
              <p className={styles.itemAuthor}>by {item.author}</p>
              <p className={styles.itemQuantity}>Adet: {item.quantity}</p>
            </div>
            
            <div className={styles.itemPrice}>
              ₺{item.price * item.quantity}
            </div>
            
            <button 
              onClick={() => removeFromCart(item.id)}
              className={styles.removeButton}
            >
              Kaldır
            </button>
          </div>
        ))}
      </div>
      
      <div className={styles.cartSummary}>
        <div className={styles.totalRow}>
          <span className={styles.totalLabel}>Toplam:</span>
          <span className={styles.totalPrice}>
            ₺{totalPrice}
          </span>
        </div>
        
        <button 
          onClick={handleCheckout}
          className={styles.checkoutButton}
        >
          Ödeme Yap
        </button>
      </div>
    </div>
  );
};

export default CartPage; 