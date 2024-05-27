using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NetcodeHub.Packages.Components.Toast;
using WebUI;
using Application.DependencyInjection;
using Application.Extensions;
using Application.Services;
using NetcodeHub.Packages.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register application services
builder.Services.AddApplicationService();
builder.Services.AddScoped<ToastService>();

// Register the custom authentication state provider
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());

// Register authentication services
builder.Services.AddAuthorizationCore();
builder.Services.AddVirtualizationService();
await builder.Build().RunAsync();
