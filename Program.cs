using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PortfolioSite;
using Supabase;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var url = builder.Configuration["Supabase:Url"];
var key = builder.Configuration["Supabase:Key"];

builder.Services.AddScoped<Supabase.Client>(provider => 
    new Supabase.Client(url, key, new Supabase.SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = true
    }));

// Diğer servislerin yanına ekle
builder.Services.AddScoped<PortfolioSite.Services.LanguageService>();

await builder.Build().RunAsync();
