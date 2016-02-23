using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin;

namespace KatanaConsole
{
    // use an alias for the  OWIN AppFunc
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public class SillAuthenticationComponent
    {
        AppFunc _next;
        public SillAuthenticationComponent(AppFunc next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            IOwinContext context = new OwinContext(environment);

            // in the real world we would do REAL auth processing here...
            var isAuthorized = context.Request.QueryString.Value == "john";
            if (!isAuthorized)
            {
                context.Response.StatusCode = 401;
                context.Response.ReasonPhrase = "Not Authorized";

                //send back a really silly error page
                await context.Response.WriteAsync(string.Format("<h1>Error {0}-{1}</h1>",
                    context.Response.StatusCode,
                    context.Response.ReasonPhrase
                ));               
            }
            else
            {
                // _next is only invoked is authentication succeeds
                context.Response.StatusCode = 200;
                context.Response.ReasonPhrase = "OK";
                await _next.Invoke(environment);
            }
        }
    }
}
