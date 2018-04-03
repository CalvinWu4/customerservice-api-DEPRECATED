# Customer Service API

This is the main project repo for the Customer service API. Main endpoint for the internal and external customer service REST API

## Getting Started

This project is built on top of the Microsoft stack. Here is what you will need

### Prerequisites

- ASP.NET Core 2
- MySQL
- Visual Studio (not required but highly useful)
- NuGet

### Installing

Getting this project is very easy.

1. Clone project locally `git clone https://github.com/RIT-SWEN-343-201705-KennUWare/customerservice-api`
2. Double click in the `CustomerServiceAPI.sln`. This will open the project in Visual Studio.
3. In Visual studio duplicate the `appsettings.Production.json` and rename to `appsettings.Development.json`
4. Place your MySQL connection string in the `appsettings.Development.json` as shown in the file
5. Hit the `run + build` button in Visual Studio & Profit 🎁


## Running the tests
Press CTRL+R ,A to run the tests.


## Deployment

Any changes to master will be automatically reflected in the production environment available through:
`https://api-customerservice.azurewebsites.net/api/`

For accessing the web application, please visit:
`https://web-customersupport.azurewebsites.net/`
