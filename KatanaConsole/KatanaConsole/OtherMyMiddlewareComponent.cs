using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin;
namespace KatanaConsole
{
    using AppFunc = Func<IDictionary<string, object>,Task>;
    public class OtherMyMiddlewareComponent
    {
        AppFunc _next;
        public OtherMyMiddlewareComponent(AppFunc next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            IOwinContext context = new OwinContext(environment);
            await context.Response.WriteAsync("<h1>Hello from my Second Middleware</h1>");
            await _next.Invoke(environment);
        }
    }
}
