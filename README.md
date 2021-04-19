![Build and test workflow](https://github.com/KarolGrzesiak/GrandParadeInterview/actions/workflows/continuous-integration.yml/badge.svg)
[![Total alerts](https://img.shields.io/lgtm/alerts/g/microsoft/dotnet.svg?logo=lgtm&logoWidth=18)](https://lgtm.com/projects/g/microsoft/dotnet/alerts/)
![GitHub branch checks state](https://img.shields.io/github/checks-status/KarolGrzesiak/UsersRegistration/main)

**Setup**: 

If run on Windows or Mac, there is probably a need to change IPs in API/appsettings.json from 172.17.0.1 to localhost, as this a problem when running docker on machines different than Linux. 

**To run:**

docker-compose -f compose/docker-compose.yml up -d

**When finished:**

docker-compose -f compose/docker-compose.yml down




**After running docker-compose command, there are few available links:**

http://localhost:5000/swagger/index.html - API.

http://localhost:5341/ - Seq, centralized logging.

http://localhost:8081/ - MongoDb, dashboard for database.
