import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import styles from './BookDetail.module.css';

const BookDetail = () => {
  const { id } = useParams();
  const [book, setBook] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchBookDetails = async () => {
      try {
        setLoading(true);
        setError(null);
        
        const response = await fetch(`https://openlibrary.org/works/${id}.json`);
        
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

    if (id) {
      fetchBookDetails();
    }
  }, [id]);

  if (loading) {
    return (
      <div className={styles.bookDetailContainer}>
        <div className={styles.loading}>
          <p>Yükleniyor…</p>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className={styles.bookDetailContainer}>
        <div className={styles.error}>
          <p>Hata: {error}</p>
        </div>
      </div>
    );
  }

  if (!book) {
    return (
      <div className={styles.bookDetailContainer}>
        <div className={styles.error}>
          <p>Kitap bulunamadı.</p>
        </div>
      </div>
    );
  }

  return (
    <div className={styles.bookDetailContainer}>
      <div className={styles.bookDetailContent}>
        <h1 className={styles.bookTitle}>{book.title}</h1>
        
        {book.description && (
          <div className={styles.bookSection}>
            <h2 className={styles.sectionTitle}>Açıklama</h2>
            <div className={styles.description}>
              {typeof book.description === 'string' 
                ? book.description 
                : book.description.value || 'Açıklama mevcut değil.'
              }
            </div>
          </div>
        )}

        {book.first_publish_date && (
          <div className={styles.bookSection}>
            <h2 className={styles.sectionTitle}>İlk Yayın Tarihi</h2>
            <p className={styles.publishDate}>{book.first_publish_date}</p>
          </div>
        )}

        {book.subjects && book.subjects.length > 0 && (
          <div className={styles.bookSection}>
            <h2 className={styles.sectionTitle}>Konular</h2>
            <div className={styles.subjects}>
              {book.subjects.slice(0, 10).map((subject, index) => (
                <span key={index} className={styles.subjectTag}>
                  {subject}
                </span>
              ))}
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default BookDetail; 