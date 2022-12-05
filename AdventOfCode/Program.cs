using AdventOfCode;
using AdventOfCode.Services.Solvers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string license = builder.Configuration["SyncfusionLicense"];
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(license);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSyncfusionBlazor();
builder.Services.AddScoped<ISolver, Solver>();

await builder.Build().RunAsync();

