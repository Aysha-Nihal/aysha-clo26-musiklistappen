# How do you submit?

**Course:** CLO26 — Cloud Developer
**Deadline:** Måndag

---

## Step by step

1. **Fork this repo** (*Fork* button in the top right of GitHub).
2. **Name your fork** to `firstname-clo26-musiklistappen`
   Example: `betty-clo26-musiklistappen`, `erik-clo26-musiklistappen`
3. **Clone your fork** to your computer and open the  `MusikApp.slnx`.
4. **Addyour database** and `MusikApp/` name it `musik.db`.
   (No database? Import `exempel/musik_exempel.sql` in DB Browser for SQLite.)
   The path is in `MusikApp/appsettings.json`.If your file is called something else, change it there.
5. **Build the app.** Start with the menu in  `Program.cs`, then the method in `DbHandler.cs`. Read `UPPGIFT.md` for the requirements. Save often with `git commit`.
6. **Fill in `README.md`** (replace the template with your own text and add a screenshot of the main menu).
7. **Fill in `reflektion.md`.**
8. **Check that everything is correct** (checklist below).
9. **Zip the entire folder** and submit it to **Google Classroom**, just like in the discography.

---

## Checklist before submitting

- [ ] The app builds and starts  (`dotnet run` i mappen `MusikApp`)
- [ ] Main menu with numbered options, and the app exits nicely
- [ ] At least three menu choices work against the database
- [ ] All database code is in `DbHandler.cs`,  no SQL in `Program.cs`
- [ ] All queries with input are **parameterized** (`@titel`), no string concatenation
- [ ] `README.md` is filled in: what the app does, how to run it and a screenshot
- [ ] `reflektion.md` is filled in
- [ ] (VG) JOIN  or subquery is in the app, and "My design choices" are in the  README

---

## What do I do if something goes wrong?

- **Could not find database" / empty list** check that `musik.db` is in `MusikApp/`, that the file name matches `appsettings.json` and that the table names in your SQL match your database.
- **`FileNotFoundException` if `appsettings.json`:** file is missing. Copy `appsettings.example.json` to `appsettings.json` folder `MusikApp/`.
- **`NotImplementedException`:** it just means that you haven't written the method yet. It's meant to be visible until you have.
- **If you get stuck:** write in the class chat or ask at the beginning of the lesson. Do not submit a half-finished assignment without asking.
