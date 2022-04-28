using System;
using Leerplatform.Areas.Identity.Data;
using Leerplatform.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Leerplatform.Areas.Identity.IdentityHostingStartup))]
namespace Leerplatform.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<LeerplatformContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("LeerplatformDB")));

                services.AddDefaultIdentity<LeerplatformUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<LeerplatformContext>();
            });
        }
    }
}