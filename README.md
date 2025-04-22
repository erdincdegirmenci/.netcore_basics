# Backend Project Template

Uygulama geliştirme çalışmalarında birçok katman ve yardımcı modülle ile yapısal standartlara uygun backend altyapısı sunar. 

## Kullanımı

Proje kullanımlarınızda herhangi namespace güncellemesine gerek kalmadan, sadece Solution (.sln) dosyasının ismini düzenlemeniz ve kaynak kod alanınıza göndermeniz yeterli olacaktır.

## Kısaltmalar ve Açıklamalar

| Kısaltma | Açıklama           |
| -------- | ------------------ |
| DAO      |Data Access Object  |
| DTO      |Data Transfer Object|

## Katman Kullanımları

| Kütüphane            | Açıklama                                   |
| -------------------- | ------------------------------------------ |
| Application.Business | İş Katmanı ile ilgili nesne tanımlarını    |
| Application.Common   | Yardımcı metot ve modeller                 |
| Application.Domain   | Veri katmanı ile ilgli nesne tanımları     |
| Application.RestApi  | Uygulamanın dışarıya açılan arayüzü        |


## Yapısı ve Klasörlerin Açıklamaları

```py
├───Application.Business    # İş Katmanı ile ilgili nesne tanımlarını 
│   ├───Common              #
│   │   ├───Const           # Katmana bağlı olarak sabitlerin içerisinde bulunduğu nesneler (Örneğin: CacheKeys)
│   │   ├───Enum            # Enum kullanımları ile ilgili türlerin tanımları 
│   │   └───Mapper          # İş katmanı modelleri ve veri katmanı modellerin eşlendiği tanımlar
│   ├───Model               #
│   │   ├───Cache           # Cache tasarım model nesneleri
│   │   ├───Request         # Request model nesneleri
│   │   └───Response        # Response model nesneleri
│   └───Service             # İşlemsel nesnelerin tanımları
│       └───Interface       # İşlemsel nesnelerin şablon tasarım, Interface nesneleri
│
├───Application.Common      # Yardımcı metot ve modeller
│   ├───Const               # Katmana bağlı olarak sabitlerin içerisinde bulunduğu nesneler (Örneğin: MessageCodes)
│   ├───Enum                # Enum kullanımları ile ilgili türlerin tanımları 
│   ├───Extension           # Katmana bağlı gereksinim duyulan extension nesne tanımları (Örneğin: StringExtension)
│   ├───Helper              # Yardımcı metotlar (Örneğin: ConfigHelper)
│   ├───Models              # Veri modelleri
│
├───Application.Domain      # Veri katmanı ile ilgli nesne tanımları
│   ├─DataAccess            #
│      ├───DAO              # İşlemsel nesnelerin tanımları
│      │   └───Interface    # İşlemsel nesnelerin şablon tasarım, Interface nesneleri
│      ├───Model            # Veri modelleri
│      └───Query            # DefineXwork.Library.DataAccess paketi mimarisinden gelen IQueryTemplate'den türemiş, nesne tanımlarını barındırır.
│
└───Application.RestApi     # Servis katmanı olarak, uygulamanın dışarıya açılan arayüzüdür.
    ├───Common              # 
    │   ├───Const           # Katmana bağlı olarak sabitlerin içerisinde bulunduğu nesneleri barındırır. (Örneğin: RestErrorCodes)
    │   ├───Extension       # Gereksinim duyulan extension nesne tanımları (Örneğin: SecurityMiddleware)
    │   ├───Filter          # ASP.NET Filter geliştirmeleri  
    │   ├───Helper          # Yardımcı metotlar (Örneğin: HttpContextHelper)
    │   └───Mapper          # API Request ve Response nesneleri ile Application.Business kütüphanesindeki modelleri eşleme nesnelerini taşır.
    ├───Controller          # Dışarıya açılan API tanımlarını barındırır.
    ├───Model               #
    │   ├───Request         # API request nesne tanımları
    │   └───Response        # API response nesne tanımları
    └───SampleDB            # Proje veritabanı şema ve nesneleri
```
