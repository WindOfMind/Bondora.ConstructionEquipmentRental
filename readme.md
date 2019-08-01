# Bondora Construction Equipment Rental

The solution is a self-service system for renting construction equipment.
Solution is implemented as a two-tier system: a web front-end (ASP.NET Core service and Angular SPA as a client app), and a separate backend service handling the business logic (console application).
Inter-services communication is based on NServiceBus that allows to use any transport layer (RabbitMQ, MSMQ, Azure Service Bus and so on).

Backend service (ConstructionEquipmentRental.Billing) contains handlers that process all input commands and generate invoices.

Frontend service (ConstructionEquipmentRental.Service) receive requests from client app for getting an invoice and push commands to NServiceBus. Once the invoice is generated NServiceBus will send it to the frontend service. After that using SignalR hub the frontend service push the invoice to the client app.

Client app is Angular SPA with several components, services and a equipment state (NgRx). All equipment data is stored and operated via state and store that allow to keep the code as performant, consistent.

Class and interaction diagram: https://github.com/WindOfMind/Bondora.ConstructionEquipmentRental/blob/master/ConstructionEquipmentRentalDiagram.pdf

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

What things you need to run the software:

* Visual Studio 2017 or greater
* Node.js https://nodejs.org/
* [.NET Core 2.2 SDK](https://dotnet.microsoft.com/download/visual-studio-sdks)

### Running the app

1. Install Visual Studio, .NET Core SDK and Node.js.
2. Make sure that the port 5000 is free.
3. Open the solution in Visual Studio.
4. Make sure that multiple startup projects is chosen otherwise set multiple start for Billing and Service projects.
5. Restore packages, build and run. 

### Running the tests

The project contains a suite of XUnit unit tests (in the Tests folder), when you open the solution you should be able to run them right away with any unit test explorer you have.

