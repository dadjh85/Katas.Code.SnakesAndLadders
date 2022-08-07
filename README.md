# Katas SnakesAndLadders Description
Resolution of the technical test at the following link: [Katas.Code.SnakesAndLadders](https://github.com/VoxelGroup/Katas.Code.SnakesAndLadders/ "Katas.Code.SnakesAndLadders")

# Description of the proposed solution

## Solution architecture ##

For the development of the test I have developed an api with a very simple data model to facilitate possible updates of the solution. The API uses the DDD pattern and Mediator making use of the MediaTr library. On the other hand, I have developed the console application requested in the technical test that makes calls to the api to be able to run the game normally.

Aspects to take into account: 


1. Due to the estimated time of the technical test the API has a very basic structure and does not perform full validations, with more time more technical details of the API can be completed in more depth. 
2. The aim was to propose a clean architecture and maintainable, decoupled code.
3. Because I have decided to propose a more complete architecture I have not done testing due to lack of time (it was not specified in the statement of the same, however as I always like to do unit and integration tests but this time I have not had time to do them). However in my github I have some repositories where I do testing on the code made, I don't have many repositories but to get an idea it's good.
4. Because this is a test done in about two hours and it is not enough time to show great knowledge in my github there are some repositories where you can extend my knowledge and my way of programming in case you consider it appropriate to examine it after reviewing the technical test.

## Instructions for running the technical test ##

1. First of all it is necessary to have a local database or a SqlServer database server as I have used Entity framework Core 6 with SqlServer and also have the .NET 6 sdk installed.
2. in the appsetting.Development.json of the API you have to put the database connection string the path is: /src/API/appsettings.Development.json as it is a test if you have a local database you can put a user with database creation permissions so when the migrations are launched you can create the database of the same (this point could be automated in the api start but really as it depends on the database user I preferred to leave it independent).
3. Once the connection string has been placed in the above mentioned file, the project migrations have to be launched to the database, for which two commands have to be launched:
    Update-Database -context GameContext
    Update-Database -context BoardContext
there are two database contexts as I have applied the Bounded Context pattern in the use of entity framework, it is about two contexts on the same database.Keep in mind that if the database user in the connection string does not have permissions to create a database, this process has to be done manually, so you would have to create a database and assign permissions to that user.
4. Finally at the time of executing the technical test from visual studio you have to take into account that you have to put two startup projects the API and the console application whose name is the following: SnakesAndLaddersGame both projects are inside the virtual folder of visual studio App.

**If you have any questions or problems about the test execution or the test code, please contact me.**

