**Warning**: This project is in active development. Things described bellow could change.

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
    * [Of any Collection](#of-any-collection)
    * [Of any Model](#of-any-model)
  * [Querying](#querying)
* [Usage](#usage)

# Installation

The only thing that you need is [pbcodegen](doc/pbcodegen.md). Download the latest version from the [Releases section](https://github.com/iluvadev/PocketBaseClient-csharp/releases), and follow the process.

In less than 1 minute, you will have a customized ORM in c# for your PocketBase application, your own PocketBaseClient.

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

There is only a function `Save` to save objects to server, it does not matter if the Registry is to be created or updated.

### Cache

Every time a Registry is downloaded from PocketBase, it will be cached for the containing Collection. 

Every time is created a new object of the collection, is cached: The new elements are also cached and marked as they are new

When an element is requested, by default it will try to serve it from memory and if is not cached, from server. This behavior can be modified at every call.

## Methods and Properties

### Of any Collection

- Information about the Collection (properties `Id`, `Name` and `System`)

- Get all elements of the collection (property `Items` and method `GetItems`)

- Get an element by Id (methods `GetById` and `GetByIdAsync`)

- Save elements (create or update) to server (methods `Save` and `SaveAsync`)

- Discard changes not saved to server (method `DiscardChanges`)

- Delete elements (methods `Delete`, `DeleteAsync`, `DeleteById` and `DeleteByIdAsync`)

- Query elements (property `Filter`). This will be explained below

### Of any Model

* Each field has its own Property of the correct type

* Validate object data (methods `Validate` and `IsValid`)

* Reload object with data in server (methods `Reload` and `ReloadAsync`)

* Discard local changes of the object (method `DiscardChanges`)

* Save element to server (create or update) (methods `Save` and `SaveAsync`)

* Delete the element (methods `Delete` and `DeleteAsync`)

And other advanced:

- Information about collection (properties `Collection`, `CollectionId` and `CollectionName`)

- Information about object status (property `Metadata_`)

- Check if two objects references the same registry (method `IsSame`)

- Update the object with data of other (method `UpdateWith`)

## Querying

(Documentation incomplete)

# Usage

(Documentation incomplete)
