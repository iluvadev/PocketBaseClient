**Warning**: This project is in active development. Things described bellow could change. There is no available release yet.

# PocketBaseClient-csharp

PocketBaseClient-csharp is a Client library in C# for interacting with a particular PocketBase application: It maps all the PocketBase Collections and Registries to Objects and structures to work in c#

* [PocketBaseClient-csharp](#pocketbaseclient-csharp)
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
* [Installation](#installation)
* [Usage](#usage)
  * [Create your client](#create-your-client)
  * [Using your client](#using-your-client)

# Overview

## Code Generator

Fitrst, you need to use **PocketBaseClient.CodeGenerator**: It connects to PocketBase (with admin rights) and generates all custom classes and structures needed to work in your c# projects.

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

# Installation

Coming soon

# Usage

(Documentation incomplete)

## Create your client

1. **Download your schema**: use PocketBaseClient.CodeGenerator with *Admin credentials* to connect with your PocketBase application and download your schema definition in a json file
2. **Generate the code**: use PocketBaseClient.CodeGenerator again to create the C# code for your client from your downloaded schema definition.
3. **Create your client library**: Create an empty C# project (library) for your client:
   - Add a reference to PocketBaseClient
   - Add the generated C# files

## Using your client

(Documentation incomplete)
