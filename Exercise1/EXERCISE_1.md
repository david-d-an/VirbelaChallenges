# Exercise 1 #

For this exercise you will create a REST API that provides data to support a classifieds application.

As you progress through the steps, feel free to add comments to the code about *why* you choose to do things a certain way. Add comments if you felt like there's a better, but more time intensive way to implement specific functionality. It's OK to be more verbose in your comments than typical, to give us a better idea of your thoughts when writing the code.

### What you need ###

* IDE of your choice
	<br>
	VS Code
* Git
	<br>
	Github: https://github.com/dong82/VirbelaChallenges
* Some chosen backend language / framework
	<br>
	C#
* Some chosen local data store
	<br>
	I used Azure SQL Database, so that deployed app has data access.
	<br>
	Azure SQL Database: 
	<br>
	<br>

	Please use below Staging database for your testing.
	<br>
	* Server: virbelalisting.database.windows.net
	* Port: 1433
	* Initial Catalog: VirbelaListing
	* User ID: appuser
	* Password: virbela1234!

	Below is Development database I am using for development.
	<br>
	* Server: virbelalisting.database.windows.net
	* Port: 1433
	* Initial Catalog: VirbelaListingDev
	* User ID: appuser
	* Password: virbela1234!

<br>

## Instructions ##

### Phase 1 - Setup ###

 1. Clone this repository to your local machine
 1. Create the basic structure needed for your API with your chosen framework
 1. Add a README.md in this exercise folder with the basic requirements and steps to run the project locally

### Phase 2 - Main Implementation ###

Implement a RESTful API to support a classifieds application that satisfies the following requirements:

 * Ability to create (essentially 'register') a User object using POST call. User must have email/password.
 * Ability to 'login' using email/password combo in POST call. Should return some kind of authorization token to be re-used on subsequent calls.
 * Ability to perform all CRUD operations for a Listing object. The Listing object represents a 'for sale' classified ad. Include minimum of Title, Description, Price fields.
 	* A valid authorization token must be provided for all Listing operations
 	* A User can create many Listings
 	* Only the User who created a Listing can update or delete a Listing
 	* An authenticated User can retrieve all Listings

### Phase 3 - Add Region based listings ###

We want to alter our very general classifieds API to limit Listings to Users based on an associated Region. Please make changes to satisfy the following requirements:

 * Each User is associated with a single Region. A Region has many Users.
 * When a User requests all Listings, they only receive Listings created by Users in the same Region as themselves.

### Phase 4 - Stretch Goals ###

Please implement any of the following stretch goals. They are in no particular order.

 * Allow paging and/or filtering of Listings
 * Add some type of self-documenting UI such as Swagger
 * Create Unit Tests (note and include in the commit with your tests any bugs/improvements you make due to Unit Test development)

## Questions ##

 1. How can your implementation be optimized?
	* Database replication
	* Denomalizing database as necessary to address frequent joins. Currently, I fully normalized three tables to minimize redundancy. However, Region is included in Listing almost all the time and it should be inluded in Listing table to reduce join operations.
	* Use document database as each listing is a document with minimum relations
	* Plan for scaling using cloud servers. The API app is deployed on AZ App Service and is ready for scaling. I just need to set up a strategy balance performance/cost.	
	* Use CDN and caches

 1. How much time did you spend on your implementation?
	* General setup: 6 (GitHub, Azure DB, VS Code)
	* Functionality work: 24 hours
	* Unit Testing: 20 hours
	* Deployment configuration: 2 hours
	* Documentation: 2hours

 1. What was most challenging for you?
	* Creating custom Token base authentication. I used Identity Server previously and didn't need to disect token generation/verification. I learned good stuff during this challenge. 
	* Unit testing for DAL layer. Unit testing on DAL layer was not so possible for PUT and POST in/out of database.
	* There are about 30 mroe unit testing to address all the edge cases but I ran out of time.

## Next Steps ##

* Confirm you've addressed the functional goals
	- I believe that all functionalities were implemented.
* Answer the questions above by adding them to this file
	- Done.
* Make sure your README.md is up to date with setup and run instructions
	- README.md is up to date.
* Ensure you've followed the sharing instructions in the main [README](../README.md)
	- Roger.
