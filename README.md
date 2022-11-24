# PocketClient-csharp
(WARNING: Is a concept. In active development) Basic client in C# for interacting with PocketBase, with a simple ORM to manage Collections and Registries

There is no available release yet. Is only a concept

## Concept
Simple client access to a defined PocketBase application, with simple ORM with cached objects

There will be a code generator to create classes to navigate throw Collections and Registries.

## Usage
The client code for a Pocket application will be generated

### PocketBase Application
- Name: "Example-app"
- Url: "https://example-app.myurl.io"

Ussage in client side
```csharp
var app = new ExampleAppApplication();
```
You can login as Admin
```csharp
var authAdminModel = await app.Auth.Admin.AuthenticateWithPassword("MyAdmin@email.com", "myAdminPwd");
```
or as user
```csharp
var authUserModel = await app.Auth.User.AuthenticateViaEmail("myUser@email.com", "myUserPwd");
```
The Authorization token will be sent automatically every call

### Collections in PocketBase
- Authors
- Posts:
  - Has a field "Author" related to Authors
- Labels
- PostsLabels:
  - Has a field "Label" related to Labels
  - Has a field "Post" related to Posts
  
### Navigating the data in client
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

### Getting an element
```csharp
var post = await Post.GetById("xxxxxxxx").GetAsync();
// Or
var post = await app.Data.Posts.GetById("xxxxxxxx").GetAsync();
// Or
var post = await app.Data.Crud.GetById<Post>("xxxxxxxx").GetAsync();

```
### Querying
Something like this
```csharp
	app.Data.Posts.QueryAll().GetAsync() // All Posts
	app.Data.Posts.Query(strQuery).GetAsync() // Filter Posts
	app.Data.Posts.Query(strQuery).Paged(1, 20).GetAsync() // Query paged
  app.Data.Posts.Query(strQuery).OrderBy(strOrder).Pagged(1,20).GetAsync() // Ordered and Paged
```
