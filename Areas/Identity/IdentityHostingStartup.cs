using System;
using System.Configuration;
using IPhoto.Data;
using IPhoto.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

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
                        "Server=106.52.209.233;Port=3306;Database=IPhoto;Uid=root;Pwd=Irvingsoft1130;AllowUserVariables=True;sslMode=None;"
                        , new MySqlServerVersion(new Version(10, 1, 40)), mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend)
                        );
            });

                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<IPhotoContext>();
            });
        }
    }
}