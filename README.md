# CI
![Build and test workflow](https://github.com/KarolGrzesiak/GrandParadeInterview/actions/workflows/continuous-integration.yml/badge.svg)
[![Total alerts](https://img.shields.io/lgtm/alerts/g/microsoft/dotnet.svg?logo=lgtm&logoWidth=18)](https://lgtm.com/projects/g/microsoft/dotnet/alerts/)

Simple build&test:
https://github.com/KarolGrzesiak/UsersRegistration/actions/workflows/continuous-integration.yml

# Setup
If run on Windows or Mac, you need to change IPs in API/appsettings.json from 172.17.0.1 to localhost, as this a problem when running docker on machines different than Linux. 

**To run: (from solution directory)**


>  docker-compose -f compose/docker-compose.yml up -d

**When finished:**

> docker-compose -f compose/docker-compose.yml down


**After running docker-compose command:**

> http://localhost:5000/swagger/index.html - API

> http://localhost:5341/ - Seq

> http://localhost:8081/ - MongoDB

# Description

## Approach  
In terms of the decisions which I've made - generally went with granularity on the endpoints level - this means, that I've created separate endpoints for MrGreen and RedBet Users registration. Depending on the amount of future brands this might be a good or bad solution - the advantage of this approach is that adding a new brand means that code related to current brands in theory shouldn't be touched (Open/Close principle comes to mind), but on the other hand if there would be a lot of new brands this might be a disadvantage as the amount of new code needed would greatly outweight the benefits. I thought about two other approaches - one would be to have a single endpoint with the unified model and try to keep the unified model inside the Domain and Application, the persistence with NoSQL wouldn't be a problem, but again, keeping unified model for all of the brands could be problematic and it would mean a lot of complex ifs inside, not mention that this single endpoint would "leak" a model and the model itself would grow tremendously probably. The other approach would be to still have a single endpoint with the unified model, but then have separate models inside Application and/or Domain and have some kind of the factories inside Application or Domain, which would create appropriate users - this would probably be better idea, but still it would impose unnecessary logic related with what values of unified model are present, which aren't - in the future, with more brands this could be problematic.
I've also went with the NoSQL as the database of my choice, simply because it seems like the data here is not having definite scheme (in the future, new Properties in the User models are not a problem) and user information doesn't really change that often - this whole bounded context seems much more read-heavy, than write-heavy. Normally you register and that's it, it's pretty rare to constantly update your information, it's much more about reading the actual information about the user. On top of all of that, getting all the users is really simple and can be easily consolidated into one endpoint. The only thing, which should probably be added here is that, even tho with NoSQL there was no need in having some kind of "Brand" property in the User database model, I think it should be added as currently there is a possibility of updating/deleting RedBet user through MrGreen endpoints.  

## Technical design
My solution is using .NET 5 with Clean/Hexagonal Architecture, DDD and CQRS with MediatR. The first question, which comes to mind is probably along the lines of "isn't that an overkill?" and the answer to this is probably yes, but let's be honest - it's an interview coding task and frankly, more often than not, this style of project in the end outweights increase in initial work and overall complexity because clear structure with proper layering and SRP are generating that much value, not to mention ease of implementing cross-cutting concerns (such as logging/validation). 

Hexagonal architecture:
![Hexagonal](https://herbertograca.files.wordpress.com/2018/11/100-explicit-architecture-svg.png)
The only difference which I like to have is having QueryHandlers in Infrastructure layer (but still keeping Queries in Application layer as it's this layer that is responsible for drawing boundaries of reporting capabilities), as the biggest advantage of CQRS is having separate read and write models, which enables us to be fast with our reads and shoot straight to database for quick reports, neglecting the aggregates etc.  (notable solutions also prefering this kind of style: https://github.com/devmentors/Pacco). Next I'll try to describe the layers.

## src

### Domain
The Domain project represents the Domain layer and contains enterprise or domain logic and includes entities, enums, exceptions, interfaces, types and logic specific to the domain layer. This layer has no dependencies on anything external. In real life scenario, where there would be a lot more entities with a lot of rules it would probably evolve into collection of more rich models (DDD). Validation here is strictly related to business rules, every ValueObject/Entity should enforce theirs invariants. 

### Application
The Application project represents the Application layer and acts as a orchestrator in business logic. This project implements CQRS with each business use case represented by a single command or query. Generally when designing directories here I prefer to split them with Vertical slicing in mind so there would be one main aggregate-type directory (Users for example) and then a business use cases which should have all the necessary classes. This layer is dependent on the Domain layer but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application needs to access a notification service, a new interface would be added to the Application and the implementation would be created within Infrastructure. Validation here is more of a data-sanitation-type. Validation decorator is a cross-cutting concern present here as this layer is responsible for validation just as a domain layer. 

### Infrastructure
The Infrastructure project represents the Infrastructure layer and contains classes for accessing external resources such as databases, file systems, web services, SMTP, and so on. These classes should be based on interfaces defined within the Application/Domain layers. Here the Domain directory is sort of a mirror of Application and Domain layers, it implements external concerns defined in those layers (for example here are the repositories implementations with Mongo database). Apart from that here are the QueriesHandlers (notice that I'm bypassing any repositories and I'm shooting straight to the database, I'm even utilizing cache here).
Added logging decorator here to present how easy it is to implement those type of cross-cutting concerns (utilizing Seq as centralized logging place). This layer contains no business logic.

### API
The API project represents one form of the communication with this bounded context (in the future another form might be added such as MQTT, and then apart from API, I would just create new project which, for example, would use RabbitMQ to consume events). This layer depends on both the Application and Infrastructure layers. Please note the dependency on Infrastructure is only to support dependency injection. Thanks to CQRS the Controllers here are really thin and simple, they contain no logic whatsoever.

## tests

### Domain.UnitTests
Simple and straight forward unit tests - by keeping the Domain layer as pure as possible it's really easy to test everything here. Test coverage is here pretty high as this is after all the heart of the application and it should in theory contain all the business logic so it should be tested as hard as possible and those tests are really cheap in terms of the work needed.

### Application.IntegrationTests
Added them as an extra because in coding task description it was said to just write unit tests, but I think a good Integration tests in those style of projects give more safety that Unit Tests and they are also pretty cheap in terms of amount of work needed. Test coverage is pretty low here as I've added them just as examples and integration here is only between my projects (SendAsync method acts basically as a controller),the infrastructure concerns are not tested, which is not that bad for now, but in the future I would try to test the integration with database also so the whole flow from sending the command/query to getting a response would be tested, but not more - going one level higher and testing flow with Controllers would be pointless imo. Ideally, then I would end up with subcutaneous tests, which I'm really a fan of.
![Tests1](https://github.com/KarolGrzesiak/UsersRegistration/blob/32e5beb2f600e7e9f7ea721896a2e2e78b0418e4/assets/piramid.png)
![Tests2](https://github.com/KarolGrzesiak/UsersRegistration/blob/32e5beb2f600e7e9f7ea721896a2e2e78b0418e4/assets/subcutaneoustests.png)
