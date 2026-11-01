//using Microsoft.Data.Sqlite;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Timers;
using Microsoft.Data.Sqlite;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace MusikApp;


// DbHandler.cs handles SQLite connections and SQL queries.
// Program.cs should never contain SQL or SqliteConnection.
public class DbHandler
{
    // Store the connection string here.It comes from appsettings.json via Program.cs (t.ex. "Data Source=musik.db").
    // Then open a NEW connection inside each method below.

    public DbHandler(string connectionString)
    {
            // TODO: store the connectionString in a field that the methods below can use.
            // Store connection string in a private field
            _connectionString = connectionString;
        }

        // Private field to store connection string
        private string _connectionString;

    // ---------------------------------------------------------------
    // G-nivå: you need at least three of the five here (choose which ones yourself)
    // ---------------------------------------------------------------

    /// <summary> </summary>
    //public List<Song> GetAllSongs()
    // TODO: SELECT from your song table using ORDER BY.
    // TODO: loop using reader.Read() and create a Song object from each row.
    // Tips: reader.GetInt32(0), reader.GetString(1) ... or reader["Titel"].



    //  Retrieves all songs, sorted by title.
    public List<Song> GetAllSongs()   
    {
        // Creates a list where we will store all the songs read from the database
        List<Song> songs = new List<Song>();  

        // Connect to SQLite
        using var connection = new SqliteConnection(_connectionString);  
        connection.Open();

        string sql = "SELECT id, title, duration_seconds, album_id FROM songs ORDER BY title";

        using var command = new SqliteCommand(sql, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read()) // Read each row
        {
            Song song = new Song();

            song.Id = reader.GetInt32(0);
            song.Title = reader.GetString(1);
            song.DurationSeconds = reader.GetInt32(2);
            song.AlbumId = reader.GetInt32(3);

            songs.Add(song);
        }
        return songs;  // Return the list of songs
        // List<Song> songs stores the songs returned from the database.
    }


    // Retrieves a specific song. Returns null if it does not exist
    public Song? GetSongById(int id)    
    {
        // Connect to SQLite
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string sql = "SELECT id, title, duration_seconds, album_id FROM songs WHERE id = $id";

        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("$id", id);

        using var reader = command.ExecuteReader();

        if (reader.Read())  // checks whether a song was found otherwise return null
        {
            Song song = new Song();

            song.Id = reader.GetInt32(0);
            song.Title = reader.GetString(1);
            song.DurationSeconds = reader.GetInt32(2);
            song.AlbumId = reader.GetInt32(3);

            return song;
        }

        return null;
    }

    ///  Searching for songs where the title contains the search text.
    public List<Song> SearchSongs(string searchText)
    {
        // Creates an empty list to store the matching songs
        List<Song> songs = new List<Song>();

        // Creates a connection to the SQLite database
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        // SQL query searches for titles containing the search text
        string sql = "SELECT id, title, duration_seconds, album_id FROM songs WHERE title LIKE $searchText ORDER BY title";

        // Creates a command to execute the SQL query
        using var command = new SqliteCommand(sql, connection);
        // Adds the search text as a parameter.
        command.Parameters.AddWithValue("$searchText", "%" + searchText + "%");

        // Executes the SQL query and reads the results
        using var reader = command.ExecuteReader();

        // Reads the matching songs one row at a time
        while (reader.Read())
        {
            Song song = new Song();

            // Gets the values from the database columns and stores them in the Song object
            song.Id = reader.GetInt32(0);
            song.Title = reader.GetString(1);
            song.DurationSeconds = reader.GetInt32(2);
            song.AlbumId = reader.GetInt32(3);

            songs.Add(song);
        }
        // Returns the list of matching songs
        return songs;
    }


    /// Adding a new song.
    public void AddSong(Song song)
    {
        
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        // INSERT INTO ... VALUES (@title, @durattion, ...) with parameters.
        // INSERT does not return any data, so use ExecuteNonQuery().
        string sql = "INSERT INTO songs (title, duration_seconds, album_id) VALUES ($title, $duration, $albumId)";

        using var command = new SqliteCommand(sql, connection);

        // AddWithValue() passes the values safely as parameters.
        command.Parameters.AddWithValue("$title", song.Title);
        command.Parameters.AddWithValue("$duration", song.DurationSeconds);
        command.Parameters.AddWithValue("$albumId", song.AlbumId);

        // ExecuteNonQuery() executes the INSERT command. It is used for commands that change data rather than return rows.
        int rowsAffected = command.ExecuteNonQuery();
        Console.WriteLine($"Rows inserted: {rowsAffected}");
    }

    ///  Removes a song. Returns true if a row was actually removed.
    public bool DeleteSong(int id)
    {
        // The WHERE id = $id condition ensures that only the song with the selected ID is deleted.
        // Using a parameter also helps protect against SQL injection.
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        // Starts a transaction to group the delete operations together
        using var transaction = connection.BeginTransaction();

        try
        {
            // First, delete the related genre information.
            string deleteGenreSql = "DELETE FROM song_genres WHERE song_id = $id";

            using var genreCommand = new SqliteCommand(deleteGenreSql, connection, transaction);
            genreCommand.Parameters.AddWithValue("$id", id);
            genreCommand.ExecuteNonQuery();

            // Then, delete the song.
            string deleteSongSql = "DELETE FROM songs WHERE id = $id";

            using var songCommand = new SqliteCommand(deleteSongSql, connection, transaction);
            songCommand.Parameters.AddWithValue("$id", id);

            // Executes the DELETE command and counts the deleted rows
            int rowsAffected = songCommand.ExecuteNonQuery();

            // Saves the changes if everything succeeds
            transaction.Commit();

            return rowsAffected > 0;
        }
        catch
        {
            // Reverses the changes if something fails
            transaction.Rollback();  // Reverses the changes if something fails.
            throw;
        }
    }

    // ---------------------------------------------------------------
    // VG-nivå: you need at least ONE of these two (and the reflection in the README)
    // ---------------------------------------------------------------

    /// <summary>(VG, alternativ A) Songs along with the album name. Requires a JOIN. </summary>
    public List<(string SongTitle, string AlbumName)> GetSongsWithAlbum()
    {
        // TODO: SELECT Songs.Titel, Album.Name FROM Songs JOIN Album ON ... ORDER BY ...
        throw new NotImplementedException();
    }

    /// <summary>(VG, alternativ B) Songs that are longer than the average length. Requires a subquery. </summary>
    public List<Song> GetSongsLongerThanAverage()
    {
        // TODO: SELECT ... WHERE Length > (SELECT AVG(Length) FROM Songs).
        throw new NotImplementedException();
    }
}



//  DbHandler class contains methods for working with the database, such as:
//  - GetAllSongs()
//  - GetSongById()
//  - SearchSongs()
//  - AddSong()
//  - DeleteSong()

//  The two execution methods
//  - ExecuteReader()    : Used when we want to retrieve data from the database
//  - ExecuteNonQuery()  : Used when we want to change data in the database

/// ADO.NET in five steps (the same in every method that communicates with the database):
///   1. Open a connection        -> new SqliteConnection(...) and connection.Open()
///   2. Create a command         -> connection.CreateCommand()
///   3. Write the SQL               -> command.CommandText = "..."
///   4. Submit values ​​securely    -> command.Parameters.AddWithValue("@namn", värde)
///   5. Run the command              -> ExecuteReader() för SELECT, ExecuteNonQuery() för INSERT/DELETE

/// Open the SQLite connection
///Create the SQL command
///Write the correct SQL query
///se parameters for user-provided values
///Use ExecuteReader() for SELECT
///Use ExecuteNonQuery() for INSERT and DELETE
///Use using var for database resources

