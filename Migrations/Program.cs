using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateApplicationBuilder(args).Build();

var config = host.Services.GetRequiredService<IConfiguration>();

Console.WriteLine(config.GetConnectionString("Database"));

