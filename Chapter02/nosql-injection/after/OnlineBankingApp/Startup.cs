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
using Microsoft.EntityFrameworkCore;

using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

using OnlineBankingApp.Models;
using OnlineBankingApp.Services;

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

            services.Configure<OnlineBankDatabaseSettings>(
                Configuration.GetSection(nameof(OnlineBankDatabaseSettings)));

            services.AddSingleton<IOnlineBankDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<OnlineBankDatabaseSettings>>().Value);

            services.AddRazorPages();
            services.AddSingleton<IPayeeService, PayeeService>();

            if (Environment.IsDevelopment())
            {
                services.AddDbContext<OnlineBankingAppContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("OnlineBankingAppContext")));
            }
            else
            {
                services.AddDbContext<OnlineBankingAppContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("OnlineBankingAppContext")));
            }

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
        
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
