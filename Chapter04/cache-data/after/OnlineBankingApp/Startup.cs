using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using OnlineBankingApp.Data;
using OnlineBankingApp.Services;
using Microsoft.EntityFrameworkCore;

using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

using OnlineBankingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineBankingApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Environment = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            if (Environment.IsDevelopment())
            {
                services.AddDbContext<OnlineBankingAppContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("OnlineBankingAppContext")));
            }
            else
            {
                services.AddDbContext<OnlineBankingAppContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("OnlineBankingAppContext")));
            }

            services.AddIdentity<Customer,IdentityRole>(
                        options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<OnlineBankingAppContext>()
                .AddDefaultTokenProviders();
         
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);  
            });            
         
            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");                        
            });

            services.AddMvc(options =>
            {
                options.CacheProfiles.Add("NoCache",
                    new CacheProfile()
                    {
                        Duration = 30,
                        Location = ResponseCacheLocation.None, 
                        NoStore = true
                    });
            });

            services.AddSession(options =>
            {
                options.Cookie.Name = ".OnlineBanking.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });            

            services.AddSingleton<IKnowledgebaseService, KnowledgebaseService>();
            services.AddSingleton<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            if (!Environment.IsDevelopment())
            {
                services.AddHsts(options =>
                {
                    options.ExcludedHosts.Clear();                    
                    options.Preload = true;
                    options.IncludeSubDomains = true;
                    options.MaxAge = TimeSpan.FromDays(60);
                });
            }
            
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5001;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder appBuilder, IWebHostEnvironment webHostEnv)
        {
            if (webHostEnv.IsDevelopment())
            {
                appBuilder.UseDeveloperExceptionPage();
            }
            else
            {
                appBuilder.UseExceptionHandler("/Error");
                appBuilder.UseHsts();
            }
        
            appBuilder.UseHttpsRedirection();        
            appBuilder.UseStaticFiles();

            appBuilder.UseRouting();

            appBuilder.UseAuthentication();
            appBuilder.UseAuthorization();

            appBuilder.UseSession();

            appBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
