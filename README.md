# dotnet-microservice-course

This project is my follow-along with the .Net Academy Microservice course, adding my own spin, style, and opinions along the way. 

## Getting Started

Currently, there is very little in the way of automation or configuration for this project and as such, there are a lot of magic numbers and paths required to get up and running. 

To run this project locally, start by bringing up the infrastructure components.

- Navigate to `src/infrastructure`
- Run `docker compose up -d`

This should bring up a MongoDb server and a RabbitMq management instance, both available at their default ports, visible on Docker Desktop or by running `docker ps`.

Before you attempt to bring up the microservices, ensure you have trusted the dotnet dev certs locally by running `dotnet dev-certs https --trust`.

Next, bring up the microservices.

- Navigate to `src/services/catalog`
- Run `dotnet run`
- Confirm the catalog service is running by navigating to `https://localhost:5001/swagger`
- Navigate to `src/services/inventory`
- Run `dotnet run`
- Confirm the inventory service is running by navigating to `https://localhost:5002/swagger`

This should bring up the two microservices and configure their communication channels in RabbitMQ via MassTransit. 

Finally, start the Angular SPA, which will require a local installation of Node.js.

- Navigate to `src/web/spa`
- Run `ng serve -o`

You should be able to interact with the SPA and send data to the backend services. 
