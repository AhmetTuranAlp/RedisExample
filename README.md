# RedisExample
.Net Core ile Redis Cache kullanılarak cache işlemlerinin yapıldığı basit bir demo. Uygulamada ülke ve ülkelere ait şehir bilgileri üzerinden redis ile cacheleme yapılmaktadır.

# Uygulamada Kullanılan Metotlar
Get() = Data Okuma 

Set() = Data Düzenleme

Remove() = Data Silme

RemoveByPattern() = Data Silme

# Redis Hakkında
Redis(Remote Dictionary Server), en temel tanımıyla anahtar-değer (key-value) mantığıyla çalışan bir cache uygulaması. Unique bir Id’ye (key) karşılık bir değer (value) tutar. Veriyi RAM’de tutuyor olması sunucunun kapanması ile verinin tümden yok olması anlamına geliyor. Tabi bunu diske yazmanız mümkün fakat en temel haliyle Redis, veriyi RAM’de kullanmak için tasarlandı. Atanan değerin tipi sadece string değil aksine daha da kompleks tiplere destek veriyor

# Redis Kurulumu
İlk önce https://github.com/MSOpenTech/redis/releases adresine giderek Microsoft Open Tech tarafından çıkarılan son Redis sürümünü indirelim. Yazının yazıldığı tarihte son sürüm 3.2.100 olarak gözükmektedir. Ayarların kolay yapılabilmesi için MSI paketinin inidirilmesini öneririm.

MSI paketi olarak indirildiğinde komut satırında redis-cli yazarak bağlananabilirsiniz. Rar paketi indirdiyseniz, klasör içerisinde redis-cli.exe dosyasını çalıştılarak bağlantı saglayabilirsiniz. Redis çalışıp çalışmadığına dair kontrol için redis-cli.exe'yi çalıştıralım ve set value1 key1 yazarak kayıt yapabilir daha sonrada get value1 diyerek key1 değerini elde edebilirsiniz




