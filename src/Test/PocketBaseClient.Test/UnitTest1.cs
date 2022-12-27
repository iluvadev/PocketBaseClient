using PocketBaseClient.DemoTest;

namespace PocketBaseClient.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var myApp = new DemoTestApplication();
            //foreach (var author in myApp.Data.AuthorsCollection.Filter(a => a.Email(Orm.Filters.OperatorText.Like, "dad")))
            //{
            //    myApp.Data.AuthorsCollection.Count()
            //}
            //myApp.Data.AuthorsCollection.Filter(a => a.Email(Orm.Filters.OperatorText.Like, "erre"));
            //myApp.Data.AuthorsCollection.Filter2(a => a.Email2.Like("oeoeo"));
            //myApp.Data.AuthorsCollection.Filter2(a => a.Email2.EndsWith("gmail.com"));

            //var authors = myApp.Data.AuthorsCollection;
            //foreach (var pepe in authors.Filter2(a => a.Email2.EndsWith("gmail.com")
            //                                                  .And(a.Email2.Contains("adssda").Or(a.Email2.Contains("ppa")))
            //                                                  .Or(a.Email2.Contains("sada"))
            //                                                  ))
            //{
            //    pepe.Name = pepe.Name + "_";
            //    pepe.Email.Host
            //}
            //foreach(var t in myApp.Data.TestForTypesCollection.Filter(a => a.Bool.IsTrue().And(a.EmailNoRestrictions.StartsWith("pepe"))))

            var col = myApp.Data.TestForTypesCollection;
            var res1 = col.Filter(i => i.Id.EndsWith("a").And(i.TextNoRestrictions.StartsWith("b")).And(i.Bool.IsTrue().And(i.SelectSingle.Equal(DemoTest.Models.TestForType.SelectSingleEnum.Option1));
            var res2 = col.Where(i => i.Id.EndsWith("a") && i.TextNoRestrictions.StartsWith("b"));
            var res3 = col.Filter(i => i.ReationSingle.Equal(new DemoTest.Models.TestForRelated()));
            res3 = col.Filter(i => i.RelationMultipleLimit.Contains(new DemoTest.Models.TestForRelated()));
            res3 = col.Filter(i => i.SelectMultiple.Contains(DemoTest.Models.TestForType.SelectMultipleEnum.Option1));
        }
    }
}