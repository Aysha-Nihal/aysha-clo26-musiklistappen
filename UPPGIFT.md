# The Music List App — C# and SQLite

🟢

**Kurs:** CLO26 — Cloud Developer
**Deadline:** Måndag

---

## Intro

You've already built the music list in SQL (the discography). Now you'll bring the database into C# and build a simple console program that lets you navigate, add, and delete data via menus.

There are no strange requirements and no long checklists. You know what to do — you've done it in SQL. Now it's a matter of connecting it with C#.

**The Goal:** show that you understand the idea — a program that talks to a database — and that you can use  **ADO.NET** (`SqliteConnection`, `SqliteCommand` och `SqliteDataReader`) to read and modify data. That's all you need.

---

## What to build

A console app that manages your favorite artist's music. The app should have a clear menu and allow the user to do basic things with the data.

You are using **SQLite** and your database from the discography submission. You can use the tables as they are, you don't need to change anything in the schema.

> Don't have a `.db`-fil left? Import it `exempel/musik_exempel.sql` in DB Browser for SQLite (*Fil → Importera → Databas från SQL-fil*), and you'll have a small, ready-made database to practice with.

---

## This is what you get in the repot

| File | What it is |
|-----|-----------|
| `MusikApp/DbHandler.cs` | A **skelett**: the methods and comments are there, you write the code yourself |
| `MusikApp/Song.cs` | Your data class (empty, you add the properties) |
| `MusikApp/Program.cs` | 	This is where you build the menu. It already loads it `appsettings.json` for you |
| `MusikApp/appsettings.json` | Settings: the path to your database (`Data Source=musik.db`) |
| `MusikApp/appsettings.example.json` | Example showing which keys are needed (same as in hero_database) |
| `README.md` | Template for your own README, which you fill in |
| `reflektion.md` | The reflection, which you fill in |
| `INLAMNING.md` | 	Step by step how to submit |

---

## Krav (G)

### The app

- Launches and displays a **huvudmeny** with options numbered 1–N
- The user navigates by typing a number and pressing Enter.
- The app exits gracefully when the user selects "Exit"

### Vad menyn ska kunna göra

At least three of the following (choose which ones):

- **Show all** songs/albums (list in console)
- **Add** a new song or album
- **Delete** a record (with confirmation: "Are you sure? y/n")
- **Search** by title or artist
- **View details** for a specific record

### Code

- At least one class for your data, e.g `Song`, `Album` or `Artist`
- The database logic is in the class `DbHandler`, not everything in `Program.cs`
- Parameterized queries — not string concatenation

### Submission

See `INLAMNING.md`. You submit:

- your code (the entire folder)
- a short `README.md` description of: what the app does, how to run it (clone, build, run) and a screenshot of the main menu
- filled in `reflektion.md`

---

## VG

VG requires **båda** parts:

### Del 1 — Advanced SQL queries in the app

Your app should use at least **ett** of the following in its database code:

**A) JOIN across multiple tables**
The app has at least two tables (e.g. 'Album' and 'Songs') and retrieves data from both in the same query:

```sql
SELECT Låtar.Titel, Album.Namn
FROM Låtar
JOIN Album ON Låtar.AlbumId = Album.Id
ORDER BY Album.Namn, Låtar.Titel;
```

**B) Subquery**
Appen använder en underfråga — t.ex. för att visa låtar som är längre än medellängden:

```sql
SELECT Titel, Längd
FROM Låtar
WHERE Längd > (SELECT AVG(Längd) FROM Låtar);
```

### Del 2 — Reflektion i README

Lägg till ett avsnitt **"Mina designval"** i din `README.md` där du svarar på:

1. Hur designade du din databas? Varför valde du just den strukturen?
2. Vilket SQL-krav valde du (JOIN eller subquery) och varför passade det din app?
3. Vad skulle du gjort annorlunda om du byggde om den från scratch?

---

## Kodexempel — så kopplar du till SQLite

```csharp
// ADO.NET mot SQLite. NuGet: Microsoft.Data.Sqlite (redan installerat i projektet)
using Microsoft.Data.Sqlite;

using var connection = new SqliteConnection("Data Source=musik.db");
connection.Open();

var command = connection.CreateCommand();
command.CommandText = "SELECT Id, Titel FROM Låtar ORDER BY Titel";

using var reader = command.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine($"{reader["Id"]}. {reader["Titel"]}");
}
```

Använd alltid parameteriserade queries när du tar emot input:

```csharp
command.CommandText = "INSERT INTO Låtar (Titel, Längd) VALUES (@titel, @langd)";
command.Parameters.AddWithValue("@titel", titel);
command.Parameters.AddWithValue("@langd", langd);
command.ExecuteNonQuery();
```

---

## Tips

- Börja med att få menyn att fungera — utan databas. Skriv ut "Här kommer en lista senare" tills anslutningen är klar.
- Lägg din `.db`-fil i mappen `MusikApp/` och döp den till `musik.db`. Heter din fil något annat ändrar du i `appsettings.json`, inte i koden.
- `appsettings.json` följer med i mallen så att appen fungerar direkt, och den innehåller här bara databasens filnamn, inga hemligheter. Normalt checkar man *inte* in den (se hero_databas, labb 1). Lägger du någonsin ett lösenord i en sådan fil: kör `git rm --cached MusikApp/appsettings.json` först, så att git slutar följa den. Det som ska finnas i repot är `appsettings.example.json`.
- En metod = en sak. `VisaAllaLåtar()`, `LäggTillLåt()`, `TaBortLåt()` — håll dem korta.
- Du behöver inte skriva om eller förbättra SQL-schemat. Det du har fungerar.
- Fastnar du: börja med `GetAllSongs()` i `DbHandler.cs`. När den fungerar är resten samma mönster.

---

*Du har redan gjort det svåra. Nu är det bara att paketera det i C#.*
