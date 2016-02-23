using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

// Add the Owin Usings:
using Owin;
using Microsoft.Owin.Hosting;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(KatanaConsole.Startup))]

namespace KatanaConsole
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var middleWare = new Func<AppFunc, AppFunc>(MyMiddleWare);
            //var otherMiddleWare = new Func<AppFunc, AppFunc>(MyOtherMiddleWare);
            //app.Use(middleWare);
            //app.Use(otherMiddleWare);

            //app.Use<MyMiddlewareComponent>();
            //app.Use<OtherMyMiddlewareComponent>();

            //app.UseMyMiddleware("This is the new greeting for myMiddleware!");
            //app.UseMyOtherMiddleware();

            app.UseSillyAuthentication();
            app.UseSillyLogging();           
            //set up the configuration options
            var options = new MyMiddlewareConfigOptions("Greeting !", "John");
            options.IncludeDate = true;

            //pass options along in call to extension method
            app.UseMyMiddleware(options);
           // app.UseMyOtherMiddleware();
        }

        public AppFunc MyMiddleWare(AppFunc next)
        {
            AppFunc appFunc = async (IDictionary<string, object> environment) => 
            {
                ////Do something with the incoming request:
                //var respone = environment["owin.ResponseBody"] as Stream;
                //using (var writer = new StreamWriter(respone))
                //{
                //    await writer.WriteAsync("<h1>Hello from my First MiddleWare</h1>");
                //}
                ////call the next middleWare in the chain:
                //await next.Invoke(environment);

                IOwinContext context = new OwinContext(environment);
                await context.Response.WriteAsync("<h1>Hello from my First Middleware</h1>");
                await next.Invoke(environment);
            };
            return appFunc;
        }

        public AppFunc MyOtherMiddleWare(AppFunc next)
        {
            AppFunc appFunc = async (IDictionary<string, object> environment) =>
            {
                //    //do something with incoming request
                //    var response = environment["owin.ResponseBody"] as Stream;
                //    using (var writer = new StreamWriter(response))
                //    {
                //        await writer.WriteAsync("<h1>Hello from my Second Middleware</h1>");
                //    };
                //    //call the next middleware in the chain:
                //    await next.Invoke(environment);

                IOwinContext context = new OwinContext(environment);
                await context.Response.WriteAsync("<h1>Hello from my Second Middleware</h1>");
                await next.Invoke(environment);
            };
            return appFunc;

        }
    }
}
