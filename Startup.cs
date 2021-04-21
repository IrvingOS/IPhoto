using IPhoto.Common.MessageSender;
using IPhoto.Repositories;
using IPhoto.Repositories.Impl;
using IPhoto.Services;
using IPhoto.Services.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SqlSugar.IOC;

namespace IPhoto
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
            services.AddRazorPages();
            services.AddControllersWithViews();

            #region SqlSugarIOC
            services.AddSqlSugar(new IocConfig()
            {
                ConnectionString = "Server=106.52.209.233;Port=3306;Database=IPhoto;Uid=root;Pwd=Irvingsoft1130;",
                DbType = IocDbType.MySql,
                IsAutoCloseConnection = true
            });
            #endregion

            #region IOCÒÀÀµ×¢Èë
            services.AddCustomIOC();
            #endregion
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
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
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

    public static class IOCExtend
    {
        public static IServiceCollection AddCustomIOC(this IServiceCollection services)
        {
            //BaseRepository<Models.ApplicationUser> baseRepository = new BaseRepository<Models.ApplicationUser>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IFileRepository, FileRepository>();            
            services.AddScoped<IUserLikeService, UserLikeService>();
            services.AddScoped<IUserLikeRepository, UserLikeRepository>();

            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<UserService>();
            services.AddScoped<FileService>();
            services.AddScoped<PhotoService>();
            services.AddScoped<UserLikeService>();

            return services;
        }
    }
}
