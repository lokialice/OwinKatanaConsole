using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin.Hosting;
using Microsoft.Owin;


namespace KatanaConsole
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public class MyMiddlewareComponent
    {
        AppFunc _next;
        //add a member to hold the greeting
        string _greeting;
        MyMiddlewareConfigOptions _conc;
        public MyMiddlewareComponent(AppFunc next,string greeting)
        {
            _next = next;
            _greeting = greeting;
        }
        public MyMiddlewareComponent(AppFunc next, MyMiddlewareConfigOptions configOption)
        {
            _next = next;
            _conc = configOption;
        }
        public async Task Invoke(IDictionary<string, object> environment)
        {
            await _next.Invoke(environment);
            IOwinContext context = new OwinContext(environment);
            await context.Response.WriteAsync("<h1>"+_conc.GetGreeting()+"</h1>");
            context.Response.StatusCode = 200;
            context.Response.ReasonPhrase = "OK";            
        }


    }
}
