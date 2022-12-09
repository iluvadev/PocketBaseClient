using PocketBaseClient.Orm.Filters;
using PocketBaseClient.SampleApp.Models;
using static PocketBaseClient.SampleApp.Models.TestForTypes;

namespace PocketBaseClient.SampleApp
{
    public static class ExampleUssage
    {
        public static void Example1()
        {
            var app = new OrmCsharpTestApplication();
            foreach (var item in app.Data.TestForTypesCollection.Items)
            {
                Console.WriteLine(item);
                //Console.WriteLine($"Collection: {item.Collection?.Name} -> Is loaded: {item.Collection?.Metadata.IsLoaded}");
                foreach (var enumVal in item.SelectMultiple ?? Enumerable.Empty<SelectMultipleEnum>())
                    Console.WriteLine(enumVal);

                item.TextNoRestrictions += "bla";
                Console.WriteLine(item);
            }

            var elem = new TestForRelated();
            elem.NumberUnique = 1;
            var isValid = elem.Validate(out var valResult);
            Console.WriteLine($"elem is Valid: {isValid}");
            foreach (var valRes in valResult)
            {
                Console.WriteLine(valRes);
            }
            //foreach (var item in app.Data.UsersCollection.LoadItems())
            //    Console.WriteLine(item);
            //foreach (var item in app.Data.TestForRelatedCollection.LoadItems())
            //    Console.WriteLine(item);
        }

        public static void Example2()
        {
            // Our application with defined url and name inside
            var app = new OrmCsharpTestApplication();

            // Our Collection "posts"
            var posts = app.Data.PostsCollection;
            //var items = app.Data.CategoriesCollection.Items;

            foreach (var category in app.Data.CategoriesCollection.Items)
            {
                Console.WriteLine(category);
                category.Name += " modif";
            }
            var newCat = new Category();

            foreach (var category in app.Data.CategoriesCollection.Items)
            {
                Console.WriteLine(category);
            }

            posts = Post.GetCollection();

            var cat1 = Category.GetById("sywd90gz2ifd7pf")!;
            cat1.Name += "das";
            Console.WriteLine(cat1);
            Console.WriteLine(Category.GetById("sywd90gz2ifd7pf", true));
            cat1.Name = "Music";
            cat1.Save();
            Console.WriteLine(cat1);
            cat1.DiscardChanges();
            Console.WriteLine(cat1);

            var post1 = cat1;


            if (!post1.Metadata.IsValid)
            {
                foreach (var validationError in post1.Metadata.ValidationErrors)
                    Console.WriteLine(validationError);
            }

            //var test = new TestForTypes();
            //test.TextNoRestrictions = "My text";
            //test.SelectMultiple.Add(SelectMultipleEnum.Option3);
            //test.SelectMultiple.Add(SelectMultipleEnum.Option4);
            ////cat.UpdateWith(cat1);
            //Console.WriteLine(test);

            //cat1.DiscardChanges();
            //app.Data.DiscardChanges(cat1);
            //app.Data.DiscardChanges(app.Data.CategoriesCollection);
            //app.Data.CategoriesCollection.DiscardChanges(cat1);


            //// Accessing a post
            //var post1 = app.Data.GetById<Post>("myPostId_1");

            //var post2 = posts.GetById("myPostId_2");

            //if (!post1.Validate(out var valResult))
            //{
            //    foreach (var validationError in valResult)
            //        Console.WriteLine(validationError);
            //}
            //var author = app.Data.GetById<Author>("MyAuthorId");
            //var tag = app.Data.GetById<Tag>("MyTagId");

            //post1.Title = "The title";
            //post1.Content = "Lorem Ipsum.... ";
            //post1.Status = Post.StatusEnum.ToPublish;
            //post1.Author = author;
            //post1.Tags.Add(tag);
        }

        public static async Task Example3()
        {
            // Our application with defined url and name inside
            var app = new OrmCsharpTestApplication();

            // Our Collection "posts"
            var cats = app.Data.CategoriesCollection;

            await foreach (var cat in cats.Filter(p => p.Name(OperatorText.Equal, "Music")).GetItemsAsync())
                Console.WriteLine(cat);

            foreach(var cat in cats.Filter("name~'music'").GetItems())
                Console.WriteLine(cat);

            var post = app.Data.PostsCollection.Items.First();

            post.

            //foreach (var cat in cats.Items)
            //    Console.WriteLine(cat);
        }
    }
}
