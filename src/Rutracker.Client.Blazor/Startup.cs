using System.IO;
using Blazor.FileReader;
using Blazored.LocalStorage;
using Blazored.Modal;
using MatBlazor;
using Microsoft.AspNetCore.Blazor.Http;
using Microsoft.AspNetCore.Components;
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

            services.AddBlazoredModal();
            services.AddFileReaderService();
            services.AddBlazoredLocalStorage();
            services.AddAuthorizationCore();

            services.AddSingleton<HttpClientService>();
            services.AddSingleton<ApiAuthenticationStateProvider>();
            services.AddSingleton<AuthenticationStateProvider>(s => s.GetRequiredService<ApiAuthenticationStateProvider>());
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<ISubcategoryService, SubcategoryService>();
            services.AddSingleton<ITorrentService, TorrentService>();
            services.AddSingleton<ICommentService, CommentService>();
            services.AddSingleton<AppState>();

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