using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using EmpPayPack.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EmpPayPack.Services;
using EmpPayPack.Services.Implementation;
using Rotativa.AspNetCore;
using EmpPayPack.Constants;

namespace EmpPayPack
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // RequireConfirmedAccount doesn't allow signin unless the mail id is confirmed, for testing purpose
            // setting it to false
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                // Default Password Setting
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = ConstantsKeys.SIGNIN_PASSWORD_LENGTH_REQUIRED;

                // Default Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(ConstantsKeys.SIGNIN_LOCKOUT_TIME_IN_MINUTES_REQUIRED);
                options.Lockout.MaxFailedAccessAttempts = ConstantsKeys.SIGNIN_MAX_FAILED_ACCESS_ATTEMPTS;
                options.Lockout.AllowedForNewUsers = true;
            });


            services.AddControllersWithViews();
            services.AddRazorPages();

            

            // Adding employee service interface and its implementation to the service collection, which in turn will create and dispose of its instance
            // as and when required
            /*
             * Adding a service to this method or in IServiceCollection helps in following:
             * Injection of the service into the constructor of the class where it's used. 
             * The framework takes on the responsibility of creating an instance of the dependency and disposing of it when it's no longer needed.
             */
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPaymentCalculationService, PaymentCalculationService>();
            services.AddScoped<INationalInsuranceContributionService, NationalInsuranceContributionService>();
            services.AddScoped<ITaxService, TaxService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseAuthentication();
            app.UseAuthorization();

            DataSeedingInitializer.UserAndRoleSeedAsync(userManager, roleManager).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            RotativaConfiguration.Setup(env.WebRootPath, "Rotativa");
        }
    }
}
