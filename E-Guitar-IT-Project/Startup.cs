using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_Guitar_IT_Project.Startup))]
namespace E_Guitar_IT_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
