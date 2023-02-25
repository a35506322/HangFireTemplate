using NSwag.Generation.Processors.Security;
using NSwag;

var builder = WebApplication.CreateBuilder(args);
IWebHostEnvironment env = builder.Environment;

// Add services to the container.

builder.Services.AddControllers();

// ���UNSwag���Ѫ��A��
builder.Services.AddOpenApiDocument();

var app = builder.Build();

 app.UseOpenApi(config =>
{
    config.PostProcess = (document, http) =>
    {
        document.Info.Title = "HangFire �d���{��";
        document.Info.Description = "�Ƶ{�U��";
    };
});

// serve Swagger UI
app.UseSwaggerUi3();

// serve ReDoc UI
app.UseReDoc(config =>  
{
    // �o�̪� Path �Ψӳ]�w ReDoc UI ������ (���}���|) (�@�w�n�H / �׽u�}�Y)
    config.Path = "/redoc";
});

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
