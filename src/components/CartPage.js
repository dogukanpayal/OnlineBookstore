import React from 'react';
import { useCart } from '../contexts/CartContext';
import styles from './CartPage.module.css';

const CartPage = () => {
  const { cartItems, removeFromCart } = useCart();

  // Calculate total price
  const totalPrice = cartItems.reduce((total, item) => {
    const price = item.price || 15; // Default price if not set
    return total + (price * item.quantity);
  }, 0);

  const handleQuantityChange = (itemId, newQuantity) => {
    if (newQuantity <= 0) {
      removeFromCart(itemId);
    } else {
      // Update quantity logic would go here
      // For now, we'll just remove if quantity is 0
    }
  };

  const handleCheckout = () => {
    alert('Ödeme entegrasyonu henüz mevcut değil. (Payment integration not yet available)');
  };

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
      <h1 className={styles.cartTitle}>Sepetim</h1>
      
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
            </div>
            
            <div className={styles.itemQuantity}>
              <label htmlFor={`quantity-${item.id}`}>Adet:</label>
              <select 
                id={`quantity-${item.id}`}
                value={item.quantity}
                onChange={(e) => handleQuantityChange(item.id, parseInt(e.target.value))}
                className={styles.quantitySelect}
              >
                {[1, 2, 3, 4, 5, 6, 7, 8, 9, 10].map(num => (
                  <option key={num} value={num}>{num}</option>
                ))}
              </select>
            </div>
            
            <div className={styles.itemPrice}>
              ₺{(item.price || 15) * item.quantity}
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
          <span className={styles.totalPrice}>₺{totalPrice}</span>
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