using BusinessLayer.Services;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVCPresentationLayer.Configuration;
using MVCPresentationLayer.Filters;
using MVCPresentationLayer.Middlewares;

namespace MVCPresentationLayer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(LoggingActionAttribute));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddOptions();
            services.Configure<PaginationSection>(Configuration.GetSection("Pagination"));
            services.Configure<LoggingFilterSection>(Configuration.GetSection("LoggingFilter"));
            services.Configure<CacheSection>(Configuration.GetSection("Cache"));

            var dbConnection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EntityFrameworkContext>(options => options.UseSqlServer(dbConnection));

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            services.AddScoped<ICacheFileRepository, CacheFileRepository>();
            services.AddScoped<ICache, Cache>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISupplierService, SupplierService>();
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
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseEndpointRouting();
            app.UseMiddleware<CacheMiddleware>();

            app.UseHttpsRedirection();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   "image",
                   "images/{id}",
                   new { controller = "Category", action = "Picture" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
