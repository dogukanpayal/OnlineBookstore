import React from 'react';
import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import { useCart } from './context/CartContext';
import HomePage from './components/HomePage';
import BookDetail from './pages/BookDetail';
import CartPage from './pages/CartPage';
import './App.css';

function App() {
  const { cartItems } = useCart();

  return (
    <BrowserRouter>
      <div className="App">
        <header className="App-header">
          <div className="container">
            <div className="header-content">
              <div className="header-left">
                <h1>Online Kitap Magazasi</h1>
                <p>Your Online Bookstore</p>
              </div>
              <nav className="header-nav">
                <Link to="/" className="nav-link">Ana Sayfa</Link>
                <Link to="/cart" className="nav-link cart-link">
                  Sepet ({cartItems.length})
                </Link>
              </nav>
            </div>
          </div>
        </header>
        
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/book/:id" element={<BookDetail />} />
          <Route path="/cart" element={<CartPage />} />
        </Routes>
        
        <footer className="App-footer">
          <div className="container">
            <p>&copy; 2024 Online Kitap Magazasi. All rights reserved.</p>
          </div>
        </footer>
      </div>
    </BrowserRouter>
  );
}

export default App; 