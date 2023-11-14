# EmployeesApi

This project is a RESTful API where I used ASP.NET Core and Entity Framework as an ORM to build it. 
I used SQLite as a DB for simplicity and the code-first approach to configure the structure of the DB according to the domain design (DDD).
I utilized the Repository design pattern and the Query Objects design pattern to abstract the access of the DB.
I made a service for each endpoint to focus on the business logic and make the action methods in the controller cleaner.
I used Action Filters for validating the inputs to keep the action methods clean. Also a record as DTO for each input and output.
I built the Authentication and Authorization using Identity Framework and Jwt Bearer token.
To enhance the performance I used ResponseCache to make a cache store and Marvin package to handle the cache expiration and validation.
I also used API versioning to allow Long-term changes to the API.
I used the AspNetCoreRateLimit to limit the number of requests from a specific IP.
