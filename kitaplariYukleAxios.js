process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

const axios = require('axios');
const https = require('https');
const agent = new https.Agent({ rejectUnauthorized: false });

const BASE_URL = 'https://localhost:5001';// Postman'da çalışan adresi kullan!
const LOGIN_URL = `${BASE_URL}/api/auth/login`;
const DEBUG_URL = `${BASE_URL}/api/kitap/debug-token`;
const GOOGLE_BOOKS_API = 'https://www.googleapis.com/books/v1/volumes?q=roman&maxResults=20';

const EMAIL = 'ahmetyilmaz@hotmail.com';
const SIFRE = '123456';

async function main() {
  // 1. Giriş yap ve token al
  console.log('Login isteği gönderiliyor:', LOGIN_URL);
  const loginResp = await axios.post(LOGIN_URL, { email: EMAIL, sifre: SIFRE }, { httpsAgent: agent });
  const token = loginResp.data.token;
  console.log('Login yanıtı:', loginResp.data);

  // 2. Token ile debug endpointini test et
  console.log('Debug endpoint isteği gönderiliyor:', DEBUG_URL);
  console.log('Token:', token.substring(0, 50) + '...');
  try {
    const debugResp = await axios.get(DEBUG_URL, {
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      },
      httpsAgent: agent
    });
    console.log('DEBUG TOKEN yanıtı:', debugResp.data);
  } catch (err) {
    console.log('DEBUG TOKEN HATASI DETAYI:');
    console.log('URL:', DEBUG_URL);
    console.log('Headers:', { 'Authorization': `Bearer ${token.substring(0, 50)}...`, 'Content-Type': 'application/json' });
    if (err.response) {
      console.log('Status:', err.response.status);
      console.log('Data:', err.response.data);
      console.log('Headers:', err.response.headers);
    } else {
      console.log('Network Error:', err.message);
    }
  }

  // Debug endpoint testi - authentication olmadan
  console.log('\n=== AUTHENTICATION OLMADAN DEBUG ENDPOINT TESTİ ===');
  try {
    const debugResponseWithoutAuth = await axios.get(DEBUG_URL, { httpsAgent: agent });
    console.log('Authentication olmadan debug endpoint yanıtı:', debugResponseWithoutAuth.data);
  } catch (error) {
    console.log('Authentication olmadan debug endpoint hatası:', error.response?.status, error.response?.data);
  }

  // Debug endpoint testi - authentication ile
  console.log('\n=== AUTHENTICATION İLE DEBUG ENDPOINT TESTİ ===');
  try {
    const debugResponseWithAuth = await axios.get(DEBUG_URL, {
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      },
      httpsAgent: agent
    });
    console.log('Authentication ile debug endpoint yanıtı:', debugResponseWithAuth.data);
  } catch (error) {
    console.log('Authentication ile debug endpoint hatası:', error.response?.status, error.response?.data);
    console.log('Hata detayı:', error.response?.data);
  }

  // 3. Kitapları çek ve ekle
  const booksResp = await axios.get(GOOGLE_BOOKS_API);
  for (const item of booksResp.data.items) {
    const info = item.volumeInfo;
    const kitap = {
      ad: info.title || 'Bilinmeyen',
      yazar: (info.authors && info.authors.join(', ')) || 'Bilinmeyen',
      fiyat: Math.floor(Math.random() * 200) + 20,
      aciklama: info.description || '',
      kategori: (info.categories && info.categories[0]) || 'Genel',
      kapakResmiUrl: info.imageLinks ? info.imageLinks.thumbnail : ''
    };

    try {
      const resp = await axios.post(`${BASE_URL}/api/kitap`, kitap, {
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json'
        },
        httpsAgent: agent
      });
      console.log('Eklendi:', kitap.ad);
    } catch (err) {
      if (err.response) {
        console.log('Hata:', kitap.ad, 'Status:', err.response.status, 'Mesaj:', err.response.data);
      } else {
        console.log('Hata:', kitap.ad, 'Mesaj:', err.message);
      }
    }
  }
}

main(); 