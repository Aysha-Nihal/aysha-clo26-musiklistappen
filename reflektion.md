# Reflection — Musiklistappen

**Namn:** Aysha Nihal
**Datum:** 2026-11-01

---

## What was the most difficult part?

### What was the most difficult part?

The most difficult part was connecting my C# application to the SQLite database and making sure that all database operations worked correctly. I had to understand how to use ADO.NET classes such as `SqliteConnection`, `SqliteCommand`, and `SqliteDataReader`.

Implementing the JOIN and subquery was also challenging at first. By working step by step and testing each method, I learned how to retrieve, add, search, and delete data from the database.

---

## What did you learn?

### What did you learn?

I learned how to connect a C# console application to a SQLite database using ADO.NET. I learned how to use `SqliteConnection`, `SqliteCommand`, and `SqliteDataReader` to retrieve, add, search, and delete data.

I also learned how to use parameterized queries to work safely with user input, how to use an INNER JOIN to combine information from two tables, and how to use a subquery with AVG() to calculate the average song duration.

Most importantly, I learned to build and test an application step by step and understand how C# and SQL work together.

---

## How did you use ADO.NET in practice—what felt natural, and what was confusing?

Keep the five steps in mind: open connection, create command, write SQL, submit values, execute.

I used ADO.NET to connect my C# application to the SQLite database and perform database operations.

I followed the five steps:

1. Open connection: I used SqliteConnection to connect to the database and opened it with connection.Open().

2. Create command: I used SqliteCommand to prepare the database operation.

3. Write SQL: I wrote SQL queries such as SELECT, INSERT, and DELETE.

4. Submit values: I used parameters such as $id and $title to pass values safely to the SQL query.

5. Execute: I used ExecuteReader() to read data and ExecuteNonQuery() to add or delete data.

The most natural part was writing SQL queries because I had already practised SQL in the database course. The confusing part was understanding how the C# objects work together, especially the connection, command, reader, and parameters. After implementing and testing each method step by step, the process became easier to understand.

---

## Why should you use parameterized queries?

What could happen if you construct SQL by concatenating strings instead? (Think of Bobby Tables.)

Parameterized queries make SQL queries safer because they separate SQL code from user input. They help prevent SQL injection attacks.

If we build SQL queries by concatenating strings, a user could enter malicious SQL instead of normal input. This could change the meaning of the query and potentially delete, modify, or access data without permission.

For example, Bobby Tables shows how a malicious input can turn a normal SQL command into a command that deletes data.

In my application, I used parameters such as $id, $title, and $duration with AddWithValue() instead of adding user input directly to the SQL string. This makes the application safer.

---

## If you had more time — what would you have added or done differently?

If I had more time, I would improve the input validation to make sure users enter correct values when adding or deleting songs. I would also add more search options, such as searching for songs by album name or artist.

I would improve the menu by making it easier to navigate and display clearer error messages. I would also add more testing to make sure the application works correctly in different situations.

I would like to continue improving the application as I learn more about C# and databases.

---

## (VG) Why did you split up the code the way you did?

Discuss your design choice: why is the database code in `DbHandler` and not in `Program.cs`? What would have become more difficult if everything were in the same file?

I separated the code to make the application easier to understand, maintain, and test.

I placed all database operations in DbHandler.cs because this class is responsible for communicating with the SQLite database. It handles operations such as showing, searching, adding, and deleting songs.

I kept Program.cs responsible for the main menu and user interaction. It asks the user what they want to do and calls the appropriate method in DbHandler.

I also created a separate Song.cs class to represent a song and store its information, such as ID, title, duration, and album ID.

If everything were in Program.cs, the file would become much longer and harder to read. It would also be more difficult to find errors, make changes, and reuse database code.

Separating responsibilities makes the application more organized and easier to maintain.
