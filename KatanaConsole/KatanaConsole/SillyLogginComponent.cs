using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin;

namespace KatanaConsole
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
   public class SillyLogginComponent
    {
        AppFunc _next;
        public SillyLogginComponent(AppFunc next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
           
            //do the logging on the way out
            IOwinContext contex = new OwinContext(environment);
            Console.WriteLine("URI: {0} status code: {1}", contex.Request.Uri, contex.Response.StatusCode);
            //pass everything up through the pipeline first;
            await _next.Invoke(environment);
        }

    }
}
