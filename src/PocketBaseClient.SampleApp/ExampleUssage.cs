using PocketBaseClient.SampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBaseClient.SampleApp
{
    public static class ExampleUssage
    {
        public static void Example1()
        {
            var app = new  OrmCsharpTestApplication();
            foreach (var item in app.Data.TestForTypesCollection.LoadItems())
                Console.WriteLine(item);
            //foreach (var item in app.Data.UsersCollection.LoadItems())
            //    Console.WriteLine(item);
            //foreach (var item in app.Data.TestForRelatedCollection.LoadItems())
            //    Console.WriteLine(item);
        }
    }
}
