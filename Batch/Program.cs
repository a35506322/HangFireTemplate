Log.Logger = new LoggerConfiguration()
   .MinimumLevel.Information()
   .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
   .Enrich.FromLogContext()
   .WriteTo.Console(new RenderedCompactJsonFormatter())
   .WriteTo.File(new RenderedCompactJsonFormatter(), "../Logs/Batch-.txt", rollingInterval: RollingInterval.Hour)
   .CreateBootstrapLogger();

try
{
    Log.Information("Starting web host");

    var builder = WebApplication.CreateBuilder(args);

    IWebHostEnvironment env = builder.Environment;

    // ���U SerilLog
    builder.Host.UseSerilog((context, services, configuration) => configuration
     .ReadFrom.Configuration(context.Configuration)
     .ReadFrom.Services(services)
     .Enrich.FromLogContext()
     .WriteTo.Console());

    // Add services to the container.

    // ���U NSwag
    builder.Services.AddOpenApiDocument();

    builder.Services.AddControllers();

    var app = builder.Build();

    // �M��Nswag���
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

    // �C�@�� Request �ϥ� Serilog �O���U�� 
    app.UseSerilogRequestLogging();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}