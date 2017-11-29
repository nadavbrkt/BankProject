using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LidavBankProj.Startup))]
namespace LidavBankProj
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
