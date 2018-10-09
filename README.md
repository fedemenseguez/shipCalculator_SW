# shipCalculator_SW
This program consume an WebAPI (https://swapi.co/) that provides you a list of starships and calculate how many stops, do they need to travel a distance in mega lights

## What it does?
You need to input an integer value that will be the mega lights you want to travel. If the value is not an integer, the program will return an error and ask you to input a new value.


## Required 
 - As it was developed with .NET Framework 4.5. You will need to get it installed. You can download it from this link:                         https://www.microsoft.com/es-ar/download/details.aspx?id=30653
 - You also need to have internet connection to run the application.

## Results
  After you input a value and press Enter. Application will show you this message : "Getting info from server...". While we get data from server.
  After getting data, it will show you a list of starships and how many stops do they need to travel the input value.
  If the starship does not include information of consumable or MegaLights the result will be unknown.
  Other wise you will see something like:
  ```javascript
     Executor: 476
     Sentinel-class landing craft: 19841
     Death Star: 3805
     Millennium Falcon: 9259
     Y-wing: 74405
     X-wing: 59524
     TIE Advanced x1: 79365
   ```
  
## Architecture

The Solution has been defined with 2 principal projects and 1 unit test project: 
   ##  ShipCalculator
   · This is main project that has the main method that will lunch the console application.
   
   ##  Calculator_BLL
   · In this project you will find all the logic.  You will have also 4 principal folders: 
       - Model : all Data Models used.
       - Helpers : static strings used and all values that are used to calculate the results.
       - API : Classes that connect you to the https://swapi.co/ to get the results
       - Repository: in this folder you will find all methods used to calculate the results for your request

   ## Unit Tests
    · This project also has some Unit Test to run, to get some values and prove that all that you get is expected.
      All these test are in a project called ShipCalculator.UnitTests

