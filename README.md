Overview::
Solution contains below projects and their .
SA.DriverReportGenerator -> Console App to run 
SA.Entities -> All the models
SA.Helpers -> For All Business Functions
SA.Tests -> To test different use case

Manual Running::
Open the SA.sln in Visual Studio 2019 and run SA.DriverReportGenerator
Download inputfile.txt and pass as file path when the console asks for it.
Run the SA.Tests to validate whether the Driver/Trips are added.

Code:
IFileProcessor.cs - Used to load the file
IHelper.cs - Used to Add Driver, Trip and Calculate Trip Duration
IOutputGenerator.cs - Used to print the output.

Requirement::

1. Process an input file. 
2. Each line in the input file will start with a command. There are two possible commands.
   Command 1:
    The first command is Driver, which will register a new Driver in the app.  
   Example: Driver Dan 
   Command 2:
    The second command is Trip, which will record a trip aributed to a driver.
    The line will be space delimited with the following fields: the command (Trip), driver name, start time, stop time, miles driven.  
    Times will be given in the format of hours:minutes.
    We'll use a 24-hour clock and will assume that drivers never drive past midnight (the start time will always be before the end time).  
    Example: Trip Dan 07:15 07:45 17.3 
    Discard any trips that average a speed of less than 5 mph or greater than 100 mph. 
    Generate a report containing each driver with total miles driven and average speed. Sort the output by most miles driven to least.
    Round miles and miles per hour to the nearest integer.
     
Example Input::
Driver Dan 
Driver Alex 
Driver Bob 
Trip Dan 07:15 07:45 17.3 
Trip Dan 06:12 06:32 21.8 
Trip Alex 12:01 13:16 42.0 

Expected Output::
Alex: 42 miles @ 34 mph 
Dan: 39 miles @ 47 mph 
Bob: 0 miles 
