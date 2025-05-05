using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Supabase;
using WeatherWise.Client;
using WeatherWise.Client.Services;

//using Supabase.Gotrue;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//// Register HttpClient
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register HttpClient with the correct base address
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7124") // Update this to match your backend URL
});

// Register WeatherService
builder.Services.AddScoped<GetWeatherService>();

builder.Services.AddScoped(provider =>
    new Supabase.Client(
        "https://myzdvtrbdbrookwgadit.supabase.co",
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im15emR2dHJiZGJyb29rd2dhZGl0Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDQ3ODEwMTAsImV4cCI6MjA2MDM1NzAxMH0._odh4PkPRbOVw9UCcD_qgII9YJymkbiFsMkXn-Nb8ME",
        new Supabase.SupabaseOptions
        {
            AutoRefreshToken = true,
            AutoConnectRealtime = true
        }
    )
);

// Register AuthService
builder.Services.AddScoped<AuthService>();

//// Add MongoDB service
//builder.Services.AddSingleton<MongoDBService>();

await builder.Build().RunAsync();