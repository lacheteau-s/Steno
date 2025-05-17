var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.Build();

app.ConfigureMiddlewares();
app.ConfigureEndpoints();

app.Run();