using BethanysPieShopHRM.Api.Models;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GSBlazor.Shared;

namespace BethanysPieShopHRM.Api
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
            //services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(databaseName: "BethanysPieShopHRM"));

            //add an Authorization Policy  DOESNT WORK!!!
            services.AddAuthorization(authOptions =>
            {
                authOptions.AddPolicy(
                    Policies.CanManageEmployees,
                    Policies.CanManageEmployeesPolicy());
            });


            var requireAuthenticationUserPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            services.AddAuthentication(
                IdentityServerAuthenticationDefaults.AuthenticationScheme
                ).AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://localhost:44333";
                    options.ApiName = "bethanyspieshophrapi";
                    
                });

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddCors(options =>
            {
                options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            
            services.AddControllers(configure => configure.Filters.Add(new AuthorizeFilter(requireAuthenticationUserPolicy)));
            // (c=>c.Filters.Add(new AuthorizeFilter(Policies.CanManageEmployeesPolicy())));
            //services.AddAuthorization(authOptions =>
            //{
            //    authOptions.AddPolicy(
            //        Policies.CanManageEmployees,
            //        Policies.CanManageEmployeesPolicy());
            //});

            //.AddJsonOptions(options => options.JsonSerializerOptions.ca);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            

            app.UseCors("Open");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
