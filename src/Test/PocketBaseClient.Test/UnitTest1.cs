using PocketBaseClient.DemoTest;
using PocketBaseClient.DemoTest.Models;
using PocketBaseClient.Orm.Structures;
using static PocketBaseClient.DemoTest.Models.TestForType;

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
            var res1 = col.Filter(i => i.Id.EndsWith("a").And(i.TextNoRestrictions.StartsWith("b")).And(i.Bool.IsTrue().And(i.SelectSingle.Equal(DemoTest.Models.TestForType.SelectSingleEnum.Option1))));
            var res2 = col.Where(i => i.Id.EndsWith("a") && i.TextNoRestrictions.StartsWith("b"));
            var res3 = col.Filter(i => i.ReationSingle.Equal(new DemoTest.Models.TestForRelated()));
            res3 = col.Filter(i => i.RelationMultipleLimit.Contains(new DemoTest.Models.TestForRelated()));
         
            res3 = col.Filter(i => i.SelectMultiple.Contains(DemoTest.Models.TestForType.SelectMultipleEnum.Option1));
            //col.Filter(i => i.TextNoRestrictions.StartsWith("pepe")).SortById(asc).SortBySelectMultipleEnum(desc);
            //col.Filter(i => i.TextNoRestrictions.StartsWith("pepe")).SortBy(i => i.SelectMultiple);
            //res3.SortBy(i => i.SelectMultiple(Desc));
            //res3.SortBy(SortField.Name, SortField.Id);
            //res3.Sort(i => (i.TextNoRestrictions, i.NumberNoRestrictions));
            var res = col.Filter(i => i.Bool.IsTrue()).SortBy(i => i.Created.Desc().AndThenBy(i.Updated));
            foreach(var item in res)
            {
                item.NumberNoRestrictions = 2;
                item.NumberRestrictions = 0.1f;
                item.FileSingleRestriction.GetThumb100x100f();
                //item.FileSingleNoRestriction.LoadFromFile
                item.FileMultipleRestrictions.Re
                (item.FileMultipleRestrictions as IFieldFileList<FileMultipleRestrictionsFile>).AddFromLocalFile("fdsad");

            //    item.FileSingleNoRestriction
            }
            var i = new TestForType();
            //i.FileSingleNoRestriction.FileName
            //i.FileSingleNoRestriction.IsEmpty
            //i.FileSingleNoRestriction.Stream
            //i.FileSingleNoRestriction.AsImage
            //i.FileSingleNoRestriction.ReplaceWith(fullPath)
            //i.FileSingleNoRestriction.Upload(fullPath)
            //i.FileSingleNoRestriction.Download(fullPath)
            //i.FileSingleNoRestriction.Remove()
            //i.FileSingleNoRestriction.Thumb100x200f
        }
    }
}