import React, { useState, useEffect } from 'react';
import { useSearchParams } from 'react-router-dom';
import SearchBar from './SearchBar';
import BookList from './BookList';
import FilterSortBar from './FilterSortBar';
import Spinner from './Spinner';
import useBooks from '../hooks/useBooks';

const HomePage = () => {
  const [searchParams, setSearchParams] = useSearchParams();
  
  // Initialize state from URL params or defaults
  const [query, setQuery] = useState(searchParams.get('q') || 'javascript');
  const [filterAuthor, setFilterAuthor] = useState(searchParams.get('author') || '');
  const [sortField, setSortField] = useState(searchParams.get('sort') || 'title');
  const [sortOrder, setSortOrder] = useState(searchParams.get('order') || 'asc');
  const [currentPage, setCurrentPage] = useState(parseInt(searchParams.get('page')) || 1);
  
  const { books, loading, error, hasMore, loadMore } = useBooks(query, currentPage);

  // Update URL params when state changes
  useEffect(() => {
    const params = new URLSearchParams();
    if (query && query !== 'javascript') params.set('q', query);
    if (filterAuthor) params.set('author', filterAuthor);
    if (sortField !== 'title') params.set('sort', sortField);
    if (sortOrder !== 'asc') params.set('order', sortOrder);
    if (currentPage > 1) params.set('page', currentPage.toString());
    
    setSearchParams(params, { replace: true });
  }, [query, filterAuthor, sortField, sortOrder, currentPage, setSearchParams]);

  // Initialize with default search if no params
  useEffect(() => {
    if (!searchParams.get('q')) {
      setQuery('javascript');
    }
  }, []);

  const handleSearch = (q) => {
    setQuery(q);
    setCurrentPage(1); // Reset to first page on new search
  };

  const handleFilterAuthor = (author) => {
    setFilterAuthor(author);
    setCurrentPage(1); // Reset to first page on filter change
  };

  const handleSortField = (field) => {
    setSortField(field);
    setCurrentPage(1); // Reset to first page on sort change
  };

  const handleSortOrder = (order) => {
    setSortOrder(order);
    setCurrentPage(1); // Reset to first page on sort change
  };

  const handleLoadMore = () => {
    const nextPage = currentPage + 1;
    setCurrentPage(nextPage);
    loadMore();
  };

  return (
    <main className="App-main">
      <div className="container">
        <SearchBar onSearch={handleSearch} />
        
        {!loading && !error && books.length > 0 && (
          <FilterSortBar
            filterAuthor={filterAuthor}
            setFilterAuthor={handleFilterAuthor}
            sortField={sortField}
            setSortField={handleSortField}
            sortOrder={sortOrder}
            setSortOrder={handleSortOrder}
          />
        )}
        
        {loading && books.length === 0 && <Spinner />}
        {error && <p>Hata: {error.message}</p>}
        {!loading && !error && (
          <BookList 
            books={books} 
            hasMore={hasMore} 
            onLoadMore={handleLoadMore}
            filterAuthor={filterAuthor}
            sortField={sortField}
            sortOrder={sortOrder}
          />
        )}
        {loading && books.length > 0 && <p>Daha fazla kitap yükleniyor…</p>}
      </div>
    </main>
  );
};

export default HomePage; 