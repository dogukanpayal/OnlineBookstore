import React from 'react';
import { Link } from 'react-router-dom';

const ConfirmationPage = () => {
  return (
    <div style={{
      maxWidth: '600px',
      margin: '100px auto',
      padding: '40px',
      backgroundColor: 'white',
      borderRadius: '8px',
      boxShadow: '0 4px 6px rgba(0, 0, 0, 0.1)',
      textAlign: 'center'
    }}>
      <div style={{
        fontSize: '64px',
        color: '#28a745',
        marginBottom: '20px'
      }}>
        ✓
      </div>
      
      <h1 style={{
        color: '#333',
        marginBottom: '20px',
        fontSize: '2rem'
      }}>
        Sipariş başarıyla alındı!
      </h1>
      
      <p style={{
        color: '#666',
        fontSize: '18px',
        lineHeight: '1.6',
        marginBottom: '30px'
      }}>
        Siparişiniz başarıyla işlendi. Sipariş detayları email adresinize gönderilecektir.
      </p>
      
      <Link
        to="/"
        style={{
          display: 'inline-block',
          padding: '12px 24px',
          backgroundColor: '#007bff',
          color: 'white',
          textDecoration: 'none',
          borderRadius: '4px',
          fontWeight: '500',
          transition: 'background-color 0.2s'
        }}
      >
        Ana Sayfaya Dön
      </Link>
    </div>
  );
};

export default ConfirmationPage; 