README v1.0 / 29 DECEMBER 2017
=

# Project name
Dellasanta_Test

## Introduction
Dellasanta_Test is an MVC web application to parse a docs document.



## Usage


### Requirements and dependencies
IMPORTANT:
**************************************
The additional packages have been removed to send a slimmer zip over the wire, as are the bin and obj folder. 
If you have the "Enable restore Nuget packages" option enabled on VS 2017 upon building the solution the packages will be automatically downloaded from Nuget.org 
(please check the Package Manager console for the actual Package source and select Nget.org). 
If you don't have the option enabled you will have to enable it:
https://docs.microsoft.com/en-us/nuget/consume-packages/package-restore
https://docs.microsoft.com/en-us/nuget/consume-packages/package-restore#enabling-and-disabling-package-restore
**************************************




### Installation

IMPORTANT:
**************************************
The project use Entity Framework Migrations; to generate the database and seed it with seminal data follow these steps in VS2017:

-Go to the Web.config of the Dellasanta.Web project and modify the connection string to point to an existing Database instance 
and choose a DataBase name which will be created, see next section.

-Go to Package Manager Console and select Default project = "Libraries\DellaSanta.DataLayer"

-Inside Package Manager Console execute the following command from the shell "update-database -verbose"

**************************************


### Configuration
----------------------------------------------------------------------------------

The software needs the following configuration keys to be set in Web.config file:
<connectionStrings>
    <add name="DefaultConnection" connectionString="Data Source=[DATABASE_INSTANCE];Initial Catalog=[DATABASE_NAME];Integrated Security=SSPI;MultipleActiveResultSets=True;" providerName="System.Data.SqlClient" />
</connectionStrings>

Example:
<connectionStrings>
    <add name="DefaultConnection" connectionString="Data Source=localhost\SQLEXPRESS01;Initial Catalog=DellaSantaSchool;Integrated Security=SSPI;MultipleActiveResultSets=True;" providerName="System.Data.SqlClient" />
</connectionStrings>

----------------------------------------------------------------------------------


## Test Configuration
To test this software use unit test and integration tests in Tests folder in this solution.

## Contributing

## API Reference


## Links
