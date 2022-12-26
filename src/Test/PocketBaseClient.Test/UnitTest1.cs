using PocketBaseClient.DemoTest;

namespace PocketBaseClient.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var myApp = new DemoTestApplication();
            foreach (var author in myApp.Data.AuthorsCollection.Filter(a => a.Email(Orm.Filters.OperatorText.Like, "dad")))
            {
                myApp.Data.AuthorsCollection.Count()
            }
            //myApp.Data.AuthorsCollection.Filter(a => a.Email.Like("oeoeo"))


        }
    }
}