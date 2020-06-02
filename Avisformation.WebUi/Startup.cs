using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Avisformation.WebUi.Startup))]
namespace Avisformation.WebUi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
