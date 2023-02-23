using PocketBaseClient.DemoTest;
using PocketBaseClient.DemoTest.Models;
using System.Diagnostics;

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
            var elem = col.GetById("kabksx32qx1qhd5");
            Debug.WriteLine(elem);

            
            //elem.FileSingleNoRestriction.SaveToLocalFileAsync(@"C:\Dev\img.png").Wait();
            //elem.FileSingleRestriction.Thumb100x100f.SaveToLocalFileAsync(@"C:\Dev\thumb100.png").Wait();
            //elem.FileSingleRestriction.LoadFromLocalFile(@"C:\Dev\TestImage.jpg");
            //elem.FileSingleRestriction.Remove();
            //elem.FileMultipleNoRestrictions.AddFromLocalFile(@"C:\Dev\TestImage.jpg");
            elem.FileMultipleNoRestrictions.First().Remove();

            elem.Save();
            Debug.WriteLine(elem.ToString());
            //using(var stream = elem.FileSingleRestriction.GetStreamAsync().Result)
            //{
            //    Debug.WriteLine(stream);
            //}

            //var element = col.First();//.GetById("kabksx32qx1qhd5");
            //var stream = element.FileSingleNoRestriction.GetStreamAsync().Result;
            //element.FileSingleRestriction.Thumb100x100f.SaveToLocalFileAsync(@"C:\Dev\thumb100.png").RunSynchronously();


            //var res1 = col.Filter(i => i.Id.EndsWith("a").And(i.TextNoRestrictions.StartsWith("b")).And(i.Bool.IsTrue().And(i.SelectSingle.Equal(DemoTest.Models.TestForType.SelectSingleEnum.Option1))));
            //var res2 = col.Where(i => i.Id.EndsWith("a") && i.TextNoRestrictions.StartsWith("b"));
            //var res3 = col.Filter(i => i.ReationSingle.Equal(new DemoTest.Models.TestForRelated()));
            //res3 = col.Filter(i => i.RelationMultipleLimit.Contains(new DemoTest.Models.TestForRelated()));

            //res3 = col.Filter(i => i.SelectMultiple.Contains(DemoTest.Models.TestForType.SelectMultipleEnum.Option1));

            //col.Filter(i => i.TextNoRestrictions.StartsWith("pepe")).SortById(asc).SortBySelectMultipleEnum(desc);
            //col.Filter(i => i.TextNoRestrictions.StartsWith("pepe")).SortBy(i => i.SelectMultiple);
            //res3.SortBy(i => i.SelectMultiple(Desc));
            //res3.SortBy(SortField.Name, SortField.Id);
            //res3.Sort(i => (i.TextNoRestrictions, i.NumberNoRestrictions));
            //var lst = myApp.Data.PostsCollection;
            //var post = lst.FirstOrDefault();
            //var res = col.Filter(i => i.Bool.IsTrue()).SortBy(i => i.Created.Desc().AndThenBy(i.Updated));
            //foreach(var item in res)
            //{
            //    item.NumberNoRestrictions = 2;
            //    item.NumberRestrictions = 0.1f;
            //}

        }
    }
}