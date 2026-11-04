using HireUp.BLL.Repositories;
using HireUp.BLL.Services;
using HireUp.DAL.Contexts;
using HireUp.DAL.Models.Auth;
using HireUp.PL.Helper;
using HireUp.PL.Mapping;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HireUp.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // ----------------------------------------------

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<HireUpDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddScoped<IJobRepository, JobRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IJobSeekerRepository, JobSeekerRepository>();

            builder.Services.AddAutoMapper(M => {
                M.AddProfile(new JobProfile());
                M.AddProfile(new JobSeekerProfile());
                M.AddProfile(new CompanyProfile());
                M.AddProfile(new ApplicationProfile());
                M.AddProfile(new JobSeekerpfpProfile());
            });

            //--------------------------------------------------------------------
            //Auth Configrations

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/Login";
            });


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.SignIn.RequireConfirmedAccount = true;
            }).AddEntityFrameworkStores<HireUpDbContext>()
              .AddDefaultTokenProviders();

            //===============================================

            var app = builder.Build();
            //--------------------------

            //========================================================
            //Auth Configrations
            await SeedDataAsync(app);

            // ==================================
            async Task SeedDataAsync(WebApplication app)
            {
                using var scope = app.Services.CreateScope();
                var services = scope.ServiceProvider;

                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                await IdentitySeeder.SeedRolesAsync(roleManager);
                await IdentitySeeder.SeedAdmin(userManager);
            }

            //========================================================
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Auth
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
