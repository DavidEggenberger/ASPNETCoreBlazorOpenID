using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETCoreBlazorOpenID.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("ASPNETCoreBlazorOpenID.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ASPNETCoreBlazorOpenID.ServerAPI"));

            builder.Services.AddOidcAuthentication(options =>
            {
                options.ProviderOptions.Authority = "https://accounts.google.com/";
                options.ProviderOptions.RedirectUri = "https://localhost:44342/authentication/login-callback";
                options.ProviderOptions.PostLogoutRedirectUri = "https://localhost:44342/authentication/logout-callback";
                options.ProviderOptions.ClientId = "";
                options.ProviderOptions.ResponseType = "id_token";
            });

            await builder.Build().RunAsync();
        }
    }
}
