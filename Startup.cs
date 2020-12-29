using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaderBoardService.Data;
using LeaderBoardService.Data.Model;
using LeaderBoardService.Helper;
using LeaderBoardService.Service;
using LeaderBoardService.Service.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace LeaderBoardService
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("https://apps-1040731663004960.apps.fbsbx.com", "https://fb.gg/play/sakitqac", "http://localhost:7456")
                                             .AllowAnyMethod()
                                             .AllowAnyHeader();
                                  });
            });




            services.AddDbContext<DBContext>(options =>
            {
                 options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
               // options.UseMySql(ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection")));
            });

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Stores.MaxLengthForKeys = 128;
                options.User.RequireUniqueEmail = false;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<DBContext>().AddDefaultTokenProviders();


            services.ConfigureApplicationCookie(
                       options =>
                       {
                           options.Cookie.Name = "User";
                           options.LoginPath = new PathString("/Account/Login");
                           options.AccessDeniedPath = new PathString("/Account/AccessDenied");
                       });

            services.AddTransient<IScore, ScoreService>();
            services.AddTransient<IUser, UserService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
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

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            ApplicationDbInitializer.SeedUsers(userManager, roleManager);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
