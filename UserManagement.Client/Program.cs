using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Client.Models;
using UserManagement.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Create an instance of APISettings by binding the configuration section
var apiSettings = builder.Configuration.GetSection("APISettings").Get<APISettings>()
    ?? new APISettings(); // Ensure non-null APISettings, with default values if needed

builder.Services.AddSingleton(apiSettings); // Register APISettings as a singleton

// Register HttpClient
builder.Services.AddScoped(http => new System.Net.Http.HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Register UserService
builder.Services.AddScoped<UserService>();

await builder.Build().RunAsync();
