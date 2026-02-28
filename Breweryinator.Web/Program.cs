using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Breweryinator.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// ── API HTTP client (attaches access token automatically) ─────────────────────
var apiBaseUrl = builder.Configuration["ApiBaseUrl"]
                 ?? builder.HostEnvironment.BaseAddress;

builder.Services.AddHttpClient("Breweryinator.Api", client =>
    client.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("Breweryinator.Api"));

// ── Google OIDC authentication ────────────────────────────────────────────────
builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Oidc", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "token id_token";
});

await builder.Build().RunAsync();
