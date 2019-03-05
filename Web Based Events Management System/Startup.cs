using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web_Based_Events_Management_System.Startup))]
namespace Web_Based_Events_Management_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
