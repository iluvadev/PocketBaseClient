# PocketBaseClient-csharp

**Warning**: This project is in active development, and some parts are only a proof of concept. Things described bellow could change. There is no available release yet.

Client in C# for interacting with a particular PocketBase application, with a simple ORM to manage  Collections and Registries.

## Description

* **PocketBaseClient** includes a set of libraries to interact with PocketBase, and has all the logic needed for the ORM.
* **PocketBaseClient.CodeGenerator** connects with your PocketBase application in order to parse your schema and generates a c# client for your application

### Steps to make your client

- **Download your schema**: use PocketBaseClient.CodeGenerator with *Admin credentials* to connect with your PocketBase application and download your schema definition in a json file
- **Generate the code**: use PocketBaseClient.CodeGenerator again to create the C# code for your client from your downloaded schema definition.
- **Create your client library**: Create an empty C# project (library) for your client:
  - Add a reference to PocketBaseClient
  - Add the generated C# files

### Generated code

PocketBaseClient.CodeGenerator generates all the code needed to map all your **Collections**, **Registries** and **Fields** (including the validation rules):

For each **Collection**:

- Generate a class for the collection (for example _PostsCollection_)
- Generate a class for its Registries (for example _Post_)

**Fields** of **Registries**:

- Each field is a property of the Registry class (for example _Post.Title_)
- If there are restrictions in the PocketBase field, these are translated to Validation Attributes.
- The "**select**" type fields maps to enums
- If the field accept multiple values is mapped to:
  - A specific LimitedList if there are a limit of values
  - A List if there are no limits
- If the field is a "**relation**", is integrated with the ORM to link with the object that represents the related registry
- Fields of type "**file**": Not implemented yet

# Example

Imagine a PocketBase application for a basic blog, with the "*APPLICATION NAME = orm-csharp-test*", and "*APPLICATION URL = https://orm-csharp-test.pockethost.io*"

## PocketBase Collections

For the example, the PocketBase application has the following collections:

- posts: with the blog posts information

- authors: with basic information about authors: name, email, url and profile

- categories: only with name

- tags: only with name

The most "complex" collection is *posts*, with this definition:

![](doc/img/PocketBase_Collection_posts.png)

Where:

- **title**: Has a "*MIN LENGTH = 5*", is "*Nonempty*" and "*Unique*"

- **author**: Is related to collection "*authors*" with "*MAX SELECT* = 1"

- **summary**: Has a "*MAX LENGTH = 100*" 

- **status**: Select with "*MAX SELECT = 1*", and *CHOICES = draft, to review, reviewed, to publish, published*

- **categories**: relation to "*COLLECTION = categories*" with "*MAX SELECT = 3*"

- **tags**: relation to "*COLLECTION = tags*", without "*MAX SELECT*"

## Generated code

With PocketBaseClient.CodeGenerator, we will generate the code to access to the PocketBase application. 

After generating the code, we can create a new project (library) and include all the generated code, adding a reference to PocketBaseClient, or include all generated code in your main project.

With these steps we will have a custom tools to access our PocketBase application.

The entry point of our application is the class **OrmCsharpTestApplication** (name generated from the Application name "*orm-csharp-test*" )

## Using the code

Whe can use the main application class without any parameter:

```csharp
// Our application with defined url and name inside
var app = new OrmCsharpTestApplication();
```

In the Application main class, we have these main properties:

### Auth

Object with options to authenticate in PocketBase. The authentication may be as "Admin" or "User":

```csharp
// Authenticate as Admin
var resAdmin = await app.Auth.Admin.AuthenticateWithPassword("myadmin@email.io", "MyAdminPwd");

// Authenticate as User
var resUser = await app.Auth.User.AuthenticateWithPassword("myUser@email.io", "MyUserPwd");
```

There are also functions to Reset passwords, authenticate clients with OAuth2, and to create new users.

### Sdk

PocketBaseClient internally uses **[pocketbase-csharp-sdk](https://github.com/PRCV1/pocketbase-csharp-sdk)**

With this property you can access directly to the sdk. In many cases, this access will not be necessary since the generated client will be enough for us.

### Data

This property is the main entry for our data: our collections and registries. With the generated code, we have all collections inside:

```csharp
// Our Collection "posts"
var posts = app.Data.PostsCollection;
```

And we have several ways to access our data:

```csharp
// Accessing a post
var post1 = app.Data.GetById<Post>("myPostId_1");

var post2 = posts.GetById("myPostId_2");
```

Every post maps a registry in posts Collection, with all fields.

Every field type in PocketBase is converted as a c# equivalent, depending of the restrictions defined in the model:

```csharp
var author = app.Data.GetById<Author>("MyAuthorId");
var tag = app.Data.GetById<Tag>("MyTagId");

post1.Title = "The title";
post1.Content = "Lorem Ipsum.... ";
post1.Status = Post.StatusEnum.ToPublish;
post1.Author = author;
post1.Tags.Add(tag);
```

The defined restrictions in PocketBase are automatically translated as Validations:

```csharp
if (!post1.Validate(out var valResult))
{
    foreach (var validationError in valResult)
        Console.WriteLine(validationError);
}
```



# Old readme.md (to review)

## Concept

Simple client access to a defined PocketBase application, with simple ORM with cached objects

There will be a code generator to create classes to navigate throw Collections and Registries, and tools to query data. This client will manage cached objects (with policies) to minimize the api calls.

## Usage

The client code for a PocketBase client application will be generated by a code generator that maps all Collections and Registry schemas to code.

### PocketBase Application

- Name: "Example-app"
- Url: "https://example-app.myurl.io"

Ussage in PocketClient

```csharp
var app = new ExampleAppApplication();
```

You could login as Admin

```csharp
var authAdminModel = await app.Auth.Admin.AuthenticateWithPassword("MyAdmin@email.com", "myAdminPwd");
```

or as user

```csharp
var authUserModel = await app.Auth.User.AuthenticateWithPassword("myUser@email.com", "myUserPwd");
```

(of course, login is not mandatory)
The Authorization token will be sent automatically at every query to get data.

### Collections in PocketBase

Example of collections:

- Authors
- Posts:
  - Has a field "Author" related to Authors
- Labels
- PostsLabels:
  - Has a field "Label" related to Labels
  - Has a field "Post" related to Posts

### Navigating data in PocketClient

Similar than:

```csharp
var author = post.Author;
var posts = author.Posts;
var labels = post.PostsLabels.Select(pl => pl.Label);
```

### Getting Collections in Client

Similar than:

```charp
var authors = app.Data.Authors;
var Posts = app.Data.Posts;
```

### Operations with an element

Getting an element:

```csharp
var post = await Post.ById("xxxxxxxx").GetAsync();
// Or
var post = await app.Data.Posts.ById("xxxxxxxx").GetAsync();
// Or
var post = await app.Data.Crud.ById<Post>("xxxxxxxx").GetAsync();
```

Creating an element (only creates the object in memory):

```csharp
var author = new Author(mandatoryField1, mandatoryField2);
// Or
var author = Author.Create(mandatoryField1, mandatoryField2);
```

Saving or Updating an element:

```csharp
author.SaveAsync(); // If is a new element without Id, it will be filled from PocketBae after saved
// Or
app.Data.Crud.SaveAsync(author); // If is a new element without Id, it will be filled from PocketBae after saved
```

Deleting an element:

```csharp
author.DeleteAsync(); 
// Or
app.Data.Crud.DeleteAsync(author); 
```

Discarting local changes not saved:

```csharp
author.Discard(); // Discard local changes in the Author element
// Or
app.Data.Crud.Discard(author); // Discard local changes in the Author element

app.Data.Authors.Discard(); // Discard local changes in every element in Authors collection
// Or
app.Data.Crud.Discard<Author>(); // Discard local changes in every element of type Author

app.Data.Crud.Discard(); // Discard all local changes in any element
```

Reloading data from PocketBase:

```csharp
author.ReloadAsync(); // Reloads the Author item

app.Data.Authors.ReloadAsync(); // Reloads all Author collection

myQuery.ReloadAsync(); // Reloads all Query results
```

### Querying

Something like this

```csharp
app.Data.Posts.QueryAll().GetAsync(); // All Posts
app.Data.QueryAll<Post>().GetAsync(); // All Posts
author.Posts.QueryAll().GetAsync(); // All Posts of the author

app.Data.Posts.Query(strQuery).GetAsync(); // Filter Posts
app.Data.Query<Post>(strQuery).GetAsync(); // Filter Posts
author.Posts.Query(strQuery).GetAsync(); // Filter Posts of the author

app.Data.Posts.Query(strQuery).Paged(1, 20).GetAsync(); // Query paged
app.Data.Query<Post>(strQuery).Paged(1, 20).GetAsync(); // Query paged
author.Posts.Query(strQuery).Paged(1, 20).GetAsync(); // Query paged Posts of the author

app.Data.Posts.Query(strQuery).OrderBy(strOrder).Paged(1,20).GetAsync(); // Ordered and Paged
app.Data.Query<Post>(strQuery).OrderBy(strOrder).Paged(1,20).GetAsync();
author.Posts.Query(strQuery).OrderBy(strOrder).Paged(1,20).GetAsync(); // Query ordered and paged Posts of the author

app.Data.Posts.Query(strQuery).OrderBy(strOrder).First().GetAsync(); 
app.Data.Query<Post>(strQuery).OrderBy(strOrder).First().GetAsync();
author.Posts.Query(strQuery).OrderBy(strOrder).First().GetAsync();
```

### More...

To think about:

- Fetching
- Force reload items (or not) in GetById, Collection list, Query
- Queries and OrderBy in Fluent form
