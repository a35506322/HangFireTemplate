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

    // 註冊 SerilLog
    builder.Host.UseSerilog((context, services, configuration) => configuration
     .ReadFrom.Configuration(context.Configuration)
     .ReadFrom.Services(services)
     .Enrich.FromLogContext()
     .WriteTo.Console());

    // Add services to the container.

    // 註冊 NSwag
    builder.Services.AddOpenApiDocument();

    builder.Services.AddControllers();

    // AutoMapper
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // DI
    builder.Services.AddSingleton<DataBaseHelper>();

    // 將Service結尾且生命週期相同的物件,統一註冊
    builder.Services
        .Scan(scan => scan
        .FromAssemblyOf<Program>() // 1.遍歷Program類別所在程序集中的所有類別
        .AddClasses(classes =>  // 2.要自動註冊的類別,條件為Service結尾的類別
            classes.Where(t => t.Name.EndsWith("Service", StringComparison.OrdinalIgnoreCase)))
        .AsImplementedInterfaces() // 3.註冊的類別有實作界面
        .WithScopedLifetime() // 4.生命週期設定為Scoped
    );

    // 將Repository結尾且生命週期相同的物件,統一註冊
    builder.Services
        .Scan(scan => scan
        .FromAssemblyOf<Program>() // 1.遍歷Program類別所在程序集中的所有類別
        .AddClasses(classes =>  // 2.要自動註冊的類別,條件為Repository結尾的類別
            classes.Where(t => t.Name.EndsWith("Repository", StringComparison.OrdinalIgnoreCase)))
        .AsImplementedInterfaces() // 3.註冊的類別有實作界面
        .WithScopedLifetime() // 4.生命週期設定為Scoped
    );

    var app = builder.Build();

    // 套用Nswag文件
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

    // 每一個 Request 使用 Serilog 記錄下來 
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