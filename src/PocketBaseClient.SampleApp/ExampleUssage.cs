using PocketBaseClient.SampleApp.Models;
using static PocketBaseClient.SampleApp.Models.TestForTypes;

namespace PocketBaseClient.SampleApp
{
    public static class ExampleUssage
    {
        public static void Example1()
        {
            var app = new OrmCsharpTestApplication();
            foreach (var item in app.Data.TestForTypesCollection.LoadItems())
            {
                Console.WriteLine(item);
                Console.WriteLine($"Collection: {item.Collection?.Name} -> Is loaded: {item.Collection?.Metadata.IsLoaded}");
                foreach (var enumVal in item.SelectMultiple ?? Enumerable.Empty<SelectMultipleEnum>())
                    Console.WriteLine(enumVal);
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

        public static async void Example2()
        {
            // Our application with defined url and name inside
            var app = new OrmCsharpTestApplication();

            // Our Collection "posts"
            var posts = app.Data.PostsCollection;

            // Accessing a post
            var post1 = app.Data.GetById<Post>("myPostId_1");

            var post2 = posts.GetById("myPostId_2");

            if (!post1.Validate(out var valResult))
            {
                foreach (var validationError in valResult)
                    Console.WriteLine(validationError);
            }
            var author = app.Data.GetById<Author>("MyAuthorId");
            var tag = app.Data.GetById<Tag>("MyTagId");

            post1.Title = "The title";
            post1.Content = "Lorem Ipsum.... ";
            post1.Status = Post.StatusEnum.ToPublish;
            post1.Author = author;
            post1.Tags;
        }
    }
}
