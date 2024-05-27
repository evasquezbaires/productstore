# ProductStore API Documentation

This project's primary goal is to support some functions within an API Service for a Product registration system. The API was developed using Visual Studio 2022 IDE and the .NET 8 Framework. It can be run standalone and doesn't require additional installations for its correct operation, since everything is integrated for easy local execution.


## Projects

- `ProductStore.Api`: This is the entry point for project execution. It contains the definition of controllers, dependency injection management (_IoC_) for the correct functioning of its components, services, and middlewares. It also includes configuration, Swagger documentation, and project versioning.
- `ProductStore.Api.Model`: Contains the input (_Request_) and output (_Response_) models that are used for the different Endpoints.
- `ProductStore.Api.Domain`: Represents the domain of the Project itself. In this practical case, the domain focuses on operating with Products. All the necessary _Interface_ definitions for interaction with the domain, the mediators that have all the business logic, and also the definition of custom Exceptions are found here.
- `ProductStore.Api.Cache`: It is responsible for implementing a local (_Lazy_) cache for Product status. It has a global configuration for cache policies, such as the duration/expiration time of that cache. It is an Infrastructure to allow its easy change/adaptation to other types of cache (In-Memory, Distributed, HTTP, others).
- `ProductStore.Api.Client`: Allows calls to external services, in this case to obtain the discount Percentage of a product. Its endpoints are contained by configuration for easy adjustment of the _URLs_. It is aligned as an Infrastructure, to allow its easy scalability if additional services are required or if the connection method changes.
- `ProductStore.Api.Repository`: Its main function is to support the persistence of information, currently locally using _SQL Lite_, which allows the creation of a file in the Entry project (_ProductStore.Api_). It carries all the configuration of Entities and Data Context for its correct operation. It is also presented as an Infrastructure to allow the change of persistence mode (Files, Relational DB, NoSQL DB, others) in a more transparent way.
- `ProductStore.Api.Test`: Contains all the unit tests that have been defined so far for most of the components. It makes use of _Mock_ and _Bogus_ for a practical and fast implementation of the tests. Its main goal is to ensure that the current implementation is correct and that in future developments the new changes are aligned with their tests.


## Architectures

- N-Layer Architecture: An N-layer architecture has been chosen according to the requirements of the project statement and to separate the different responsibilities of the system into well-defined logical layers. This promotes the modularity, maintainability, and testability of the code.
- Microservices Architecture: Although currently there is only one Product-oriented controller, from now on this project has been structured as independent and scalable microservices. This facilitates the development and maintenance of each service.
- Clean Architecture: The principles of clean architecture have been followed to guarantee solid, maintainable, and adaptable code to future changes. This is achieved through separation of concerns, encapsulation, and the use of well-defined interfaces.


## Design Patterns

- Domain-Driven Design: DDD has been used to effectively model the problem domain. This improves code understanding and facilitates collaboration between developers and domain experts.
- Repository Pattern: It aims to abstract data access, providing a unique interface for different data sources.
- Command Query Responsibility Segregation: CQRS is being applied in the project to separate read (_queries_) and write (_commands_) operations into different models, thereby improving application performance and scalability.
- Mediator: Making use of the MediatR library, which is used as a message bus to handle communication between different components of the system. It helps us simplify the workflow and facilitates the implementation of asynchronous behaviors.
- Inversion of Control: IoC is being used to manage the creation and dependency of objects, thus facilitating the change of concrete implementations when necessary.
- Test-Driven Development: While this current development has not been 100% oriented towards the methodical TDD approach, it has encouraged code quality from the start. By writing code tests, it is ensured that each functionality meets the expected standards, helping to reduce errors and providing reliability.


## Applied Principles

- SOLID: These principles have been followed to guarantee code quality.
- Clean Code: Clean Code practices are followed to write clean, readable, and maintainable code, including: indentation, use of descriptive names, code documentation, code organization, and removal of duplicate code.
- Don't Repeat Yourself: The DRY principle was applied to avoid code duplication. This is achieved by refactoring code and creating reusable functions and modules.
- Keep It Simple, Stupid (KISS): The KISS principle is followed to maintain simple and easy-to-understand code. This involves avoiding unnecessary complexity and favoring clear and concise solutions.
- Request Validation: Using FluentValidation for robust and expressive data validation. It enabled us to create clear and easy-to-understand validation rules, enhancing data reliability.


## Execution Steps

1. Open the `productstore.sln` solution using Visual Studio 2022 IDE.
2. Verify that the solution can be opened and its dependent projects load correctly.
   - Otherwise, check if the `.net8.0` framework is available
3. Verify that the `ProductStore.Api` project is marked as the startup project.
4. Run the project with Visual Studio and wait for it to load in your browser.
   - If there is any problem with this execution, validate that the required Nuget packages have been restored correctly.
5. Different requests (`POST`, `PUT`, `GET`) can be operated directly from the Swagger window of the browser.
   - The Swagger documentation automatically offers examples of the expected _schemas_, so you would only need to modify the data as needed.
6. Validating its functionality:
   - If there is any validation error with the sent _request_ it will be shown in the Swagger window.
   - If any exception occurs in the transaction of the _request_ or _response_ it will be shown in the Swagger window.
   - If it was executed correctly, this result will also appear in the Swagger window.
      - Taking into account the previous item, it would also be possible to check the generated files, for example the Logs (_ProductStore.Api/Logs_) and the database (_ProductStore.Api/productstore.db_).