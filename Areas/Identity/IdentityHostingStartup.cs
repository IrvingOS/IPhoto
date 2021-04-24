using IPhoto.Data;
using IPhoto.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

[assembly: HostingStartup(typeof(IPhoto.Areas.Identity.IdentityHostingStartup))]
namespace IPhoto.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IPhotoContext>(options => {
                    options.UseMySql(
                        "Server=127.0.0.1;Port=3306;Database=IPhoto;Uid=root;Pwd=root;AllowUserVariables=True;sslMode=None;"
                        , new MySqlServerVersion(new Version(10, 1, 40)), mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend)
                        );
            });

                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<IPhotoContext>();
            });
        }
    }
}