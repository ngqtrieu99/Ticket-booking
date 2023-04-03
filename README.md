# 2023SEInternSS1-ticket-booking-api

## Project description

An web application that is used to book plane tickets from only 1 airline. Users are allowed to choose suitable tickets based on time, budget, and locations.

## Dependencies for running locally

- .NET 6 or higher
  - You can install .NET SDK from here: https://dotnet.microsoft.com/en-us/download/dotnet/6.0
- IDE (or code editor) to run project
  - Visual Studio 2022: https://visualstudio.microsoft.com/vs/
  - Visual Studio Code: https://code.visualstudio.com/

##### Note:

If you prefer VS Code then Visual Studio, you will need to read this guide to install .NET properly
https://code.visualstudio.com/docs/languages/dotnet

## Build instruction

1. Clone this repository

###### For Visual studio

2. Open **TicketBookingAPI.sln** with Visual Studio 2022
3. Tools -> Nuget Package Manager -> Package Manager Console
4. Enter: update-database
5. Build the project: CTRL + F5

###### For Visual Studio Code

2. Open the Vscode integrated terminal with CTRL + `
3. Moving to the data folder with the command line: cd ..\Ticket-booking\TicketBooking.Data
4. Enter: dotnet ef database update
5. Move to the API folder: cd ..\Ticket-booking\TicketBookingAPI
6. Build the project with dotnet run
