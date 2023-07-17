var builder = WebApplication.CreateBuilder(args);

//Service (Container)
builder.Services.AddControllers();//, projenize API denetleyicilerini eklersiniz ve bu denetleyicileri yöneten hizmetleri yapýlandýrýrsýnýz. Böylece, HTTP isteklerine yanýt vermek ve API'nizde belirli iþlemleri gerçekleþtirmek için bu denetleyicileri kullanabilirsiniz.
builder.Services.AddEndpointsApiExplorer();//Bu kod parçasýný kullanarak, projenize API belgelendirme özelliði eklersiniz. Bu, Swagger veya OpenAPI gibi araçlarla uyumludur ve API'lerinizin otomatik olarak belgelendirilmesini saðlar. Belgeleme araçlarý, API'nizin taranmasý ve açýklayýcý bir þekilde sunulmasýyla ilgili bilgileri toplar ve bir belge üretir. Geliþtiriciler bu belgeye eriþebilir ve API'nizi daha iyi anlamak, test etmek ve tüketmek için kullanabilir.
builder.Services.AddSwaggerGen(); //swager ekeldýk nuget den ýndýrdýk gen ve uý yý


var app = builder.Build();
if (app.Environment.IsDevelopment()) //geliþtirme ortamýnda ise sweger kullansýn 
{
    app.UseSwagger(); //swager ý kullan dedýk 
    app.UseSwaggerUI();
    //gene swager gelemez bunu ýcýn propertýesten 
    //"launchUrl": "swagger", bunu ekledýk cunku ýlk actýgýmýzda swagger gelsindiye yapmýs olmak gerekir
}

app.MapControllers(); //Kýsacasý, app.MapControllers() yöntemi, uygulamanýn yapýlandýrma aþamasýnda belirtilen bir URL rotasýyla iliþkilendirilen denetleyici sýnýflarýnýn istekleri nasýl iþleyeceðini belirtir. Ýstekin geldiði URL rotasý, ilgili denetleyiciyi belirler ve denetleyici içerisindeki ilgili metodun çalýþtýrýlmasýný saðlar.

app.Run();
