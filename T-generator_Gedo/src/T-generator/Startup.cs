using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using T_generator.Data;
using T_generator.Models;
<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc;
using T_generator.Controllers.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace T_generator
=======
using T_generator.Services;
using T_generator.Data.Amazon;
using Microsoft.AspNetCore.Mvc.Razor;
using T_generator.Services.Amazon;

namespace T_generator
{
    public class Startup
>>>>>>> 2ece3ae610bdcbcb5ba5719814ad407260cb38d0
    {
    public class Startup
        {
        private IHostingEnvironment _env;

        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
            {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
                {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
                }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
            {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddDbContext<AmazonContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
                {
                config.Password.RequiredLength = 6;
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc(config =>
                {
                // [BK] padeployinus isitikinti, kad ant https vaziuoja
                //if (_env.IsProduction())
                    config.Filters.Add(new RequireHttpsAttribute());
                });

            services.AddAuthorization(options =>
                {
                options.AddPolicy(AdminRequirement.ADMIN_POLICY, policy => policy.Requirements.Add(new AdminRequirement(true)));
                });

            services.AddSingleton<IAuthorizationHandler, AdminHandler>();

            services.Configure<RazorViewEngineOptions>(options => {
                options.ViewLocationExpanders.Add(new ViewLocationExpander());
            });

            // Add application services.
            services.AddTransient<DefaultUsersSeedData>();
            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
<<<<<<< HEAD
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DefaultUsersSeedData seeder)
            {
=======
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, AmazonContext context)
        {
>>>>>>> 2ece3ae610bdcbcb5ba5719814ad407260cb38d0
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
                {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
                }
            else
                {
                app.UseExceptionHandler("/Home/Error");
                }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
<<<<<<< HEAD
            });

            seeder.AddDefaultUsers().Wait();
            }
        }
=======
            });

            AmazonDbInitializer.Initialize(context);
        }
>>>>>>> 2ece3ae610bdcbcb5ba5719814ad407260cb38d0
    }
