using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyRental.Startup))]
namespace MyRental
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
