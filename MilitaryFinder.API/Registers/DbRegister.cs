using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MilitaryFinder.API.Registers.Abstract;

namespace MilitaryFinder.API.Registers
{
    public class DbRegister : IRegister
    {
        public void RegisterServices(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<Data.DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<Data.DataContext>();
            services.AddControllersWithViews();
        }
    }
}
