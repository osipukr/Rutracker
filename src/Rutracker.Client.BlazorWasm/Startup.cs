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
using Rutracker.Client.BusinessLayer.Interfaces;
using Rutracker.Client.BusinessLayer.Providers;
using Rutracker.Client.BusinessLayer.Services;
using Rutracker.Shared.Models;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace Rutracker.Client.BlazorWasm
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var clientSettings = GetClientSettings();

            services.AddSingleton(clientSettings.ApiUrlOptions);
            services.AddSingleton(clientSettings.FileOptions);
            services.AddSingleton(clientSettings.PageSettings);

            services.AddLoadingBar();
            services.AddBlazoredModal();
            services.AddFileReaderService(options => options.UseWasmSharedBuffer = true);
            services.AddBlazoredLocalStorage();
            services.AddAuthorizationCore(config =>
            {
                config.AddPolicy(Policies.IsAdmin, Policies.IsAdminPolicy());
                config.AddPolicy(Policies.IsUser, Policies.IsUserPolicy());
            });

            services.AddScoped<HttpClientService>();
            services.AddScoped<ApiAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<ApiAuthenticationStateProvider>());
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubcategoryService, SubcategoryService>();
            services.AddScoped<ITorrentService, TorrentService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICommentService, CommentService>();

            services.AddMatToaster(x =>
            {
                x.Position = clientSettings.ToasterSettings.Position;
                x.PreventDuplicates = clientSettings.ToasterSettings.PreventDuplicates;
                x.NewestOnTop = clientSettings.ToasterSettings.NewestOnTop;
                x.ShowProgressBar = clientSettings.ToasterSettings.ShowProgressBar;
                x.ShowCloseButton = clientSettings.ToasterSettings.ShowCloseButton;
                x.MaximumOpacity = clientSettings.ToasterSettings.MaximumOpacity;
                x.VisibleStateDuration = clientSettings.ToasterSettings.VisibleStateDuration;
            });
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            WebAssemblyHttpMessageHandler.DefaultCredentials = FetchCredentialsOption.Include;

            app.UseLoadingBar();
            app.AddComponent<App>("app");
        }

        private static ClientSettings GetClientSettings()
        {
            var assembly = typeof(Startup).Assembly;

            var resource = $"{assembly.GetName().Name}.clientsettings.json";

            using var stream = assembly.GetManifestResourceStream(resource);
            using var reader = new StreamReader(stream);

            return JsonConvert.DeserializeObject<ClientSettings>(reader.ReadToEnd());
        }
    }
}