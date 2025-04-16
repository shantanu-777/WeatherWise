// Program.cs
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Supabase;
using WeatherWise.Client;
using WeatherWise.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var url = builder.Configuration["Supabase:Url"] ?? throw new Exception("Supabase URL not configured");
var key = builder.Configuration["Supabase:Key"] ?? throw new Exception("Supabase Key not configured");

var options = new SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = true
};

// Register Supabase
builder.Services.AddScoped(provider =>
    new Supabase.Client(url, key, options));

// Register Auth State Provider
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<SupabaseService>();

// Add authorization
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();