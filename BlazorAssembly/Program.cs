using BlazorAssembly;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var app = builder.Build();

// 移除這行，因為 WebAssemblyHost 不支援 MapControllers
// app.MapControllers(); // 如果應用中有使用 API 控制器。

await app.RunAsync();
