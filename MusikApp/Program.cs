using Microsoft.Extensions.Configuration;
using MusikApp;

// Reads configuration settings from appsettings.json.
// The database path is stored there instead of writing it directly in C# code.
// This makes it easier to change the database path without changing the code.

// appsettings.json = stores the database path.
// ConfigurationBuilder = reads the settings.
// GetConnectionString() = retrieves the database connection string.
var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

// Gets the database connection string named "Default" from appsettings.json.
// Stores the connection string in the connectionString variable.
// ?? checks if the value is null.
// If the connection string is missing (null), throw an InvalidOperationException.
// This helps us know if the database connection string is missing.
var connectionString = config.GetConnectionString("Default")
    ?? throw new InvalidOperationException("Can't find a connection string 'Default' in appsettings.json.");

// Creates a DbHandler object and passes the database connection string to it.
// The DbHandler object will be used to handle database operations, such as getting, adding and deleting songs.
// ie  Creates the object that handles database operations.
Console.WriteLine($"Working directory: {Directory.GetCurrentDirectory()}");
var dbHandler = new DbHandler(connectionString);
Console.WriteLine("Database handler created.");

while (true)
{
    // Display menu options.
    Console.WriteLine();
    Console.WriteLine("----- My Music App -----");
    Console.WriteLine("1. Show all songs");
    Console.WriteLine("2. Add a song");
    Console.WriteLine("3. Delete a song");
    Console.WriteLine("4. Exit");
    Console.Write("Choose: ");
    string? choice = Console.ReadLine();
    // Reads the user's selection and stores it in choice
    // string? means choice can contain text or null.
    // Console.WriteLine() displays the menu.
    // Console.ReadLine() reads what the user types.


    // // switch checks which option the user selected.
    switch (choice)
    {
        case "1":
            // Gets all songs from the database using DbHandler.
            List<Song> songs = dbHandler.GetAllSongs();

            // Goes through the song list one song at a time.
            foreach (Song song in songs)
            {
                // Displays the song ID, title and duration
                Console.WriteLine($"{song.Id}. {song.Title} - {song.DurationSeconds} seconds");
            }
            break;

        case "2":
            Song newSong = new Song();

            Console.Write("Enter song title: ");
            newSong.Title = Console.ReadLine() ?? "";

            Console.Write("Enter duration in seconds: ");
            newSong.DurationSeconds = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter album ID: ");
            newSong.AlbumId = Convert.ToInt32(Console.ReadLine());

            dbHandler.AddSong(newSong);

            Console.WriteLine("Song added successfully!");
            break;

        case "3":
            // Display all songs.
            var allSongs = dbHandler.GetAllSongs();

            foreach (var song in allSongs)
            {
                Console.WriteLine($"{song.Id}. {song.Title}");
            }

            // Ask the user which song to delete.
            Console.Write("Enter the song ID to delete: ");
            int id = Convert.ToInt32(Console.ReadLine());

            // Ask for confirmation.
            Console.Write("Are you sure? y/n: ");
            string confirmation = Console.ReadLine() ?? "";

            if (confirmation.ToLower() == "y")
            {
                bool deleted = dbHandler.DeleteSong(id);

                if (deleted)
                {
                    Console.WriteLine("Song deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Song not found.");
                }
            }
            else
            {
                Console.WriteLine("Deletion cancelled.");
            }
            break;

        case "4":
            Console.WriteLine("Goodbye!");
            return;

        default:
            Console.WriteLine("Invalid choice. Please choose 1-4.");
            break;
    }

}


// Application working directory: C:\Users\nihal\source\repos\aysha-clo26-musiklistappen\MusikApp\bin\Debug\net10.0\musiklist.db
