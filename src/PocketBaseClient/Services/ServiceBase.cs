using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBaseClient.Services
{
    public abstract class ServiceBase
    {
        internal PocketBaseClientApplication App { get; }

        public ServiceBase(PocketBaseClientApplication app)
        {
            App = app;
        }

    }
}
