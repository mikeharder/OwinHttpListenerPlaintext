using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OwinHttpListenerPlaintext
{
    public class PlaintextMiddleware
    {
        private static readonly PathString _path = new PathString("/plaintext");
        private static readonly byte[] _helloWorldPayload = Encoding.UTF8.GetBytes("Hello, World!");

        private readonly Func<IDictionary<string, object>, Task> _next;

        public PlaintextMiddleware(Func<IDictionary<string, object>, Task> next)
        {
            _next = next;
        }

        public Task Invoke(IDictionary<string, object> environment)
        {
            IOwinContext context = new OwinContext(environment);

            if (context.Request.Path.StartsWithSegments(_path))
            {
                return WriteResponse(context.Response);
            }

            return _next(context.Environment);
        }

        public static Task WriteResponse(IOwinResponse response)
        {
            response.StatusCode = 200;
            response.ContentType = "text/plain";
            // HACK: Setting the Content-Length header manually avoids the cost of serializing the int to a string.
            //       This is instead of: httpContext.Response.ContentLength = _helloWorldPayload.Length;
            response.Headers["Content-Length"] = "13";
            return response.Body.WriteAsync(_helloWorldPayload, 0, _helloWorldPayload.Length);
        }
    }

    public static class PlaintextMiddlewareExtensions
    {
        public static IAppBuilder UsePlainText(this IAppBuilder builder)
        {
            return builder.Use<PlaintextMiddleware>();
        }
    }
}
