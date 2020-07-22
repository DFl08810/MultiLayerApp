using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Data;
using App.Core.Data.Interfaces;
using App.Core.Factories;
using App.Core.Models;
using AppLibrary.Core.Data.Interfaces;
using AppLibrary.Core.Factories;
using AppLibrary.Core.Factories.Interfaces;
using AppLibrary.Core.Models;
using AppLibrary.Core.Services;
using AppLibrary.Core.Services.Interfaces;
using DataAccess.Core;
using DataAccess.Core.Models;
using IdentityAuthLib.DataContext;
using IdentityAuthLib.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App.Core
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
            #region IdentityBlock
            services.AddDbContext<IdentityDataContext>();
            //services.AddDbContext<IdentityDataContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>();
            #endregion
            services.AddDbContext<DataContext>();
            services.AddTransient<IModelMapper<CourseModel, CourseDbModel>, AppLibrary.Core.Data.CourseDataMap>();
            services.AddTransient<DataAccess.Core.Interface.IDataAccess<CourseDbModel>, DataAccess.Core.DataAccess.TrainCourseDataAccess>();
            services.AddTransient<DataAccess.Core.Interface.IDataAccess<UserActionDbModel>, DataAccess.Core.DataAccess.UserActionDataAccess>();
            services.AddTransient<AppLibrary.Core.Services.Interfaces.ITrainCourseService, AppLibrary.Core.Services.CourseDataService>();
            services.AddTransient<AppLibrary.Core.Services.Interfaces.IUserActionService, AppLibrary.Core.Services.UserActionService>();
            services.AddTransient<IViewModelMapper<CourseViewModel, CourseModel>, App.Core.Data.CourseViewDataMap>();
            services.AddTransient<IModelMapper<UserActionModel, UserActionDbModel>, AppLibrary.Core.Data.UserActionDataMap>();
            services.AddTransient<IViewModelMapper<UserViewModel, UserActionModel>, UserActionViewModelMap>();
            services.AddTransient<ITrainCalendarFactory, TrainCalendarFactory>();
            services.AddTransient<ICourseViewFactory, CourseViewFactory>();
            services.AddControllersWithViews();
            services.AddRazorPages();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
