using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using article_manager.Services;
using article_manager.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using article_manager.Models;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Identity;

namespace article_manager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddScoped<TokenProvider>();

            //     services.AddHttpClient("CDS", client =>
            //   {
            //       client.BaseAddress = new Uri("http://localhost:5001");
            //   });

            services.AddHttpClient<IUserService, UserService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5001");
            });


            // services.AddDbContext<BDArticleManagementContext>(options =>
            //  options.UseSqlServer(Configuration.GetConnectionString("BDArticleManagement")));

            //services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<BDArticleManagementContext>();

            // Authentication
            services.AddScoped<AuthenticationStateProvider, AuthenticationService>();
            services.AddOptions();
            services.AddAuthorizationCore();

            ArticleCategoriesService(services);
            services.AddBlazoredSessionStorage();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");

                // endpoints.MapGet("/", async context =>
                // {
                //     await context.Response.WriteAsync("Hello World!");
                // });
            });
        }

        private void ArticleCategoriesService(IServiceCollection services)
        {
            services.AddTransient<ICRUDService<ArticleCategoryListItem, ArticleCategoryItem>, ArticleCategoriesService>();
            services.AddHttpClient<ICRUDService<ArticleCategoryListItem, ArticleCategoryItem>, ArticleCategoriesService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5001");
            });
        }
    }
}
