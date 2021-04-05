# Exercise 1 #

For this exercise you will create a REST API that provides data to support a classifieds application.

As you progress through the steps, feel free to add comments to the code about *why* you choose to do things a certain way. Add comments if you felt like there's a better, but more time intensive way to implement specific functionality. It's OK to be more verbose in your comments than typical, to give us a better idea of your thoughts when writing the code.

### What you need ###

* IDE of your choice
	> VS Code <br>
* Git
	> Github: https://github.com/dong82/VirbelaChallenges <br>
	> Branch: main <br>
* Some chosen backend language / framework
	> C# <br>
* Some chosen local data store
	> I used Azure SQL Database, so that deployed app has data access. <br>

	Please use below Staging database for your testing.
	<br>
	> * Environment: Staging <br>
	> * Server: virbelalisting.database.windows.net <br>
	> * Port: 1433 <br>
	> * Initial Catalog: VirbelaListing <br>
	> * User ID: appuser <br>
	> * Password: virbela1234! <br>

	Below is Development database I am using for development.
	<br>
	> * Environment: Development <br>
	> * Server: virbelalisting.database.windows.net <br>
	> * Port: 1433 <br>
	> * Initial Catalog: VirbelaListingDev <br>
	> * User ID: appuser <br>
	> * Password: virbela1234! <br>

<br>

## Instructions ##

### Phase 1 - Setup ###

 1. Clone this repository to your local machine
 1. Create the basic structure needed for your API with your chosen framework
 1. Add a README.md in this exercise folder with the basic requirements and steps to run the project locally

### Phase 2 - Main Implementation ###

Implement a RESTful API to support a classifieds application that satisfies the following requirements:

 * Ability to create (essentially 'register') a User object using POST call. User must have email/password.
	> Register(POST) implemented

 * Ability to 'login' using email/password combo in POST call. Should return some kind of authorization token to be re-used on subsequent calls.
	> Login(POST) implemented

 * Ability to perform all CRUD operations for a Listing object. The Listing object represents a 'for sale' classified ad. Include minimum of Title, Description, Price fields.
 	* A valid authorization token must be provided for all Listing operations
		> JWT is returned after login and required for all listing operations
 	* A User can create many Listings
	 	> Listing (POST) is implemented
 	* Only the User who created a Listing can update or delete a Listing
	 	> Listing (PUT, DELETE) are implemented and allow listing owners only
 	* An authenticated User can retrieve all Listings
	 	> Listing (GET) is implemented and returns listings in own region.

### Phase 3 - Add Region based listings ###

We want to alter our very general classifieds API to limit Listings to Users based on an associated Region. Please make changes to satisfy the following requirements:

 * Each User is associated with a single Region. A Region has many Users.
	> * DB and API were implementd to meet the requirements <br>
	> * User registration requires region ID <br>
	> * Currently, there is no user edit feature <br>
 * When a User requests all Listings, they only receive Listings created by Users in the same Region as themselves.
	> Implemented <br>

### Phase 4 - Stretch Goals ###

Please implement any of the following stretch goals. They are in no particular order.

 * Allow paging and/or filtering of Listings
	> * Pagination is implemented by *Skip()* and *Take()* of *System.Linq* <br>
	> * I normally leave pagination to the front-end JacaScript codes such as Angular or Jquery <br>
	> * I prefer less chatty Front-End / API interaction.
	> 	* Use filtering parameters and smaller data model to reduce data transmission to front-end <br>
	> 	* Once bulk data is loaded to JS pagination, it's much faster to navigate afterwards <br>

 * Add some type of self-documenting UI such as Swagger
	> * Development: https://localhost:15000/swagger <br>
	> * staging: https://execise1api6921.azurewebsites.net/swagger <br>

 * Create Unit Tests (note and include in the commit with your tests any bugs/improvements you make due to Unit Test development)
	> * Created 2 unit test projects for API layer and DataAccess layer using **xUnit** and **Moq** <br>
	> * DataAccess layer has simple validation to make sure DB conection is working <br>
	> * DataAccess layer is meant to be used only for the early stage of development <br>
	> * API layer two classes of tests: **ListingControllerTest** & **UserControllerTest** <br>
	> * **ListingControllerTest** has 7 types of tests to cover basic CRUD cases <br>
	> * **UserControllerTest** has 6 types of tests to cover **Login** and **Registration** for both success & failure <br>
	> * My Unit Test is far from complete but didn't have time to add more test cases and had to stop after basic checks <br>

## Questions ##

 1. How can your implementation be optimized?
	> * Database replication <br>
	> * Denomalizing database as necessary to address frequent joins. <br>
	> * Currently, the database (3 tables) is fully normalized to minimize redundancy. <br>
	> * However, Region is included in Listing almost all the time and it should be inluded in Listing table to reduce join operations. <br>
	> * Document database like NoSql would nice as each listing is a document with very few relations <br>
	> * Plan for scaling using cloud servers. <br>
	> 	* The API app and database are deployed on AZ App Service and is ready for scaling <br>
	> 	* Strategy to balance performance/cost is yet to be set up <br>	
	> * Use CDN and caches to increase response to frequent repeated request <br>

 1. How much time did you spend on your implementation?
	> * General setup: 4 hours (GitHub, Azure DB, VS Code) <br>
	> * API Implementaion: <br>
	> 	* User Contorller: 14 hours (Token exchange was a challenge) <br>
	>	* Listing Controller: 24 houurs (Not much of challenge, jsut work) <br>
	> * Unit Testing: 10 hours <br>
	> * Deployment configuration: 2 hours (Smooth overall. GitHub and Azure made it easy) <br>
	> * Documentation: 4 hours <br>
	> * Swagger: 2 hours <br>

 1. What was most challenging for you?
	> * Creating custom Token base authentication. <br>
	>	* I used Identity Server 4 previously and didn't need to implement token generation/verification. <br>
	> 	* I learned good stuff during this challenge. <br>
	> * Unit testing for DAL layer. Unit testing on DAL layer was not so possible for PUT and POST in/out of database. <br>
	> * About 30 more unit tests need to be added to address all the edge cases but I ran out of time. <br>
	> 	* OnAuthorization mothod of custom Auth Attribute is not firing during Unit Testing <br>
	> 	* I had to create two mocks to simulate attrubute actions that run separately from API Endpoint invocation. <br>

## Next Steps ##

* Confirm you've addressed the functional goals
	> * I believe that all functionalities were implemented.
* Answer the questions above by adding them to this file
	> * Done.
* Make sure your README.md is up to date with setup and run instructions
	> * README.md is up to date.
* Ensure you've followed the sharing instructions in the main [README](../README.md)
	> * Roger.
