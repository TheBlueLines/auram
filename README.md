# Auram

Auram is a database! It's easy to use and it's open source! Made in C#

## How to use

1. Create a C# program (Windows Forms App or Console App)
2. Right Click on Dependencies in the Solution Explorer
3. Click Add Project Reference
4. Click Browse at the bottom right, then in the open file dialog, select Auram.dll
5. In your program, add this:

```cs
using Auram;
```

## Commands

```cs
Database.SetTitle("EXAMPLE TITLE"); # Set the database title (optional)
Database.AddToDatabase("KeyGoesHere", "ValueGoesHere"); # Add a value to database
Database.SaveDatabase(path); # Save the database in your computer
Database.ClearDatabase(); # Clear the database
Database.LoadDatabase(path); # Load the database from the computer
string author = Database.GetFromDatabase("KeyGoesHere"); # Return the value of the key given
DateTime createdOn = Database.Since(); # Return the date, when the database created
DateTime lastModified = Database.LastEdit(); # Return the date, when the database edited
```

## About Auram
- It sompress the database file
- The database goes into a single file
- Open Source
- Easy to use in C#

## My website (empty)
[TTMC](https://ttmc.site/)