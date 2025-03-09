using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Migrations;
using Migrations.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.ConfigureDefaults();

var host = builder.Build();
var dbManager = host.Services.GetRequiredService<DatabaseManager>();

dbManager.Run();
