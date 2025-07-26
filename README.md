# 🧩 Sudoku Oyunu - Unity Projesi

## 📖 Proje Hakkında

Bu proje, Unity oyun motoru kullanılarak geliştirilmiş kapsamlı bir **Sudoku oyunu**dur. Klasik sudoku bulmacalarının mobil ve masaüstü platformlarda oynanabilir modern bir versiyonunu sunar.

## ✨ Özellikler

### 🎮 Oyun Özellikleri

- **3 Zorluk Seviyesi**:

  - **Kolay**: 31 boş hücre ile başlama
  - **Orta**: 41 boş hücre ile başlama
  - **Zor**: 46 boş hücre ile başlama

- **Gelişmiş Oynanış**:
  - Akıllı hücre vurgulama sistemi
  - Not alma modu (9'lu grid sisteminde notlar)
  - İpucu sistemi (10 ipucu hakkı)
  - Hata sayacı (maksimum 5 hata)
  - Puan sistemi (doğru cevap başına 100 puan)
  - Gerçek zamanlı zamanlayıcı

### 🎵 Ses Sistemi

- **Dinamik Ses Yönetimi**:
  - Ana menü teması
  - Buton tıklama sesleri
  - Oyun bitişi müziği
  - Ayarlanabilir ses seviyesi

### 📱 Kullanıcı Arayüzü

- **Modern UI Tasarımı**:
  - Sezgisel kontroller
  - Oyun durdurma/devam etme
  - Oyun sonu istatistikleri
  - Seçenekler menüsü
  - Geri dönüş butonları

### 🔧 Teknik Özellikler

- **Algoritmalar**:
  - Otomatik sudoku üretimi
  - Backtracking algoritması ile çözüm doğrulama
  - Satır/sütun/kare çakışma kontrolü
  - Akıllı hücre vurgulama

## 🎯 Oynanış

### Temel Kurallar

1. Her satırda 1-9 arası sayılar bir kez bulunmalı
2. Her sütunda 1-9 arası sayılar bir kez bulunmalı
3. Her 3x3'lük karede 1-9 arası sayılar bir kez bulunmalı

### Kontroller

- **Hücre Seçimi**: Boş hücreye tıklayarak seçim yapın
- **Sayı Girişi**: Alt kısımdaki sayı butonlarını kullanın
- **Not Modu**: Not düğmesini aktifleştirerek küçük notlar alın
- **Silme**: Seçili hücredeki değeri silmek için sil düğmesini kullanın
- **İpucu**: İpucu düğmesi ile doğru cevabı görebilirsiniz

### Oyun Sonu Koşulları

- **Başarı**: Tüm hücreler doğru doldurulduğunda
- **Başarısızlık**: 5 hata yapıldığında oyun sona erer

## 🛠️ Teknik Detaylar

### Unity Versiyonu

- **Unity Editor**: 2020.3.33f1
- **Target Platform**: Mobil ve Masaüstü

### Kullanılan Paketler

- Unity Input System (1.3.0)
- Mobile Notifications (2.0.0)
- TextMeshPro (3.0.6)
- Unity GUI (UGUI)

### Proje Yapısı

```
Assets/
├── Scenes/           # Oyun sahneleri (Ana Menü, Kolay, Orta, Zor)
├── Scripts/          # C# betikleri
│   ├── AudioScripts/ # Ses yönetimi
│   ├── EasyGameScripts/    # Kolay seviye
│   ├── MediumScripts/      # Orta seviye
│   ├── HardGameScripts/    # Zor seviye
│   └── MainMenuScripts/    # Ana menü
├── Prefabs/          # Yeniden kullanılabilir nesneler
├── Textures/         # Görseller ve UI elementleri
└── Sounds/           # Ses dosyaları
```

### Ana Sınıflar

- **`SudokuGenerator`**: Sudoku bulmacası oluşturma
- **`EasyGameManager`**: Kolay seviye oyun mantığı
- **`MediumGameManager`**: Orta seviye oyun mantığı
- **`HardGameManager`**: Zor seviye oyun mantığı
- **`MainMenuManager`**: Ana menü kontrolü
- **`AudioManager`**: Ses sistem yönetimi
- **`Timer`**: Zaman takibi
- **`NumberCell`**: Hücre etkileşimi

## 🎨 Grafikler ve Ses

### Görsel Varlıklar

- Özel tasarım UI elementleri
- Sayı butonları (1-9)
- Arka plan grafikleri
- İkon ve logo tasarımları

### Ses Varlıkları

- **Müzik**: Ana menü teması (Living In A Mad World)
- **Ses Efektleri**: 10 farklı tıklama ve oyun sesi
- **Ses Formatı**: WAV ve MP3

## 🚀 Kurulum ve Çalıştırma

### Gereklilikler

- Unity 2020.3.33f1 veya üzeri
- .NET Framework 4.7.1+

### Kurulum Adımları

1. Projeyi Unity Hub ile açın
2. Gerekli paketlerin yüklenmesini bekleyin
3. `MainMenuScene` sahnesini açın
4. Play düğmesine basarak oyunu test edin

### Build Alma

1. File > Build Settings menüsünü açın
2. Hedef platformu seçin (Android/iOS/Windows/Mac)
3. Build düğmesine basın

## 🎲 Oyun Mekanikleri

### Sudoku Üretimi

- Rastgele sayı dağılımı ile başlama
- Backtracking algoritması ile geçerli çözüm oluşturma
- Zorluk seviyesine göre hücre gizleme

### Hata Kontrolü

- Gerçek zamanlı doğruluk kontrolü
- Görsel geri bildirim (kırmızı/yeşil renkler)
- Hata limiti sistemi

### Puan Sistemi

- Doğru cevap: +100 puan
- İpucu kullanımı: Puan kesintisi yok
- Süre faktörü: Hızlı tamamlama bonusu

## 📊 İstatistikler

### Oyun Sonu Bilgileri

- Toplam süre
- Kazanılan puan
- Yapılan hata sayısı
- Kullanılan ipucu sayısı

## 🔧 Geliştirici Notları

### Kod Yapısı

- **Singleton Pattern**: AudioManager, Timer, SudokuGenerator
- **Interface Kullanımı**: ISelectable arayüzü
- **Component-based Architecture**: Unity'nin modüler yapısı

### Performans Optimizasyonları

- Object pooling kullanımı
- Efficient UI updates
- Memory management

### Genişletilebilirlik

- Yeni zorluk seviyeleri eklenebilir
- Tema sistemi eklenebilir
- Çoklu dil desteği eklenebilir
- Online skorboard sistemi eklenebilir


## 🤝 Katkıda Bulunma

Proje geliştirmelerine katkıda bulunmak için:

1. Projeyi fork edin
2. Yeni bir branch oluşturun
3. Değişikliklerinizi commit edin
4. Pull request gönderin

🎮 **Keyifli Oyunlar!**
