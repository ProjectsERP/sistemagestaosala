using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using gestaosala.core.manager.agenda;
using gestaosala.core.manager.sala;
using gestaosala.core.manager.usuario;
using gestaosala.core.providers.agenda;
using gestaosala.core.providers.salas;
using gestaosala.core.providers.usuario;
using gestaosala.provider.agenda;
using gestaosala.provider.sala;
using gestaosala.provider.usuario;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace gestaosala
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            // Configuration = configuration;

            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var env = services.BuildServiceProvider().GetService<IHostingEnvironment>();
            

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region Dependency Injections

            services.AddTransient<IUsuarioManager, UsuarioManager>();
            services.AddTransient<ISalaManager, SalaManager>();
            services.AddTransient<IAgendaSalaManager, AgendaSalaManager>();

            services.AddTransient<IUsuarioProvider, UsuarioProvider>();
            services.AddTransient<ISalaProvider, SalaProvider>();
            services.AddTransient<IAgendaSalaProvider, AgendaSalaProvider>();

            services.AddSession();

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new[] { new CultureInfo("pt-BR") };

                    options.DefaultRequestCulture = new RequestCulture("pt-BR", "pt-BR");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                });

            #region Authentication

            //por causa de = https://github.com/dotnet/corefx/issues/8768

            services.AddAuthentication("PanelCookie")
                .AddCookie("PanelCookie", options =>
                {
                    options.LoginPath = new PathString("/");
                    options.AccessDeniedPath = new PathString("/AcessoNegado/");
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                    options.SessionStore = services.BuildServiceProvider().GetService<ITicketStore>();
                    options.Cookie.SecurePolicy = env.IsDevelopment()
                                                   ? CookieSecurePolicy.SameAsRequest
                                                   : CookieSecurePolicy.Always;
                    //options.Events
                });


            #endregion

#pragma warning disable CS0618 // Type or member is obsolete
            services.AddAutoMapper();
#pragma warning restore CS0618 // Type or member is obsolete

            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            #region Culture Info pt-BR

            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            #endregion

            app.UseSession();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            app.UseAuthentication();  
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
