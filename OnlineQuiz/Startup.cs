using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineQuiz.Startup))]
namespace OnlineQuiz
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
