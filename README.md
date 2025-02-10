# Library API Service
A company is launching a library management service. The service will be a web API layer built using .NET, with an existing prepared infrastructure. Implement two controllers: in LibrariesController, add the DELETE method, and in BooksController, implement the POST, and GET methods as per the guidelines below. Additionally, implement corresponding services for the controllers, and register these services in the Startup.cs file to enable Dependency Injection.

## Environment
- .NET version: 6.0

## Read-Only Files
- LibraryService.Tests/IntegrationTests.cs

## Commands
- run:  
```
dotnet clean && dotnet restore && dotnet run --project LibraryService.WebAPI
```
- install:  
```
dotnet clean && dotnet build
```
- test: 
```
dotnet restore && dotnet build && dotnet test --logger xunit --results-directory ./reports/
```

## Sample Data
Here is an example of a library model JSON object:

```
{
    id: 5,
    name: "Library name",
    location: "2838 Violet Ct, Columbus, IN 47201, USA"
}  
```

Here is an example of a book model JSON object:

```
{
    id: 3,
    name: "The Norton Anthology of English Literature",
    category: "Anthology",
    libraryId: 5
}  
```

## Requirements

The service should adhere to the following API format and response codes:

`POST api/libraries/{libraryId}/books`:
  - Add the book to the given libraryId. 
  - The HTTP response code should be 201 on success.
  - For the body of the request, use the JSON example of the book model given above.
  - If a library with `{libraryId}` does not exist, return 404.

`GET api/libraries/{libraryId}/books`:
  - Return the entire list of books for the library with the given libraryId.
  - The HTTP response code should be 200.
  - If a library with `{libraryId}` does not exist, return 404.
 
 `DELETE api/libraries/{libraryId}`:
  - Delete the library with libraryId. 
  - The HTTP response code should be 204 on success.
  - If a library with `{libraryId}` does not exist, return 404.
 
NOTE: You need to add support for Dependency Injection for internal services (LibrariesService and BooksService) in the project Startup.cs file.

## Sample Requests & Responses
<details><summary>Expand to view details on sample requests and responses for each endpoint.</summary>

`POST api/libraries/5/books`

Example request:

```
{
    id: 3,
    name: "The Norton Anthology of English Literature",
    category: "Anthology"
}
```
The response code will be 201, and this book will be added to the library with ID 5.

`GET api/libraries/5/books`

Example response:

The response code is 200, and when converted to JSON, the response body (assuming that the below objects are all objects in the collection) is as follows:

```
[{
    id: 3,
    name: "The Norton Anthology of English Literature",
    category: "Anthology",
    libraryId: 5
} {
    id: 10,
    name: "Inception",
    category: "Thriller",
    libraryId: 5
}]
```

`DELETE api/libraries/10`

Example response:

Assuming that the library with ID 10 exists, the response code is 204 and there are no particular requirements for the response body. This causes the library with ID 10 to be removed from the collection. When a library with ID 10 does not exist, the response code is 404 and there are no particular requirements for the response body.

</details>
