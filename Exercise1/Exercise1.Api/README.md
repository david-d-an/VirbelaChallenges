# Introduction
The project was written for .Net Core 3.1.
Please install the .Net Core SDK 3.1 to locally debug the projecgt.

# Build
Please follow below command in your command line to test the code locally.
```sh
> cd ./Execise1.Api
> dotnet restore
> dotnet build
> dotnet run
```

# Database
The targe database is located in Azure SQL Database. Please use the following information to access DB by your choice of a Database management tool.

* Server: virbelalisting.database.windows.net
* Port: 1433
* Initial Catalog: VirbelaListing
* User ID: appuser
* Password: virbela1234!
* Firewall: Open to all as I don't know Virbela's IP ranges at this time

# API Entrypoint

## Local
The API runs off of port 15000 over TLS by default (i.e., https://localhost:15000/api). Some of the most popular HTTP request addresses are as follows:

* User Management
    * Registration: https://localhost:15000/api/User/Register (POST)
    * Login: https://localhost:15000/api/User/Login (POST)
* Listing Management
    * View All: https://localhost:15000/api/User/Listing (GET)
    * Find: https://localhost:15000/api/User/Listing/{id} (GET)
    * Update: https://localhost:15000/api/User/Listing/{id} (PUT)
    * Create: https://localhost:15000/api/User/Listing (POST)
    * Delete: https://localhost:15000/api/User/Listing/1 (DELETE)

## Azure App Service
The API is deployed on Azure on https://execise1api6921.scm.azurewebsites.net. HTTP endpoints are synonymous to the local address except the domain address. Please see below for details:

* User Management
    * Registration: https://execise1api6921.scm.azurewebsites.net/api/User/Register (POST)
    * Login: https://execise1api6921.scm.azurewebsites.net/api/User/Login (POST)
* Listing Management
    * View All: https://execise1api6921.scm.azurewebsites.net/api/User/Listing (GET)
    * Find: https://execise1api6921.scm.azurewebsites.net/api/User/Listing/{id} (GET)
    * Update: https://execise1api6921.scm.azurewebsites.net/api/User/Listing/{id} (PUT)
    * Create: https://execise1api6921.scm.azurewebsites.net/api/User/Listing (POST)
    * Delete: https://execise1api6921.scm.azurewebsites.net0/api/User/Listing/1 (DELETE)


# Functionalities

## Initial Data Set
The database was seeded with initial data to enable basic testing. Please use the credentials in  to get started:

* ListingUsers
```
| Id | UserId             | Password | Region |
|----|--------------------|----------|--------|
| 1  | jsmith@contoso.com | test     |   1    |
| 2  | jdoe@contoso.com   | test     |   3    |
| 3  | lmessi@contoso.com | test     |   4    |
```

* Regions
```
| Id | Name |
|----|------|
| 1  |  A   |
| 2  |  B   |
| 3  |  C   |
| 4  |  D   |
| 5  |  E   |
```

* Listings
```
| Id  | Title     | Description | Price  | Creator_Id | Created_Date        |
|-----|-----------|-------------|--------|--------- --|---------------------| 
|  1  | Listing A | ...         |  12.34 |     1      | 2021-03-21 09:15:22 |
|  2  | Listing B | ...         |  22.34 |     1      | 2021-04-01 13:46:52 |
|  3  | Listing C | ...         |  32.45 |     2      | 2021-03-11 15:42:59 |
|  4  | Listing D | ...         | 455.56 |     2      | 2021-02-08 14:21:46 |
|  5  | Listing E | ...         | 556.99 |     3      | 2021-02-27 18:19:19 |
```

## User Management
### Registration
Anyone can create an account by sending a JSON body attached to HTTP Post request to ***{EndPoint}/register*** in the following format:
```json
{
    "Id": 0,                    // Int: value insignificant
    "UserId": "jsmith",         // string: required
    "FirstName": "John",        // string: can be null
    "LastName": "Smith",        // string: can be null
    "Password": "test",         // string: required
    "RegionId": 1,              // Int: required, foreign key to Region.Id
}
```

### Login
Most actions of the API app requires acees token, which can be obtained only after successful login. To login, send an HTTP post requt with a JSON body attache to ***{EndPoint}/login*** in the following format:
```json
{
    "userid": "jsmith",     // string: required
    "password": "test"      // string: required
}
```

## Listing Management
### View All Listings

### CRUD on Listing

## Regional Listing Management
### View All Listings

### CRUD on Listing


### 

