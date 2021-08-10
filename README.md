# About The Project
This project is a Cron Expression Parser console application, which parses a cron string and expands each field to show the times at which it will run. 

The standard cron forma conposites fieve time fields: minute, hour, day of month, month, and day of week.

**Build With**

 - C#
 - DotNetCore 3.1

# Getting Started
**Prerequisites**
 - On Linux System
 Add the Microsoft package signing key to your list of trusted keys and add the package repository.
 ```
 wget https://packages.microsoft.com/config/ubuntu/21.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
 ```
 Install the SDK
 ```
 sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-3.1
  ```
 - On MacOS
 
SDK Intaller would be ready for donload: https://dotnet.microsoft.com/download/dotnet/3.1

# Usage
 - Acceptable Inputs:
```
                                           Allowed values    Allowed special characters   Comment

    ┌───────────── second (optional)       0-59              * , - /                      
    │ ┌───────────── minute                0-59              * , - /                      
    │ │ ┌───────────── hour                0-23              * , - /                      
    │ │ │ ┌───────────── day of month      1-31              * , - /               
    │ │ │ │ ┌───────────── month           1-12              * , - /                      
    │ │ │ │ │ ┌───────────── day of week   0-6 (0 - Sun)     * , - /
    │ │ │ │ │ │
    * * * * * *
   ```
 - Run in termal
Fisrt `cd` into the project folder.
 `dotnet run --project Cron_Parser_Deliveroo "YourInput"`
 Example Usage:
 `dotnet run --project Cron_Parser_Deliveroo "23 0-20/2 * * *"`
 Example Output:
 ```
 minute        23
hour          0,2,4,6,8,10,12,14,16,18,20
day of month  1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31
month         1,2,3,4,5,6,7,8,9,10,11,12
dayofweek     0,1,2,3,4,5,6
```

# Test
Tests Included:
 - Test_Point_Time
 - Test_Range
 - Test_Interval
 - Test_Range_With_Interval
 - Test_Any_With_Interval
 - Test_Minute_OutOfRange
 - Test_Hour_OutOfRange
 - Test_DayOfMonth_OutOfRange
 - Test_Month_OutOfRange
 - Test_DayOfWeek_OutOfRange
 - Test_Interval_Negtive
 - Test_Interval_Zero

To run the test you will need to install `Visual Studio` or `Visual Studio Code`

# Future Development
 - For month, day of week, only integers are accpeted and string expression like `JAN` or `MON` is not accepted
 - Special expressions like `@weekly`, `@hourly`, `L` and `W`.
 - Code could be refactored to smaller methods with more time.

