Trendyol Link Converter
Proje Adı
Trendyol Link Converter  Web Api 
Gereksinimler
•	.NET 5.0
•	Redis
•	Visual Studio 2019
Proje Tanımı
Trendyol Link Converter 2  endpointten oluşan bir web api projesidir.
Endpointlerden biri kendisine string olarak gönderilen bir Web URL’i belirli kurallar dahilinde Deeplink’e çevirir.Diğer endpointte yine kendisine string olarak gönderilen Deeplink’i aynı kurallar ile Web URL’e çevirir.Her bir request ve response işlemini Redis üzerinde saklar.
Proje Mimarisi
Proje 4 katmandan oluşmaktadır.
•	TrendyolLinkConverter.Data katmanı  .Net Core Class Library projesidir. Bu katmanda projede ihtiyaç duyulan Enum, Request-Response Objeleri ve Veritabanı Modelleri mevcut.
•	TrendyolLinkConverter.Business katmanı .Net Core Class Library projesidir.Referans olarak Data katmanını alır.Link Converter için gerekli kuralları işleten, yani asıl işlemlerin yapıldığı sınıfları içeren katmandır.Sınıflar tek bir response dönecek şekilde yazılmıştır.Bu sınıflar Base bir sınıftan, Base sınıfta Generic Request ve Response alan Base bir interfaceden türer. 

ConvertDeeplinkToWebUrl sınıfı kendisine GetWebUrlRequestObject nesnesinden gelen Deeplink’i bir Uri formatına dönüştürerek istenen kuralları işletip GetWebUrlResponseObject tipinde bir response ile Web URL döner.

ConvertWebUrlToDeeplink sınıfı kendisine GetDeeplinkRequestObject nesnesinden gelen Web URL’i bir Uri formatına dönüştürerek istenen kuralları işletip GetDeeplinkResponseObject tipinde bir response ile Deeplink döner.

•	TrendyolLinkConverter.Test katmanı XUnit .Net Core Test projesidir. TDD uygulamak amacıyla Business katmanında ilgili işlemleri yapan sınıflar yazıldıktan sonra burdaki test sınıfları ve senaryoları aracılığıyla test edilmiştir.

•	TrendyolLinkConverter.WebApi katmanı  .Net Core Web Api projesidir. Referans olarak Business katmanını alır. İlgili controller’lar Business katmanındaki sınıfları çağırıp kullanıcıya json response döner. 

 

Business katmanındaki sınıflar bağımlılıkları azaltmak için Startup dosyasında register edilmiştir.

RedisService sınıfı Redis’e connection oluşturan bir Connect Methodu ve  kendisine gelen objeyi Json fortamında Redis’e kaydeden bir Set methodundan oluşmaktadır.
Connection işleminin konfigürasyonu Startup sınıfı içerisinde yapılmıştır.Redis Host ve Port bilgileri appsettings.json dosyasında tutulmaktadır.

ConvertDeeplinkToWebUrlController içerisinde bulunan GetWebUrl HttpPost methodu GetWebUrlRequestObject tipinde Deeplink özelliği verilen Json request alarak IBaseLinkConverter üzerinden register olunan Convert methodunu çağırmaktadır. Bu methoddan dönen GetWebUrlResponseObject tipindeki objeyi Json response olarak dönmektedir.Eğer convert işlemi başarılı ise responsta bulunan Success özelliği true ve WebUrl özelliği dolu bir şekilde dönecektir. Eğer Success false ise WebUrl null ve ExeptionMessage olarak karşılaşılan hata dönecektir. Her request ve response’u TrendyolLinkConverterModel tipindeki nesneye vererek bir Guid key ile Redis’e Json olarak kaydetmektedir.

Endpoint: https://localhost:44376/api/ConvertDeeplinkToWebUrl/GetWebUrl
HttpMethod: POST
Request: {
"Deeplink":"ty://?Page=Product&ContentId=1925865&CampaignId=439892&MerchantId=105064"
}
Response:{
"webUrl":" https://www.trendyol.com/brand/name-p-1925865?boutiqueId=439892&merchantId=105064"
"success": true,
"exceptionMessage": null
}




ConvertWebUrlToDeeplinkController içerisinde bulunan GetDeeplinkUrl HttpPost methodu GetDeeplinkRequestObject tipinde WebUrl özelliği verilen Json request alarak IBaseLinkConverter üzerinden register olunan Convert methodunu çağırmaktadır. Bu methoddan dönen GetDeeplinkResponseObject tipindeki objeyi Json response olarak dönmektedir.Eğer convert işlemi başarılı ise responsta bulunan Success özelliği true ve Deeplink özelliği dolu bir şekilde dönecektir. Eğer Success false ise Deeplink null ve ExeptionMessage olarak karşılaşılan hata dönecektir. Her request ve response’u TrendyolLinkConverterModel tipindeki nesneye vererek bir Guid key ile Redis’e Json olarak kaydetmektedir.

Endpoint: https://localhost:44376/api/ConvertWebUrlToDeeplink/GetDeeplink
HttpMethod: POST
Request: {
"webUrl":" https://www.trendyol.com/brand/name-p-1925865?boutiqueId=439892&merchantId=105064"
}
Response:{
"Deeplink":" ty://?Page=Product&ContentId=1925865&CampaignId=439892&MerchantId=105064"
"success": true,
"exceptionMessage": null
}


 
 
 
