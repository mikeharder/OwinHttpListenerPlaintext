using Microsoft.Owin;
using Owin;
using System.Threading.Tasks;

namespace OwinHttpListenerPlaintext
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UsePlainText();
        }
    }
}
