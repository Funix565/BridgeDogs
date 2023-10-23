# BridgeDogs

Develop a simple REST API using C#.

1. `/ping` url, `Ping` action returns the following message: "Dogshouseservice.Version1.0.1"
2. `/dogs` url, `GetDogs` action allows querying dogs. It supports sorting by attribute `/dogs?attribute=weight&order=desc`. It supports pagination `/dogs?pageNumber=3&pageSize=10`. Both sorting and pagination work together.
3. `/dog` url, `PostDog` action allows creating dogs. No dogs can exist with the same name. Negative `tail_length` or `weight` fields are not supported.
4. Rate limiting implemented using `Microsoft.AspNetCore.RateLimiting` available in .NET 7. It handles situations where there are too many incoming requests to the application. Settings are configured in `DogshouseRateLimitOptions` class.
5. This application is ASP.NET Core Web API project.
6. It uses EF Core code-first approach. Please, specify the db connection string for `DogshouseContext` in the `appsettings.json` file.
7. Controller logic is covered by unit tests.
8. Application uses **Repository** software development pattern.
