using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Services;
using WebSite.Managers;
using WebSite.Mapping;
using System.Reflection;
using AutoMapper;

namespace WebSite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Add Entity Framework services to the services container.
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"], b => b.MigrationsAssembly("BusinessLogic")));

            // Add application services.
            AddApplicationServices(services);

            // Add AutoMapper.
            AddAutoMapper(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        private void AddApplicationServices(IServiceCollection services)
        {
            //Business Logic
            services.AddScoped<IDataContext, DataContext>(serviceProvider => serviceProvider.GetService<DataContext>());
            services.AddScoped<ICarService, CarService>();

            //Managers
            services.AddScoped<ICarManager, CarManager>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

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

        private void AddAutoMapper(IServiceCollection services)
        {
            var projectMappings = typeof(IViewModelMapping).GetTypeInfo().Assembly.GetExportedTypes()
                .Where(x => !x.GetTypeInfo().IsAbstract && typeof(IViewModelMapping).IsAssignableFrom(x))
                .Select(Activator.CreateInstance)
                .Cast<IViewModelMapping>();

            var serviceProvider = services.BuildServiceProvider();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.ConstructServicesUsing(type => serviceProvider.GetService(type));

                foreach (var m in projectMappings)
                    m.Create(cfg);
            });

            config.AssertConfigurationIsValid();

            //IMapper
            services.AddTransient<IMapper>(s => config.CreateMapper());
        }
    }
}
