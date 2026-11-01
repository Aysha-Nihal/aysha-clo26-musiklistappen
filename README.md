# Musiklistappen — [Aysha Nihal]

> **Instructions for you:** this file is *your* README. Replace all text in square brackets with your own, and remove this box when you are finished. The task and requirements are in [`UPPGIFT.md`](UPPGIFT.md), and the submission is described in [`INLAMNING.md`](INLAMNING.md).

---

## What the app does

[This is a console-based Music List App built with C# and SQLite, using songs from A. R. Rahman's discography. The app allows users to view all songs, add new songs, delete songs, see songs together with their album names, and find songs that are longer than the average duration. ]

## How to operate it
Clone the repository and run the application:
```bash
git clone (https://github.com/Aysha-Nihal/aysha-clo26-musiklistappen.git)
cd aysha-clo26-musiklistappen/MusikApp
dotnet run
```

The SQLite database file musiklist.db is already included in the MusikApp folder. No separate database setup is required.

Make sure you run the commands from the repository folder as shown above.

## This is what the main menu looks like

[![Main menu](Main%20menu-image.png)``]

---

## My design choices (VG)

*Fill in this section if you are aiming for a VG. Otherwise, you can remove it..*

1. **How did you design your database? Why did you choose that particular structure?**

   [I designed my database using separate tables for artists, albums, and songs. The artists table stores artist information, the albums table stores album information, and the songs table stores song information. I connected the tables using primary keys and foreign keys.

I chose this structure to avoid storing the same information multiple times. It also makes it easier to manage songs and albums and connect them when needed.]

2. **Which SQL approach did you choose (JOIN or subquery), and why was it a good fit for your app?**

   [I used both JOIN and a subquery in my application.

I used an INNER JOIN to display songs together with their album names. This was useful because the song and album information is stored in separate tables.

I also used a subquery with AVG() to find songs that are longer than the average song duration. This allowed the application to calculate the average automatically and compare each song with it.]

3. **What would you have done differently if you rebuilt it from scratch?**

   [If I rebuilt the application from scratch, I would plan the database and application structure more carefully before starting to code. I would also test each method step by step and add input validation earlier to prevent invalid user input.

This project helped me understand how C#, SQLite, and ADO.NET work together in a real application.]
