# Software Guild Work
This repository contains my work completed at the Software Guild (April 2018 cohort). This cohort focused on the skills required to develop fullstack .NET applications.

# Battleship

A C# console adaptation of the board game BattleShip given an existing business logic layer.  Designed the UI and orchestrated the workflows required to complete the application, using a previous developer’s project plan and NUnit tests as documentation. Created additional tests to ensure the functionality of UI worked as intended, and used test-driven development to ensure new features worked as intended and did not break the functionality of existing features.

# HotelDB

A SQL database schema for a hotel reservation system. The creation script uses foreign key relationships and bridge tables to handle one-to-many and many-to-many relationships, allowing for the tracking of room types, guests, promotions, amenities, and billing details.

# SGBank

A C# console banking application which allows for checking balance, withdraws, and deposits. The application uses interfaces to allow for many different repository types and account types. Using N-tier architecture, the functionality of each layer of the application is separated out. The application uses dependency injection to allow for switching between an in-memory test repository and a file-on-disk repository. Workflow requests and responses are passed between layers using data transfer objects, ensuring the single responsibility principle is adhered to. Utilizing NUnit, test cases ensure that all functionality is working properly, and that changes to the codebase do not affect existing functionality.

# SGFlooring

A flooring order C# console application, utilizing layered architecture to implement create, read, update, and delete functionality. This application utilizes a data repository interface and dependency injection to switch between in-memory static data, or reading and writing data to files. Utilized a business logic validation layer to take in user request actions and process against in-repository data such as prices and state tax laws, using data transfer objects and a request/response pattern to ensure separation of concerns.  Utilized test-first development to ensure workflow functionality and direct development efforts to the most relevant tasks first.

# VendingMachine

A web application using HTML, Javascript, Bootstrap, and jQuery, which allows users to make transactions with a virtual vending machine. The application utilizes Javascript and the jQuery library to make AJAX calls to a REST service, facilitating a fast and responsive UI. 

# SG_Dealership

A fully-featured car dealership web application, using ASP.NET MVC design. Features include adding vehicles to inventory, adding new makes and models to the database, featuring specific vehicles on the home page, and search queries against the database. The application utilizes Entity Framework to create and interact with a SQL relational database containing site data. Using ASP.NET Identity, created a user management system whereby administrators can manage employees and their roles on the application. Created a Web API controller to accept AJAX calls from search pages on the site which allow the user to query the vehicle database with search terms. The API utilizes a parameterized ADO.NET SQL command to safely query the database and return vehicles which match the search parameters.
