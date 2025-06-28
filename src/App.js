import React from 'react';
import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext';
import { CartProvider } from './context/CartContext';
import { useCart } from './context/CartContext';
import { useAuth } from './context/AuthContext';
import ProtectedRoute from './components/ProtectedRoute';
import HomePage from './components/HomePage';
import BookDetail from './components/BookDetail';
import CartPage from './components/CartPage';
import LoginPage from './pages/LoginPage';
import CheckoutPage from './pages/CheckoutPage';
import ConfirmationPage from './pages/ConfirmationPage';
import './App.css';

const Header = () => {
  const { cartItems } = useCart();
  const { currentUser, logout } = useAuth();
  
  const cartCount = cartItems.reduce((total, item) => total + item.quantity, 0);

  return (
    <header className="App-header">
      <div className="container">
        <div className="header-content">
          <div className="header-left">
            <h1>Online Kitap Magazasi</h1>
            <p>Your Online Bookstore</p>
          </div>
          <nav className="header-nav">
            <Link to="/" className="nav-link">Ana Sayfa</Link>
            {currentUser ? (
              <>
                <Link to="/cart" className="nav-link cart-link" data-testid="cart-link">
                  Sepet ({cartCount})
                  <span className="cart-count" data-testid="cart-count">{cartCount}</span>
                </Link>
                <button onClick={logout} className="nav-button">Çıkış</button>
              </>
            ) : (
              <Link to="/login" className="nav-link">Giriş</Link>
            )}
          </nav>
        </div>
      </div>
    </header>
  );
};

function App() {
  return (
    <AuthProvider>
      <CartProvider>
        <BrowserRouter>
          <div className="App">
            <Header />
            
            <Routes>
              <Route path="/" element={<HomePage />} />
              <Route path="/login" element={<LoginPage />} />
              <Route path="/checkout" element={<CheckoutPage />} />
              <Route path="/confirmation" element={<ConfirmationPage />} />
              <Route 
                path="/cart" 
                element={
                  <ProtectedRoute>
                    <CartPage />
                  </ProtectedRoute>
                } 
              />
              <Route 
                path="/book/:id" 
                element={
                  <ProtectedRoute>
                    <BookDetail />
                  </ProtectedRoute>
                } 
              />
            </Routes>
            
            <footer className="App-footer">
              <div className="container">
                <p>&copy; 2024 Online Kitap Magazasi. All rights reserved.</p>
              </div>
            </footer>
          </div>
        </BrowserRouter>
      </CartProvider>
    </AuthProvider>
  );
}

export default App; 