import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { Helmet } from 'react-helmet';

const RatingForm = ({ bookId, onReviewSubmit }) => {
  const [rating, setRating] = useState(0);
  const [comment, setComment] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    if (rating > 0) {
      onReviewSubmit({ rating, comment, date: new Date().toISOString() });
      setRating(0);
      setComment('');
    }
  };

  return (
    <div style={{
      marginTop: '30px',
      padding: '20px',
      backgroundColor: '#f8f9fa',
      borderRadius: '8px'
    }}>
      <h3 style={{ marginBottom: '15px', color: '#333' }}>Değerlendirme Yap</h3>
      <form onSubmit={handleSubmit}>
        <div style={{ marginBottom: '15px' }}>
          <label style={{ display: 'block', marginBottom: '5px', fontWeight: '500' }}>
            Yıldız Seçimi:
          </label>
          <div style={{ display: 'flex', gap: '5px' }}>
            {[1, 2, 3, 4, 5].map((star) => (
              <button
                key={star}
                type="button"
                onClick={() => setRating(star)}
                style={{
                  background: 'none',
                  border: 'none',
                  fontSize: '24px',
                  cursor: 'pointer',
                  color: star <= rating ? '#ffc107' : '#ddd'
                }}
              >
                ★
              </button>
            ))}
          </div>
        </div>
        
        <div style={{ marginBottom: '15px' }}>
          <label style={{ display: 'block', marginBottom: '5px', fontWeight: '500' }}>
            Yorum:
          </label>
          <textarea
            value={comment}
            onChange={(e) => setComment(e.target.value)}
            rows="4"
            style={{
              width: '100%',
              padding: '10px',
              border: '1px solid #ddd',
              borderRadius: '4px',
              resize: 'vertical'
            }}
            placeholder="Kitap hakkında düşüncelerinizi paylaşın..."
          />
        </div>
        
        <button
          type="submit"
          disabled={rating === 0}
          style={{
            padding: '10px 20px',
            backgroundColor: rating > 0 ? '#007bff' : '#ccc',
            color: 'white',
            border: 'none',
            borderRadius: '4px',
            cursor: rating > 0 ? 'pointer' : 'not-allowed'
          }}
        >
          Değerlendirme Gönder
        </button>
      </form>
    </div>
  );
};

const BookDetail = () => {
  const { id: workId } = useParams();
  const [book, setBook] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [reviews, setReviews] = useState([]);

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
      // Load existing reviews
      const savedReviews = localStorage.getItem(`reviews_${workId}`);
      if (savedReviews) {
        setReviews(JSON.parse(savedReviews));
      }
    }
  }, [workId]);

  const handleReviewSubmit = (review) => {
    const newReviews = [...reviews, review];
    setReviews(newReviews);
    localStorage.setItem(`reviews_${workId}`, JSON.stringify(newReviews));
  };

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

  const bookTitle = book.title || 'Kitap Detayı';
  const bookDescription = typeof book.description === 'string' 
    ? book.description 
    : book.description?.value || 'Açıklama mevcut değil.';

  return (
    <>
      <Helmet>
        <title>Book Detail – {bookTitle}</title>
        <meta name="description" content={bookDescription.substring(0, 160)} />
      </Helmet>
      
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
          {bookTitle}
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
              {bookDescription}
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
          <div style={{ marginBottom: '30px' }}>
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

        <RatingForm bookId={workId} onReviewSubmit={handleReviewSubmit} />

        {reviews.length > 0 && (
          <div style={{ marginTop: '30px' }}>
            <h2 style={{
              fontSize: '1.5rem',
              fontWeight: '600',
              color: '#555',
              marginBottom: '15px'
            }}>
              Değerlendirmeler ({reviews.length})
            </h2>
            {reviews.map((review, index) => (
              <div key={index} style={{
                border: '1px solid #ddd',
                borderRadius: '8px',
                padding: '15px',
                marginBottom: '15px',
                backgroundColor: '#f9f9f9'
              }}>
                <div style={{ marginBottom: '10px' }}>
                  {[1, 2, 3, 4, 5].map((star) => (
                    <span
                      key={star}
                      style={{
                        color: star <= review.rating ? '#ffc107' : '#ddd',
                        fontSize: '18px'
                      }}
                    >
                      ★
                    </span>
                  ))}
                  <span style={{ marginLeft: '10px', color: '#666', fontSize: '14px' }}>
                    {new Date(review.date).toLocaleDateString('tr-TR')}
                  </span>
                </div>
                {review.comment && (
                  <p style={{ margin: 0, color: '#333', lineHeight: '1.5' }}>
                    {review.comment}
                  </p>
                )}
              </div>
            ))}
          </div>
        )}
      </div>
    </>
  );
};

export default BookDetail; 