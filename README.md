# events-api
A scalable .NET web API I'm building to practise and evidence backend specialist topics.  
![GitHub Workflow Status](https://github.com/cBridges851/events-api/actions/workflows/dotnet.yml/badge.svg?logo=github)

## Milestones:
- Create an ASP.NET Web API with simple in-memory CRUD operations ✅ 15/07/2023
- Dockerise the Web API ✅ 18/07/2023
- Add a Postgres database with NHibernate ✅ 01/08/2023
- Add second-level caching
- Add centralised logging and monitoring
- Add a service layer with MassTransit and RabbitMQ
- Implement CQRS
- Implement event sourcing

## Setup
1) Ensure you have .NET, PostgreSQL, pgAdmin and Docker (optional) installed.

If you wish to use your local database:
2) Ensure you have a PostgreSQL database server.
3) Open ./EventsAPI/appsettings.json and put in a connection string to your database server

This web API can also run on Docker. Build the image by running this command in your terminal: `docker build --rm -t events-api:latest .` in the directory the Dockerfile is located.

## How To Use
If not using Docker:
1) Run the EventsAPI project in your IDE or via your terminal `dotnet run`

If using Docker and a local database: 
1) Run `docker run --rm -p 5000:5000 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5000 events-api`

If using Docker and the database in the Docker container:
1) Run `docker-compose up --build` in the directory the docker-compose.yml file is located.


2) You can hit the endpoints in your browser or via tools like Postman:   
   Get All Events: `https://localhost:<port number>/events`  
   Get Individual Event: `https://localhost:<port number>/events/<id of event>`  
   Create Event: `https://localhost:<port number>/Events/Create`, with a JSON body like below:
   ```json
   {
      "name": "Extreme Squirrel Juggling",
      "description": "JUGGLING SQUIRRELS IN A DRAMATIC MANNER 4HEAD",
      "eventType": 0,
      "date": "2012-04-23T18:25:43.511Z"
   }
   ```
   
   Update Event: `https://localhost:<port number>/Events/Update`, with a JSON body like below:
   ```json
   {
      "id": "68958a30-af55-43db-9eb4-0650fa83cd8e",
      "name": "Extreme Squirrel Juggling",
      "description": "JUGGLING SQUIRRELS IN A DRAMATIC MANNER 4HEAD",
      "eventType": 0,
      "date": "2012-04-23T18:25:43.511Z"
   }
   ```

   Delete Event: `https://localhost:<port number>/Events/Delete` with the ID of the event you want to delete in the body.


In your browser, enter `localhost:5000/events` in the search bar and you should see the JSON that the API returns.