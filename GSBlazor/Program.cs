using GSBlazor.MessageHandlers;
using GSBlazor.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GSBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient<BethanysPieShopHRMApiAuthorizationMessageHandler>();

            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("OidcConfiguration", options.ProviderOptions);
                // options.UserOptions.NameClaim = "email";
                //builder.Configuration.Bind("UserOptions", options.UserOptions);

                //options.ProviderOptions.Authority = "https://localhost:44333";
                //options.ProviderOptions.ClientId = "bethanyspieshophr";
                //options.ProviderOptions.DefaultScopes.Add("email");
                //options.ProviderOptions.RedirectUri = "https://localhost:44301/authentication/login-callback";
                //options.ProviderOptions.PostLogoutRedirectUri = "https://localhost:44301/authentication/logout-callback";
                //options.ProviderOptions.ResponseType = "code";

               
            });

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client =>
            client.BaseAddress = new Uri("https://localhost:44340/"))
                .AddHttpMessageHandler<BethanysPieShopHRMApiAuthorizationMessageHandler>();
            builder.Services.AddHttpClient<ICountryDataService, CountryDataService>(
                client => client.BaseAddress = new Uri("https://localhost:44340/"))
                 .AddHttpMessageHandler<BethanysPieShopHRMApiAuthorizationMessageHandler>();
            builder.Services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>
                (client => client.BaseAddress = new Uri("https://localhost:44340/"))
                 .AddHttpMessageHandler<BethanysPieShopHRMApiAuthorizationMessageHandler>();
            

            await builder.Build().RunAsync();
        }
    }
}
