import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';

const BookDetail = () => {
  const { id: workId } = useParams();
  const [book, setBook] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchBookDetails = async () => {
      try {
        setLoading(true);
        setError(null);
        
        const response = await fetch(`https://openlibrary.org/works/${workId}.json`);
        
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        
        const data = await response.json();
        setBook(data);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    if (workId) {
      fetchBookDetails();
    }
  }, [workId]);

  if (loading) {
    return (
      <div style={{ 
        padding: '40px', 
        textAlign: 'center', 
        fontSize: '18px',
        color: '#666'
      }}>
        Yükleniyor…
      </div>
    );
  }

  if (error) {
    return (
      <div style={{ 
        padding: '40px', 
        textAlign: 'center', 
        color: '#dc3545',
        fontSize: '16px'
      }}>
        Hata: {error}
      </div>
    );
  }

  if (!book) {
    return (
      <div style={{ 
        padding: '40px', 
        textAlign: 'center', 
        fontSize: '16px',
        color: '#666'
      }}>
        Kitap bulunamadı.
      </div>
    );
  }

  return (
    <div style={{
      maxWidth: '800px',
      margin: '0 auto',
      padding: '40px 20px',
      backgroundColor: 'white',
      borderRadius: '8px',
      boxShadow: '0 2px 4px rgba(0, 0, 0, 0.1)',
      marginTop: '20px',
      marginBottom: '20px'
    }}>
      <h1 style={{
        fontSize: '2.5rem',
        fontWeight: '700',
        color: '#333',
        marginBottom: '20px',
        textAlign: 'center'
      }}>
        {book.title}
      </h1>

      {book.description && (
        <div style={{ marginBottom: '30px' }}>
          <h2 style={{
            fontSize: '1.5rem',
            fontWeight: '600',
            color: '#555',
            marginBottom: '15px'
          }}>
            Açıklama
          </h2>
          <p style={{
            fontSize: '16px',
            lineHeight: '1.6',
            color: '#666',
            textAlign: 'justify'
          }}>
            {typeof book.description === 'string' 
              ? book.description 
              : book.description.value || 'Açıklama mevcut değil.'
            }
          </p>
        </div>
      )}

      {book.first_publish_date && (
        <div style={{ marginBottom: '30px' }}>
          <h2 style={{
            fontSize: '1.5rem',
            fontWeight: '600',
            color: '#555',
            marginBottom: '15px'
          }}>
            İlk Yayın Tarihi
          </h2>
          <p style={{
            fontSize: '18px',
            color: '#007bff',
            fontWeight: '500'
          }}>
            {book.first_publish_date}
          </p>
        </div>
      )}

      {book.subjects && book.subjects.length > 0 && (
        <div>
          <h2 style={{
            fontSize: '1.5rem',
            fontWeight: '600',
            color: '#555',
            marginBottom: '15px'
          }}>
            Konular
          </h2>
          <div style={{
            display: 'flex',
            flexWrap: 'wrap',
            gap: '10px'
          }}>
            {book.subjects.map((subject, index) => (
              <span key={index} style={{
                backgroundColor: '#e9ecef',
                color: '#495057',
                padding: '8px 16px',
                borderRadius: '20px',
                fontSize: '14px',
                fontWeight: '500'
              }}>
                {subject}
              </span>
            ))}
          </div>
        </div>
      )}
    </div>
  );
};

export default BookDetail; 