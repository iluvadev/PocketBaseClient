**Warning**: This project is in active development. Things described bellow could change.



With **PocketBaseClient** and the code generated for your application with [pbcodegen](doc/pbcodegen.md), you can interact with your PocketBase server without having to worry about communication, APIs, object binding, cache management, etc.
It is an ORM connected to your PocketBase server with your application.


You will be able to do things like:

```csharp
var myApp = new MyTodoListApplication(); // Binding to PocketBase 'my-todo-list' application
var myData = myApp.Data; // The data of the application
            
var tasks = myData.TasksCollection; // Collection "tasks"

// Iterate over entire collection
foreach (var task in tasks)
    Console.WriteLine($"{task.Title} ({task.Status}): {task.Description}");

// Filter your data: Paused tasks updated long time ago
var oldPausedTasks = tasks.Filter(t => t.Status.Equal(Task.StatusEnum.Paused).And(
                                            t.Updated.LessThan(DateTime.Now.AddMonths(-1))));

// Find an element
var users = myData.UsersCollection; // Collection "users"
var me = users.GetById("qwertyuiop");

// Modify your elements
foreach (var oldPausedTask in oldPausedTasks)
{
    oldPausedTask.Status = Task.StatusEnum.ToDo; // Status is an Enum (type select)
    oldPausedTask.AssignedTo = me;  // AssignedTo is an User (type relation)
}

// Create a new element
var newTask = new Task
{
    Title = "A new Task",
    Description = "Lorem ipsum...",
    Status = Task.StatusEnum.ToDo,
};

// Save all changes to PocketBase (also new elements)
myData.SaveChanges();

```

# PocketBaseClient-csharp

PocketBaseClient-csharp is a Client library in C# for interacting with a particular PocketBase application: It maps all the PocketBase Collections and Registries to Objects and structures to work in c#

Set up an ORM to access your PocketBase application data in less than 1 minute with pbcodegen, the **PocketBaseClient CodeGenerator**

* [PocketBaseClient-csharp](#pocketbaseclient-csharp)
* [Installation](#installation)
* [Overview](#overview)
  * [Code Generator](#code-generator)
  * [What PocketBaseClient can do](#what-pocketbaseclient-can-do)
  * [How it works](#how-it-works)
    * [Collections and Model Registries](#collections-and-model-registries)
    * [Cache](#cache)
  * [Methods and Properties](#methods-and-properties)
    * [Collections](#collections)
    * [Object Models](#object-models)
      * [Object Metadata](#object-metadata)
  * [Querying](#querying)
* [Usage](#usage)

# Installation

The only thing that you need is [pbcodegen](doc/pbcodegen.md). Download the latest version from the [Releases section](https://github.com/iluvadev/PocketBaseClient-csharp/releases), and follow the process.

In less than 1 minute you will have a customized ORM in c# for your PocketBase application, your own PocketBaseClient.

More information in [pbcodegen](doc/pbcodegen.md)

# Overview

## Code Generator

[**pbcodegen**](doc/pbcodegen.md) generates your PocketBaseClient, the ORM for your PocketBase application in less than 1 munute: 

It connects to your PocketBase (with admin rights) and generates a c# project for you, with all the logic for the management of comunications with server and  persistence of objects. Exposes custom classes and structures that maps to your Collections and Registries.

In your .NET projects, you will only need a reference to this generated project to access your PocketBase application.

See more information in [pbcodegen](doc/pbcodegen.md)

## What PocketBaseClient can do

PocketBaseClient uses the classes generated to map data and operate with a particular PocketBase application: 

1. Creating enums for fields of type "*Select*".

2. Assigning validation rules for all fields with restrictions.

3. Mapping related registry Ids to relations between objects.

4. Managing the comunication with PocketBase server.

5. Caching objects in memory to minimize the api calls.

6. Getting unknown object data from server when needed (lazy load).

7. Registering changed objects that are in memory and not updated to server.

8. Registering new objects in memory that will map to a new registry in the server. 

9. Exposing a simple fluent Filter adapted to the objects

10. CRUD options with objects

11. ...

Is a real ORM for a concrete PocketBase application.

Ok, it sounds good, but, how really works?

## How it works

The magic happens at the junction of PocketBaseClient library and the code generated with *PocketBaseClient.CodeGenerator*:

- Every Collection has its own class with internal cache for its Registries

- Registries have their own class, mapping fields to properties

- Each Registry field is mapped to the correct type, enum or class

### Collections and Model Registries

Each collection has its own class, and internally mantains a cache for its objects. Every object in a Collection is (or will be) a Registry in the mapped PocketBase Collection.

Every registry belongs to a Collection. This is translated as every Modeled object belongs to a Collection: also new ones (that are only in memory, without a Registry in the server yet).

When is created a new object that would become a Registry in PocketBase, it is registered automatically to the correct Collection.

Every change in an object that maps to a registry, marks the object as "modified".

The changes in memory of the objects can be discarded or saved.

There is only a function `Save` to save objects to server, it does not matter if the Registry is to be created, updated or deleted in PocketBase.

### Cache

Every time a Registry is downloaded from PocketBase, it will be mapped as an object and cached. 

The new objects are also cached: they live in memory until they are saved to PocketBase.

The deleted objects are marked as "to be deleted in PocketBase"

When an element is requested, by default PocketBaseClient will try to serve it from memory and if it is not cached, from server. This behavior can be modified at every call.

## Methods and Properties

There are a lot of properties and methods in any modeled object:

### Collections

- Information about the Collection (properties `Id`, `Name` and `System`)

- Get all elements of the collection (enumerate over the collection, implements `IEnumerable<T>`, or use the method `GetItems`)

- Get an element by Id (methods `GetById` and `GetByIdAsync`)

- Check if contains an element (method `Contains`)

- Delete an element (method `Delete`)

- Save to server all changes in the collection and its elements (create, update or delete) (methods `SaveChanges`, `SaveChangesAsync`)

- Discard changes in the collection that are not saved to server (method `DiscardChanges`)

- Query elements (property `Filter`). This will be explained below

### Object Models

* Each field has its own Property with the correct type

* Information about collection (properties `Collection`, `CollectionId` and `CollectionName`)

* Information about the object status (property `Metadata_`, see [Object Metadata](#object-metadata))

* Validate object data (method `Validate` or properties `Metadata_.IsValid` and `Metadata_.ValidationErrors`)

* Reload the object with data in server (methods `Reload` and `ReloadAsync`)

* Delete the element (method `Delete`)

* Discard local changes of the object (method `DiscardChanges`)

* Save element to server (create or update) (methods `Save` and `SaveAsync`)

* Check if two objects references the same registry (method `IsSame`)

* Update the object with data of other (method `UpdateWith`)

#### Object Metadata

Contains properties with information about the object:

* Validation:  `IsValid`, `ValidationErrors`

* Changes:  `HasLocalChanges`, `FirstChange`, `LastChange`

* Status: `IsCached`, `IsLoaded`, `IsNew`, `IsTobeDeleted`, `IsTrash`, `SyncStatus`

## Querying

(Documentation incomplete)

# Usage

Supose this PocketBase Schema of an application with name '`my-todo-list`':

```
 ┌──────────────────┐       ┌──────────────────┐
 │ todo_lists       │       │ tasks            │       ┌────────────────┐
 ├──────────────────┤       ├──────────────────┤       │ priorities     │
 │ name        :T   │       │ title       :T   │       ├────────────────┤
 │ description :T   │     N │ description :T   │     1 │ name        :T │
 │ tasks       :Rel ------->│ priority    :Rel ------->│ value       :# │
 └──────────────────┘       │ status      :Sel │       │ description :T │
                            │     │to do       │       └────────────────┘
                            │     │doing       │
                            │     │paused      │
                            │     │done        │
                            └──────────────────┘
```

With [pbcodegen](doc/pbcodegen.md) you can generate the Model and it can be used as:

```csharp
// 'my-todo-list' application
var myApp = new MyTodoListApplication(); 

// The data of the application
var myData = myApp.Data;

// Print all todo_lists registries:
foreach (var todoList in myData.TodoListsCollection)
{
   Console.WriteLine($"{todoList.Name} ({todoList.Description})");
   foreach (var task in todoList.Tasks)
   {
      Console.WriteLine($"   - {task.Title} ({task.Description})");
      Console.WriteLine($"     {task.Priority.Name}");
      Console.WriteLine($"     {task.Status}");
      if (task.Status == Task.StatusEnum.Done)
         Console.WriteLine("Well done!!");
   }
}

// Filter over todo_lists collection in PocketBase
var myTodoLists = myData.TodoListsCollection.Filter(todo => todo.Name(OperatorText.Like, "my todo list")).GetItems();

// Linq in-memory (using cache, getting all registries from server if needed)
var myTodoLists2 = myData.TodoListsCollection.Where(t => t.Name.Contains("my todo list"));

var allPriorities = myData.PrioritiesCollection;

// Getting by Id
var myList = myData.TodoListsCollection.GetById("qwertyuiop");
// Creating a new Task (it will be a registry in 'tasks' collection)
var newTask = new Task
{
   Title = "My Title",
   Description = "My Description",
   Priority = allPriorities.OrderByDescending(p => p.Value).First(),
   Status = Task.StatusEnum.ToDo
};
// Add it in the Tasks list (it will modify the 'tasks' field)
myList.Tasks.Add(newTask);

// Now, the changes are only in memory

// Save the todo_list (and its tasks)
myList.Save();

// Or save all todo_lists
myData.TodoListsCollection.SaveChanges();

//Or save all changes in all collections
myData.SaveChanges();

// Now, the changes are saved to PocketBase
```

(Documentation incomplete)
