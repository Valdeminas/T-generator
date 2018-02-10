using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using T_generator.Data;
using T_generator.Models;
using Microsoft.AspNetCore.Mvc;
using T_generator.Controllers.Helpers;
using Microsoft.AspNetCore.Authorization;
using T_generator.Data.Amazon;
using Microsoft.AspNetCore.Mvc.Razor;
using T_generator.Services.Amazon;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System;

namespace T_generator
    {
    public class Startup
        {
        private IHostingEnvironment _env;

        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
            {
            //ImageCollider.ImageCollider.Test();

            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
                {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                //builder.AddApplicationInsightsSettings(developerMode: true);
                }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
            {
            // Add framework services.
            //services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc(config =>
            {
                config.ModelBinderProviders.Insert(0, new InvariantDecimalModelBinderProvider());
            });

            if (_env.IsProduction())
            {
                services.AddDbContext<AmazonContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ProductionConnection")));

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ProductionConnection")));
        }
            else
            {
                services.AddDbContext<AmazonContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            }

    services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/Login"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;
            });

            services.AddMvc(config =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();

                    config.Filters.Add(new AuthorizeFilter(policy));
                    // [TODO] [BK] padeployinus isitikinti, kad ant https vaziuoja
                    if (_env.IsProduction())
                    {
                        //config.Filters.Add(new RequireHttpsAttribute());
                    }
                    else
                    {
                        //config.Filters.Add(new RequireHttpsAttribute());
                    }
                });
            services.AddAuthorization(options =>
                {
                    options.AddPolicy(AdminRequirement.ADMIN_POLICY, policy => policy.Requirements.Add(new AdminRequirement()));
                    options.AddPolicy(IsNotSelfRequirement.ISNOTSELF_POLICY,policy=>policy.Requirements.Add(new IsNotSelfRequirement()));
                });

            //services.AddSingleton<IAuthorizationHandler, AdminHandler>();
            //services.AddSingleton<IAuthorizationHandler, IsNotSelfHandler>();

            services.AddTransient<IAuthorizationHandler, AdminHandler>();
            services.AddTransient<IAuthorizationHandler, IsNotSelfHandler>();

            new AuthorizationOptions()
            {
                
            };

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ViewLocationExpander());
            });

            // Add application services.
            services.AddTransient<DefaultUsersSeedData>();
            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, AmazonContext amazonContext, DefaultUsersSeedData seeder, ApplicationDbContext appContext)
            {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //app.UseApplicationInsightsRequestTelemetry();
            if (env.IsDevelopment())
                {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
                }
            else
                {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
                }

            app.UseStatusCodePagesWithRedirects("~/Home/Index");

            //app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            //app.UseIdentity();
            app.UseAuthentication();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Account", action = "Login" });
                    //defaults: new { controller = "Home", action = "Index" });
        });

            ApplicationDbInitializer.Initialize(appContext);
            AmazonDbInitializer.Initialize(amazonContext);

            seeder.AddDefaultUsers().Wait();

        }
        }
    }
