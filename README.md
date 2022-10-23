# ASP.NET REST API sample

The REST API for the angular-mvvm-sample project.

There are steps to run the app below:
1. Fill out variables in file [appsettings.json](https://github.com/dennisshevtsov/asp-net-rest-api-sample/blob/main/AspNetRestApiSample.Api/appsettings.json).
  1. `AccountEndpoint` and `AccountKey` are credentials of a Cosmos DB account.
  2. `DatabaseName`, `ContainerName` and `Throughput` are details of a container in the Cosmos DB account that the app will create after the fist run.
  3. `Origins` is a URL of an instance of the [angular-mvvm-sample](https://github.com/dennisshevtsov/angular-mvvm-sample) app.
2. Run with a console or VS.
