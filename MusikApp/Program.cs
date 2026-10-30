using Microsoft.Extensions.Configuration;
using MusikApp;

// Reads settings from appsettings.json (in the same way as in hero_databas).
// The database path is located there, so you don’t have to write it in the code.
var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var connectionString = config.GetConnectionString("Default")
    ?? throw new InvalidOperationException("Can't find a connection string 'Default' in appsettings.json.");

// Start here. This line indicates that the settings were loaded; feel free to remove it whenever you like.
//Console.WriteLine($"The music playlist app: start by getting the menu to work!  (databas: {connectionString})");

// Tips for order (check UPPGIFT.md):
//   1. Print the main menu (options numbered 1-N) and load a selection with Console.ReadLine().
//      Print "A list will appear here later" until the database is connected.
//   2. Create a DbHandler object: new DbHandler(connectionString)
//   3. Call DbHandler’s methods from the menu. SQL should NEVER appear here.
//   4. Exit gracefully when the user selects "Exit".

var dbHandler = new DbHandler(connectionString); //creates an object that will handle database operations.
Console.WriteLine("Database handler created.");

while (true)
{
    Console.WriteLine();
    Console.WriteLine("=== My Music App ===");
    Console.WriteLine("1. Show all songs");
    Console.WriteLine("2. Add a song");
    Console.WriteLine("3. Delete a song");
    Console.WriteLine("4. Exit");
    Console.Write("Choose: ");
    string? choice = Console.ReadLine();
    // string? means the variable choice can contain text or null
    //choice stores the user's selection

    switch (choice)
    {
        case "1":
            List<Song> songs = dbHandler.GetAllSongs();

            foreach (Song song in songs)
            {
                Console.WriteLine($"{song.Id}. {song.Title} - {song.DurationSeconds} seconds");
            }
            break;

        case "2":
            Console.WriteLine("Adding a song...");
            break;

        case "3":
            Console.WriteLine("Deleting a song...");
            break;

        case "4":
            Console.WriteLine("Goodbye!");
            return;

        default:
            Console.WriteLine("Invalid choice. Please choose 1-4.");
            break;
    }

}