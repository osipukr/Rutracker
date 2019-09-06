using System.IO;
using Blazor.FileReader;
using Blazored.LocalStorage;
using Blazored.Modal;
using MatBlazor;
using Microsoft.AspNetCore.Blazor.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Rutracker.Client.Blazor.Interfaces;
using Rutracker.Client.Blazor.Services;

namespace Rutracker.Client.Blazor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var clientSettings = GetSettings("clientsettings.json");

            services.AddSingleton(clientSettings.ApiUrlSettings);
            services.AddSingleton(clientSettings.ViewSettings);

            services.AddAuthorizationCore();
            services.AddBlazoredModal();
            services.AddFileReaderService();
            services.AddBlazoredLocalStorage();
            services.AddAuthorizationCore();

            services.AddScoped<HttpClientService>();
            services.AddScoped<ApiAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<ApiAuthenticationStateProvider>());
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubcategoryService, SubcategoryService>();
            services.AddScoped<ITorrentService, TorrentService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<AppState>();

            services.AddMatToaster((MatToastConfiguration)clientSettings.MatToasterSettings);
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            WebAssemblyHttpMessageHandler.DefaultCredentials = FetchCredentialsOption.Include;

            app.AddComponent<App>("app");
        }

        private static ClientSettings GetSettings(string filePath)
        {
            var assembly = typeof(Startup).Assembly;
            var resource = $"{assembly.GetName().Name}.{filePath}";

            using var stream = assembly.GetManifestResourceStream(resource);
            using var reader = new StreamReader(stream);

            return JsonConvert.DeserializeObject<ClientSettings>(reader.ReadToEnd());
        }
    }
}