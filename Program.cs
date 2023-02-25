using NSwag.Generation.Processors.Security;
using NSwag;

var builder = WebApplication.CreateBuilder(args);
IWebHostEnvironment env = builder.Environment;

// Add services to the container.

builder.Services.AddControllers();

// 註冊NSwag提供的服務
builder.Services.AddOpenApiDocument();

var app = builder.Build();

 app.UseOpenApi(config =>
{
    config.PostProcess = (document, http) =>
    {
        document.Info.Title = "HangFire 範本程式";
        document.Info.Description = "排程託管";
    };
});

// serve Swagger UI
app.UseSwaggerUi3();

// serve ReDoc UI
app.UseReDoc(config =>  
{
    // 這裡的 Path 用來設定 ReDoc UI 的路由 (網址路徑) (一定要以 / 斜線開頭)
    config.Path = "/redoc";
});

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
