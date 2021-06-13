using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(OOAD_Projekat.Areas.Identity.IdentityHostingStartup))]
namespace OOAD_Projekat.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}