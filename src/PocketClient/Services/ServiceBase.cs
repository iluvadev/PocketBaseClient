using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketClient.Services
{
    public abstract class ServiceBase
    {
        public PocketClientAppication App { get; }

        public ServiceBase(PocketClientAppication app)
        {
            App = app;
        }

    }
}
