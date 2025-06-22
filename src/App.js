import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import HomePage from './components/HomePage';
import BookDetail from './components/BookDetail';
import CartPage from './components/CartPage';
import { CartProvider } from './contexts/CartContext';
import './App.css';

function App() {
  return (
    <CartProvider>
      <BrowserRouter>
        <div className="App">
          <header className="App-header">
            <div className="container">
              <h1>Online Kitap Magazasi</h1>
              <p>Your Online Bookstore</p>
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
    </CartProvider>
  );
}

export default App; 