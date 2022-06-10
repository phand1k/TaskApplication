using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskApplication.Startup))]
namespace TaskApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
