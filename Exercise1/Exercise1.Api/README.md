# Applicant Information
* Name: David An
* Email: david.d.an@outlook.com
* Phone: (301) 351-0655
* Recruiter in charge: Ari Alcaraz (ari.alcaraz@virbela.com)

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

# API Endpoint
In this article, the common HTTPS address to access the API services will be called **Endpoint** . The endpoint typically is in  a format similar to https://contoso.com/api. To activate a particular API service, a request must be sent to the endpoint with speicfic request type and a body if necessary. Please see the next chapter *Functionalities* for request details.

Currently, the app is deployed on Azure App Service to provide a staging environment. Plase consult next two section to find API Endpoints for *Developemnt* and *Staging* environments,.

## Local (Development)
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

## Azure App Service (Staging)
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
This chapter will explain the types of services and usage instructions.
## User Management
### Registration
Anyone can create an account by sending a JSON body attached to HTTP POST request to ***{EndPoint}/register*** in the following format:
```json
{
    "Id": 0,                    // Int: requiredd as placeholder, value insignificant
    "UserId": "jsmith",         // string: required
    "FirstName": "John",        // string: can be null
    "LastName": "Smith",        // string: can be null
    "Password": "test",         // string: required
    "RegionId": 1,              // Int: required, foreign key to Region.Id
}
```

### Login
Most actions of the API app requires acees token, which can be obtained only after successful login. To login, send an HTTP POST request with a JSON body attache to ***{EndPoint}/login*** in the following format:
```json
{
    "userid": "jsmith",     // string: required
    "password": "test"      // string: required
}
```

## Listing Management

### View All Listings
User can view all listing in his/her region whether the listings were created by him/her or not. The request can be sent as HTTP GET to ***{EndPoint}/Listing***. 

### CRUD on Listing

## Regional Listing Management
### View All Listings

### CRUD on Listing


### 




<br>
<br>

# Initial Data Set
The database was seeded with initial data to enable basic testing. Please use the credentials in  to get started:

<br>

### 1. Listinguser

| Id  | UserId |  Email              | Password   | Region |
| :-: | ------ | ------------------- | :--------: | :----: |
|  1  | jsmith | jsmith@contoso.com  | test       |   1    |
|  2  | jdoe   | jdoe@contoso.com    | test       |   3    |
|  3  | lmessi | lmessi@contoso.com  | test       |   4    |

<br>

* ID: Primary Key, Integer, Autoincrement by 1 starting from 1.
* UserId: String, Unique

<br>

### 2. Region

| Id  | Name |
| :-: | :--: |
| 1   |  A   |
| 2   |  B   |
| 3   |  C   |
| 4   |  D   |
| 5   |  E   |

<br>

* ID: Primary Key, Integer, Autoincrement by 1 starting from 1.
* Name: String

<br>

### 3. Listing

| Id  | Title     |        Description          | Price  | Creator_Id |    Created_Date     |
| :-: |-----------|-----------------------------| -----: | :--------: |---------------------| 
|  1  | Listing A | Descripion for Listing A... |  12.34 |     1      | 2021-03-21 09:15:22 |
|  2  | Listing B | Descripion for Listing B... |  22.34 |     1      | 2021-04-01 13:46:52 |
|  3  | Listing C | Descripion for Listing C... |  32.45 |     2      | 2021-03-11 15:42:59 |
|  4  | Listing D | Descripion for Listing D... | 455.56 |     2      | 2021-02-08 14:21:46 |
|  5  | Listing E | Descripion for Listing E... | 556.99 |     3      | 2021-02-27 18:19:19 |

<br>

* ID: Primary Key, Integer, Autoincrement by 1 starting from 1.
* Price: Decimal
* Creator_Id: Foreign Key to *Listinguser.Id*.
* Created_Date: DateTime

<br>


