using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
namespace KatanaConsole
{
    public static class AppBuilderExtensions
    {
        public static void UseMyMiddleware(this IAppBuilder app, MyMiddlewareConfigOptions configOption)
        {
            app.Use<MyMiddlewareComponent>(configOption);
        }
        public static void UseMyOtherMiddleware(this IAppBuilder app)
        {
            app.Use<OtherMyMiddlewareComponent>();
        }

        public static void UseSillyLogging(this IAppBuilder app)
        {
            app.Use<SillyLogginComponent>();
        }

        public static void UseSillyAuthentication(this IAppBuilder app)
        {
            app.Use<SillAuthenticationComponent>();
        }
    }
}
