process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

const BACKEND_URL = 'http://localhost:5000/api/kitap';
const LOGIN_URL = 'http://localhost:5000/api/auth/login';
const DEBUG_URL = 'http://localhost:5000/api/kitap/debug-token';
const DEBUG_NO_AUTH_URL = 'http://localhost:5000/api/kitap/debug-no-auth';
const GOOGLE_BOOKS_API = 'https://www.googleapis.com/books/v1/volumes?q=roman&maxResults=20';

// Kendi kullanıcı bilgini gir
const EMAIL = 'ahmetyilmaz@hotmail.com';
const SIFRE = '123456';

async function testNoAuthEndpoint() {
  console.log('\n=== NO AUTH TEST ===');
  try {
    const resp = await fetch(DEBUG_NO_AUTH_URL);
    if (resp.ok) {
      const data = await resp.json();
      console.log('No auth endpoint yanıtı:', data);
    } else {
      console.log('No auth endpoint hatası:', resp.status, await resp.text());
    }
  } catch (error) {
    console.log('No auth endpoint exception:', error.message);
  }
  console.log('=== NO AUTH TEST BİTTİ ===\n');
}

async function testToken(token) {
  console.log('\n=== TOKEN DEBUG TEST ===');
  const headers = {
    'Authorization': `Bearer ${token}`
  };
  
  try {
    const resp = await fetch(DEBUG_URL, { headers });
    if (resp.ok) {
      const data = await resp.json();
      console.log('Token debug yanıtı:', JSON.stringify(data, null, 2));
    } else {
      const errorText = await resp.text();
      console.log('Token debug hatası:', resp.status, errorText);
    }
  } catch (error) {
    console.log('Token debug exception:', error.message);
  }
  console.log('=== DEBUG TEST BİTTİ ===\n');
}

async function main() {
  // 1. Önce authentication gerektirmeyen endpoint'i test et
  await testNoAuthEndpoint();

  // 2. Giriş yap ve token al
  const loginResp = await fetch(LOGIN_URL, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email: EMAIL, sifre: SIFRE })
  });
  const loginData = await loginResp.json();
  console.log('Login yanıtı:', loginData);
  let token = loginData.token;

  // Test için token'ı manuel olarak sabitlemek istersen aşağıdaki satırı aç:
  // token = 'BURAYA_POSTMANDA_CALISAN_TOKENI_YAPISTIR';

  // 3. Token'ı test et
  await testToken(token);

  // 4. Kitapları çek ve ekle
  const res = await fetch(GOOGLE_BOOKS_API);
  const data = await res.json();

  for (const item of data.items) {
    const info = item.volumeInfo;
    const kitap = {
      ad: info.title || 'Bilinmeyen',
      yazar: (info.authors && info.authors.join(', ')) || 'Bilinmeyen',
      fiyat: Math.floor(Math.random() * 200) + 20,
      aciklama: info.description || '',
      kategori: (info.categories && info.categories[0]) || 'Genel',
      kapakResmiUrl: info.imageLinks ? info.imageLinks.thumbnail : ''
    };

    const headers = {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    };
    console.log('Kullanılan token:', token);
    console.log('Header:', headers);

    const resp = await fetch(BACKEND_URL, {
      method: 'POST',
      headers,
      body: JSON.stringify(kitap)
    });

    if (resp.ok) {
      console.log('Eklendi:', kitap.ad);
    } else {
      const errorText = await resp.text();
      console.log('Hata:', kitap.ad, 'Status:', resp.status, 'Mesaj:', errorText);
    }
  }
}

main();