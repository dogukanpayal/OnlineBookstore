import React, { useState, useEffect } from 'react';
import SearchBar from './SearchBar';
import BookList from './BookList';
import useBooks from '../hooks/useBooks';

const HomePage = () => {
  const [query, setQuery] = useState('javascript');
  const [page, setPage] = useState(1);
  const { books, loading, error, nextPageExists } = useBooks(query, page);

  useEffect(() => {
    setQuery('javascript');
    setPage(1);
  }, []);

  const handleSearch = (q) => {
    setQuery(q);
    setPage(1); // Reset to page 1 for new searches
  };

  const loadMore = () => {
    setPage(prevPage => prevPage + 1);
  };

  return (
    <main className="App-main">
      <div className="container">
        <SearchBar onSearch={handleSearch} />
        {loading && page === 1 && <p>Yükleniyor…</p>}
        {error && <p>Hata: {error.message}</p>}
        {!loading && !error && <BookList books={books} nextPageExists={nextPageExists} onLoadMore={loadMore} />}
        {loading && page > 1 && <p>Daha fazla kitap yükleniyor…</p>}
      </div>
    </main>
  );
};

export default HomePage; 