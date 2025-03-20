using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Migrations;
using Migrations.Services;

var builder = Host.CreateApplicationBuilder(args);
var import = args.Contains("import");

builder.Services.ConfigureDefaults(import);

var host = builder.Build();
var dbManager = host.Services.GetRequiredService<DatabaseManager>();

await dbManager.RunAsync();

if (import)
    await host.Services.GetRequiredService<ImportManager>().RunAsync();
