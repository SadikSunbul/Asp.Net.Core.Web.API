var builder = WebApplication.CreateBuilder(args);

//Service (Container)
builder.Services.AddControllers();//, projenize API denetleyicilerini eklersiniz ve bu denetleyicileri y�neten hizmetleri yap�land�r�rs�n�z. B�ylece, HTTP isteklerine yan�t vermek ve API'nizde belirli i�lemleri ger�ekle�tirmek i�in bu denetleyicileri kullanabilirsiniz.
builder.Services.AddEndpointsApiExplorer();//Bu kod par�as�n� kullanarak, projenize API belgelendirme �zelli�i eklersiniz. Bu, Swagger veya OpenAPI gibi ara�larla uyumludur ve API'lerinizin otomatik olarak belgelendirilmesini sa�lar. Belgeleme ara�lar�, API'nizin taranmas� ve a��klay�c� bir �ekilde sunulmas�yla ilgili bilgileri toplar ve bir belge �retir. Geli�tiriciler bu belgeye eri�ebilir ve API'nizi daha iyi anlamak, test etmek ve t�ketmek i�in kullanabilir.
builder.Services.AddSwaggerGen(); //swager ekeld�k nuget den �nd�rd�k gen ve u� y�


var app = builder.Build();
if (app.Environment.IsDevelopment()) //geli�tirme ortam�nda ise sweger kullans�n 
{
    app.UseSwagger(); //swager � kullan ded�k 
    app.UseSwaggerUI();
    //gene swager gelemez bunu �c�n propert�esten 
    //"launchUrl": "swagger", bunu ekled�k cunku �lk act�g�m�zda swagger gelsindiye yapm�s olmak gerekir
}

app.MapControllers(); //K�sacas�, app.MapControllers() y�ntemi, uygulaman�n yap�land�rma a�amas�nda belirtilen bir URL rotas�yla ili�kilendirilen denetleyici s�n�flar�n�n istekleri nas�l i�leyece�ini belirtir. �stekin geldi�i URL rotas�, ilgili denetleyiciyi belirler ve denetleyici i�erisindeki ilgili metodun �al��t�r�lmas�n� sa�lar.

app.Run();
