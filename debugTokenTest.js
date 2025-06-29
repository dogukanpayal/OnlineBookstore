const axios = require('axios');
process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

const BASE_URL = 'http://localhost:5000'; // x.x kısmını kendi IP adresinle değiştir
const DEBUG_URL = `${BASE_URL}/api/kitap/debug-token`;

// Postman'da çalışan token'ı buraya yapıştır!
const token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjUiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWhtZXQgWcSxbG1heiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImFobWV0eWlsbWF6QGhvdG1haWwuY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE3NTEwODQyMzQsImlzcyI6Ik9ubGluZUJvb2tTdG9yZSIsImF1ZCI6Ik9ubGluZUJvb2tTdG9yZVVzZXJzIn0.THzEVWOjXVTmMJ5iELdPDm_JaSbuDofaMT0rWEmHi7g';

axios.get(DEBUG_URL, {
  headers: {
    'Authorization': `Bearer ${token}`,
    'Content-Type': 'application/json'
  }
}).then(res => {
  console.log(res.data);
}).catch(err => {
  if (err.response) {
    console.log('Hata:', err.response.status, err.response.data);
  } else {
    console.log('Hata:', err.message);
  }
}); 