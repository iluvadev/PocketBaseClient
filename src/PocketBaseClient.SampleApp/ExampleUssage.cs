using PocketBaseClient.SampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBaseClient.SampleApp
{
    public class ExampleUssage
    {
        public void Example()
        {
            var t = new TestForTypes();
            t.SelectSingle = TestForTypes.SelectSingleEnum.Option1;
        }
    }
}
