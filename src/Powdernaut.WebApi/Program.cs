using Powdernaut.WebApi;
using MediatR;
try
{
    var builder = WebApplication.CreateBuilder(args).WebApplicationBuilder();
    Log.Information("Start Application ...");
    var app = await builder.ConfigurePipeline();
    Log.Information("Application Running ...");
    await app.RunAsync();
}
catch
{
    Log.Fatal(string.Format("Application Down : {0}", DateTime.Now));
    await Log.CloseAndFlushAsync();
    throw;
}

//using Serilog;
//using FastEndpoints;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Logging.AddFilter("LuckyPennySoftware.MediatR.License", LogLevel.None);

//builder.Host.UseSerilog((context, configuration) =>
//    configuration.ReadFrom.Configuration(context.Configuration));

//builder.Services.AddControllers();
//builder.Services.AddFastEndpoints();
//builder.Services.AddHealthChecks();
//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();
//app.MapHealthChecks("/health");
//app.UseFastEndpoints();

//app.Run();
