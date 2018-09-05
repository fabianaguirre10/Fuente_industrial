using System;
using Mardis.Engine.DataAccess;
using Mardis.Engine.Framework;
using Mardis.Engine.Web.Data;
using Mardis.Engine.Web.Libraries.Model;
using Mardis.Engine.Web.Libraries.Security;
using Mardis.Engine.Web.Model;
using Mardis.Engine.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
//using AspNet.Security.OAuth.GitHub;
//using Microsoft.AspNetCore.Authentication.OAuth;
using UserStore = Mardis.Engine.Web.Libraries.Security.UserStore;

namespace Mardis.Engine.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            string sAppPath = env.ContentRootPath; //Application Base Path
            string swwwRootPath = env.WebRootPath;  //wwwroot folder path

            Configuration = builder.Build();
        }

        public static IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var conn = Configuration.GetConnectionString("DefaultConnection");
            var userStore = new UserStore(conn);
            var roleStore = new RoleStore(conn);
            var manager = new ApplicationPartManager();
            manager.ApplicationParts.Add(new AssemblyPart(typeof(Startup).Assembly));
            services.AddSingleton(manager);
            services.AddSingleton<IUserStore<ApplicationUser>>(userStore);
            services.AddSingleton<IRoleStore<ApplicationRole>>(roleStore);
            M@rdis123
            services.AddMvcCore();
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<MardisContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            var sessionTimeOut = Configuration.GetValue<int>("SessionTimeOut");

            services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
                {
                    config.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromMinutes(sessionTimeOut);
                    config.Lockout = new LockoutOptions()
                    {
                        DefaultLockoutTimeSpan = TimeSpan.FromHours(2)
                    };
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {

                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                //options.SecurityStampValidationInterval = new TimeSpan(1, 12, 23, 62);
                options.SecurityStampValidationInterval = TimeSpan.FromMinutes(sessionTimeOut);
                // Cookie settings
                options.Cookies.ApplicationCookie.LoginPath = "/Account/Login";
                options.Cookies.ApplicationCookie.LogoutPath = "/Account/Login";

                // User settings
                options.User.RequireUniqueEmail = true;

                //services.AddCors();
                

            });


            services.Configure<FormOptions>(x => x.ValueCountLimit = 8192);

            services.AddDataProtection()
                .SetApplicationName("M@rdisEngin3")
                .SetDefaultKeyLifetime(TimeSpan.FromDays(14));

            services.AddCors(o => o.AddPolicy("MPT", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            // Add framework services.
            services.AddMvc()
                        .AddJsonOptions(options =>
                        {
                            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                        }
                    );
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("MPT"));
            });
            services.AddDistributedMemoryCache();

            services.AddMemoryCache();
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.

            });



            services.AddSingleton<RedisCache, RedisCache>();

            //Add context to controllers
            services.AddDbContext<MardisContext>(opc => opc.UseSqlServer(conn));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services.AddScoped<IMenuService, MenuService>();

     
            //.WithOrigins("https://localhost:44306")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddAzureWebAppDiagnostics();

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
           

            app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");
            app.UseStaticFiles();

            app.UseIdentity();
            app.UseDefaultFiles();
       
            app.UseSession();
            app.UseCors("MPT");

            //app.UseMvcWithDefaultRoute();
          

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
          
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                        "public,maxQueryString=" + "32768";
                }
            });
        }
    }
}
