> # Applicant Information
* Name: David An
* Email: david.d.an@outlook.com
* Phone: (301) 351-0655
* Recruiter in charge: Ari Alcaraz (ari.alcaraz@virbela.com)

<br><br>

> # Introduction
The title of the project is **Excercise1** which provides **API Endpoints** that provide several different services:
* Authentication / User Registration
* Create / View / Update Ad-Listings

The project was written for .Net Core 3.1.
Please install the .Net Core SDK 3.1 to locally debug the project.

<br><br>

> # Project Structure
The entire solution is bound under Exercise1.sln and there are 7 projects total
```
  Excercise1.sln
    |
    |- Exercise1.Api   (Main API application)
    |
    |- Exercise1.Api.Test   (Unit test for Exercise1.Api)
    |
    |- Exercise1.Common   (Collection of small utility functions/classes)
    |
    |- Exercise1.Data   (Collection of models for object, repository, and unit of work)
    |
    |- Exercise1.DataAccess   (Implementation of Exercise1.Data)
    |
    |- Exercise1.DataAccess.Test   (Unit test for Exercise1.DataAccess)
    |
    |- Exercise1.DbScaffold   (Database scaffold to create object-models / context and deploy DB with seed data)
```

<br><br>

> # Build and Run
Please follow the below command in your command line to run the API locally.
```sh
> cd {Exercise1 Root}/Execise1.Api
> dotnet restore
> dotnet build
> dotnet run
```
Currently, the application is configured to run on https://localhost:15000. <br>
If the port is already occupied, declare another point inside **launchSetings.json**.
<br><br><br>

> # Functionalities in API Application
This chapter will explain the types of services and usage instructions.
The terminology **ApiRoot** is used in the chapter to denote the root location of all of the API services.<br>
Please see the chapter **API Endpoints** to get the details

## Authentication
Most actions of the API app requires acees token, which can be obtained only after successful login. To login, send an HTTP POST request with a JSON body attache to ***{ApiRoot}/login*** in the following format:
```json
{
    "userid": "jsmith",     // string: required
    "password": "test"      // string: required
}
```
Upon successufl authentication, the server will respond with a response body in the following format:
```json
{
    "id": 1,
    "firstname": "John",
    "lastname": "Smith",
    "userid": "jsmith",
    "email": "jsmith@contoso.com",
    "regionId": 1,
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9eyJJZCI6IjEiLCJVc2VyaWQiOiJqc21pdGg..."
}
```

The *token* is a string type, a self-contained Jason Web Token, and must be attached if the API service requires authentication to prevent **HTTP 401 Unauthorized** response.<br>
To verify the contents of the token, you can use one of the online tools such as https://jwt.io/.

<br>

## User Management
### Registration
Anyone can create an account by sending a JSON body attached to HTTP POST request to ***{ApiRoot}/register*** in the following format:
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
### User Information Update
The feature has not been implemented yet.

### Delte/Deactivate User
The feature has not been implemented yet.

<br>

## Listing Management
The API supports basic CRUD operations on listings:
1. Get(Parameters): returns collection of listings that math Parameters 
1. Get(Id): returns a listing that matches id value
1. Put(Id, Listing-Data): updates a listing that matches id value
1. Post(Listing-Data): creates a listing

### 1. Find All Listings in User's Region
Users can view all listings in his/her region whether the listings were created by him/her or not.<br>

* Request Type: GET
* Service Url: **{ApiRoot}**/Listing 
* Input Parameters: (included in the query string)
    * pageNum: int
    * pageSize: int
    * title: string
    * description: string
    * price: decimal
    * regionName: string
> **{ApiRoot}**/Listing?pageNum=1&pageSize=5&title=Listing%20A&description=Listing%20Description&price=24.99&regionName=A

* Output: List of Region_Listing that has the following fields:
    * Id: Int, Primary key of Listing
    * Title: String, Title of Listing
    * Description: String, Description of Listing
    * Price: Decimal, Price of Listing
    * CreatorId: Int, Primary key of user who created Listing
    * CreatedDate: DateTime, Date when Listing was created
    * RegionId: Int, Id of Region with which user is associated.
    * RegionName: String, name of Region with which user is associated.

```json
    [
        {
            "id": 1,
            "title": "Listing A",
            "description": "Description for Listing A. Details for Listing A is provided here.",
            "price": 12.3400,
            "creatorId": 1,
            "createdDate": "2021-03-21T09:15:22",
            "regionId": 1,
            "regionName": "A"
        },
        {
            "id": 6,
            "title": "Listing F",
            "description": "Description for Listing F. Details for Listing F is provided here.",
            "price": 489.9900,
            "creatorId": 4,
            "createdDate": "2021-04-01T19:43:58",
            "regionId": 1,
            "regionName": "A"
        }
    ]
```

### 2. Find One Listing
Users can view any listing in his/her region whether the listings were created by him/her or not.<br>

* Request Type: GET
* Service Url: **{ApiRoot}**/Listing/{id}
* Input Parameters: None
> **{ApiRoot}**/Listing/8

* Output: List of Region_Listing that has the following fields:
    * Id: Int, Primary key of Listing
    * Title: String, Title of Listing
    * Description: String, Description of Listing
    * Price: Decimal, Price of Listing
    * CreatorId: Int, Primary key of user who created Listing
    * CreatedDate: DateTime, Date when Listing was created
    * RegionId: Int, Id of Region with which user is associated.
    * RegionName: String, name of Region with which user is associated.
```json
    {
        "id": 8,
        "title": "New Listing",
        "description": "Description for New Listing",
        "price": 27.9900,
        "creatorId": 1,
        "createdDate": "2021-04-05T03:59:18.903",
        "regionId": 1,
        "regionName": "A"
    }
```
### 3. Edit Listing
Users can edit any listing created by himself/herself.<br>

* Request Type: PUT
* Service Url: **{ApiRoot}**/Listing/{id}
* Input Parameters: (attached as body)
    * Title: String, Title of Listing
    * Description: String, Description of Listing
    * Price: Decimal, Price of Listing
> **{ApiRoot}**/Listing/26
```json
    {
        "Title": "Listing B",
        "Description": "Description for Listing B",
        "Price": 27.99
    }
```

* Output: The updated Listing:
    * Id: Int, Primary key of Listing
    * Title: String, Title of Listing
    * Description: String, Description of Listing
    * Price: Decimal, Price of Listing
    * CreatorId: Int, Primary key of user who created Listing
    * CreatedDate: DateTime, Date when Listing was created
```json
    {
        "id": 26,
        "title": "Listing B",
        "description": "Description for Listing B",
        "price": 27.99,
        "creatorId": 4,
        "createdDate": "2021-03-02T14:47:42.903",
        "creator": null     // Insignificant
    }
```

### 4. Create Listing
Users can create any listing.<br>

* Request Type: POST
* Service Url: **{ApiRoot}**/Listing
* Input Parameters: (attached as body)
    * Title: String, Title of Listing
    * Description: String, Description of Listing
    * Price: Decimal, Price of Listing
> **{ApiRoot}**/Listing/
```json
    {
        "Title": "New Listing",
        "Description": "Description for New Listing",
        "Price": 27.99,
    }
```

* Output: The created Listing:
    * Id: Int, Primary key of Listing, Database will assign a new Id upon creation.
    * Title: String, Title of Listing
    * Description: String, Description of Listing
    * Price: Decimal, Price of Listing
    * CreatorId: Int, Primary key of user who created Listing
    * CreatedDate: DateTime, Date when Listing was created
```json
    {
        "id": 26,
        "title": "New Listing",
        "description": "Description for New Listing",
        "price": 27.99,
        "creatorId": 4,
        "createdDate": "2021-04-03T03:59:18.901",    // Time of record creation
        "creator": null     // Insignificant
    }
```

### 5. Delete Listing
Users can delete any listing created by himself/herself.<br>

* Request Type: DELETE
* Service Url: **{ApiRoot}**/Listing/{id}
* Input Parameters: None
> **{ApiRoot}**/Listing/1

* Output: The deleted Listing:
    * Id: Int, Primary key of Listing
    * Title: String, Title of Listing
    * Description: String, Description of Listing
    * Price: Decimal, Price of Listing
    * CreatorId: Int, Primary key of user who created Listing
    * CreatedDate: DateTime, Date when Listing was created
```json
    {
        "id": 1,
        "title": "Listing A",
        "description": "Descripion for Listing A. Details for Listing A is provided here.",
        "price": 12.3400,
        "creatorId": 1,
        "createdDate": "2021-03-21T09:15:22",
        "creator": null     // Insignificant
    }
```
* Note: If the access token's User Id is different from the CreatorId of the listing subject to deletion, the server denies the request and returns HTTP 401 Unauthorized response. 

<br><br>

> # API Endpoints
In this article, the common HTTPS address to access the API services will be called **Endpoint** and the url to the service domain is **ApiRoot**. The endpoint typically is in  a format similar to https://contoso.com/api/ResourceName. To activate a particular API service, a request of a specific request type (GET/PUT/POST/DELETE, etc.) must be sent to the correct endpoint with a request body or a query string if necessary. Please see the next chapter *Functionalities* for request details.

Currently, the app is deployed on Azure App Service to provide a staging environment. Please consult the next two sections to find **API Endpoints** for *Development* and *Staging* environments.

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
The API is deployed on Azure on https://execise1api6921.scm.azurewebsites.net. HTTP endpoints are similar to those of the local address except for the domain address. Please see below for details:

* User Management
    * Registration: https://execise1api6921.azurewebsites.net/api/User/Register (POST)
    * Login: https://execise1api6921.azurewebsites.net/api/User/Login (POST)
* Listing Management
    * View All: https://execise1api6921.azurewebsites.net/api/User/Listing (GET)
    * Find: https://execise1api6921.azurewebsites.net/api/User/Listing/{id} (GET)
    * Update: https://execise1api6921.azurewebsites.net/api/User/Listing/{id} (PUT)
    * Create: https://execise1api6921.azurewebsites.net/api/User/Listing (POST)
    * Delete: https://execise1api6921.azurewebsites.net/api/User/Listing/1 (DELETE)

<br><br>

> # Unit Test
Please follow below command in your command line to test the code locally.
```sh
> cd *{Exercise1 Root}*/Execise1.Api.Test
> dotnet restore
> dotnet build
> dotnet test
> cd {Exercise1 Root}/Execise1.DataAccess.Test
> dotnet restore
> dotnet build
> dotnet test
```
<br><br>

> # Database
The targe database is located in Azure SQL Database. Please use the following information to access DB by your choice of a Database management tool such as SQL Server Management Studio.<br>
The firewall rule is set to open to the whole IP range as I don't know Virbela's IP ranges at this time.

* Staging:
The database is for Vribela personnel to test the API functions
    * Server: virbelalisting.database.windows.net
    * Port: 1433
    * Initial Catalog: VirbelaListing
    * User ID: appuser
    * Password: virbela1234!

* Development:
The database is for development to build API functions
    * Server: virbelalisting.database.windows.net
    * Port: 1433
    * Initial Catalog: VirbelaListingDev
    * User ID: appuser
    * Password: virbela1234!

<br><br>


> # Initial Data Set
The database was seeded with initial data to enable basic testing. 
The staging database will have the exact dataset described below for Virbela.
Please use the credentials in the previous section to access the database via your DB management tool such as SQL Server Management Studio.

<br>

### 1. Listinguser

| Id  |  UserId  |  Email                | Password   | Region |
| :-: | :------- | :-------------------- | :--------: | :----: |
|  1  | jsmith   | jsmith@contoso.com    | test       |   1    |
|  2  | jdoe     | jdoe@contoso.com      | test       |   3    |
|  3  | lmessi   | lmessi@contoso.com    | test       |   4    |
|  4  | maradona | maradona@contoso.com  | test       |   1    |

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
* Name: String, Unique

<br>

### 3. Listing

| Id  | Title     |        Description          | Price  | Creator_Id |    Created_Date     |
| :-: | :-------- | :-------------------------- | -----: | :--------: | :------------------ | 
|  1  | Listing A | Descripion for Listing A... |  12.34 |     1      | 2021-03-21 09:15:22 |
|  2  | Listing B | Descripion for Listing B... |  22.34 |     1      | 2021-04-01 13:46:52 |
|  3  | Listing C | Descripion for Listing C... |  32.45 |     2      | 2021-03-11 15:42:59 |
|  4  | Listing D | Descripion for Listing D... | 455.56 |     2      | 2021-02-08 14:21:46 |
|  5  | Listing E | Descripion for Listing E... | 556.99 |     3      | 2021-02-27 18:19:19 |
|  6  | Listing F | Descripion for Listing F... | 498.99 |     4      | 2021-04-01 19:43:58 |
|  7  | Listing G | Descripion for Listing G... | 124.99 |     4      | 2021-03-31 08:22:38 |

<br>

* ID: Primary Key, Integer, Autoincrement by 1 starting from 1.
* Price: Decimal
* Creator_Id: Foreign Key to *Listinguser.Id*.
* Created_Date: DateTime

<br><br>

> # Deployment
The API application is currently deployed on Azure as Staging for testing.
See below for server details:

* Application: <br>
  * Url: https://execise1api6921.azurewebsites.net <br>
  * OS: Debian Version 10 <br>
* Database: <br>
  * Type: SQL Database <br>
  * Server: virbelalisting.database.windows.net <br>
  * Database: VirbelaListing <br>

<br>

## Application Deployment
The application is deployed by GitHub action. The script is located at:
```
{Exercise1 Root}/.github/workflows/staging_execise1api6921.yml <br>
```
The script will be activated to deploy the application onto Azure App Service if the code is pushed from IDE or Pull Request.

<br>

## Database Deployment
Database structure is deployed by **Scaffolding service of Entity Framework Core**. <br>
Database migration instructions are provided at: <br>

```
{Exercise1 Root}/Exercise1.DbScaffold/EF DB Migration Instructions.txt <br>
```

Currently, **Exercise1.DbScaffold** project is set up to seed the database with a basic dataset.
The contents of the data seeding were explained in **Initial Data Set** chapter.
Although the seeding service is convenient, the seeding should be used only during the initial set-up of the database and is not meant to migrate a large set of data.

<br><br>

> # Logging
Application keeps the logs in a folder located at 

```
{Exercise1 Root}/Exercise1.Api/Logs <br>
```

Log files are created daily and files are named by the following convention:

* Information: inf_*YYYYMMDD*.log <br>
    * Captures inportant information to help maintenace and troubleshooting
    * Configurations and environmental variables are logged
* Error:  err_*YYYYMMDD*.log
    * Exceptions
    * Stacktrace
