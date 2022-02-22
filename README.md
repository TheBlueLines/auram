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
Database.Add("KeyGoesHere", "ValueGoesHere"); # Add a value to database
Database.Save(path); # Save the database in your computer
Database.Clear(); # Clear the database
Database.Load(path); # Load the database from the computer
string value = Database.Get("KeyGoesHere"); # Return the value of the key given
```

## About Auram
- It sompress the database file
- The database goes into a single file
- Open Source
- Easy to use in C#

## My website (empty)
[TTMC](https://ttmc.site/)