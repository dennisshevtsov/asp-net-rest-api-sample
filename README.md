# ASP.NET REST API sample

The REST API for the angular-mvvm-sample project.

There are steps to run the app below:
1. Fill out variables in file [appsettings.json](https://github.com/dennisshevtsov/asp-net-rest-api-sample/blob/main/src/AspNetRestApiSample.WebApi/appsettings.json).
  - `AccountEndpoint` and `AccountKey` are credentials of a Cosmos DB account.
  - `DatabaseName`, `ContainerName` and `Throughput` are details of a container in the Cosmos DB account that the app will create after the fist run.
  - `Origins` is a URL of an instance of the [angular-mvvm-sample](https://github.com/dennisshevtsov/angular-mvvm-sample) app.
2. Run with a console or VS.

There are steps to run tests below:
1. The unit tests do not require configuring any settings.
2. The integration tests require that you configure credentials of the Cosmos DB. Configure them as it is described above.
3. Use [JMeter](https://jmeter.apache.org/) to run [the functional tests of the API](https://github.com/dennisshevtsov/asp-net-rest-api-sample/blob/main/test/AspNetRestApiSample.Test/Functional/todo_list.jmx).
