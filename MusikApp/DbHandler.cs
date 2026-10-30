//using Microsoft.Data.Sqlite;
using Microsoft.Data.Sqlite;
namespace MusikApp;

/// <summary>
/// All database code lives here. Program.cs should never contain SQL or SqliteConnection.
///
/// This is a skeleton: the methods are there, the comments show what each method should do,
/// but the code is yours. All methods throw NotImplementedException until you have written them.
///
/// ADO.NET in five steps (the same in every method that communicates with the database):
///   1. Open a connection        -> new SqliteConnection(...) and connection.Open()
///   2. Create a command         -> connection.CreateCommand()
///   3. Write the SQL               -> command.CommandText = "..."
///   4. Submit values ​​securely    -> command.Parameters.AddWithValue("@namn", värde)
///   5. Run the command              -> ExecuteReader() för SELECT, ExecuteNonQuery() för INSERT/DELETE
///
/// Use "using var" for the connection and command, and they will close themselves.
/// Construct the SQL: using parameters (@title), never by concatenating strings.
/// </summary>
/// 

public class DbHandler
{
    // Tips: just store the connection string here.It comes from appsettings.json.
    // via Program.cs (t.ex. "Data Source=musik.db").
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

        /// <summary> Retrieves all songs, sorted by title. </summary>
        //public List<Song> GetAllSongs()
        // TODO: SELECT from your song table using ORDER BY.
        // TODO: loop using reader.Read() and create a Song object from each row.
        // Tips: reader.GetInt32(0), reader.GetString(1) ... or reader["Titel"].
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





    /// <summary> Retrieves a specific song. Returns null if it does not exist.</summary>
    public Song? GetSongById(int id)
    {
        // TODO: SELECT ... WHERE Id = @id (parameter!).
        // TODO: if (reader.Read()) { ... } annars returnera null.
        throw new NotImplementedException();
    }

    /// <summary> Searching for songs where the title contains the search text. </summary>
    public List<Song> SearchSongs(string searchText)
    {
        // TODO: SELECT ... WHERE Titel LIKE @sok.
        // Tips: the parameter value should be "%" + searchText + "%" so that LIKE matches parts of the title.
        throw new NotImplementedException();
    }

    /// <summary> Adding a new song. </summary>
    public void AddSong(Song song)
    {
        // TODO: INSERT INTO ... VALUES (@titel, @langd, ...) with parameters.
        // Tips: INSERT does not return any data, so use ExecuteNonQuery().
        throw new NotImplementedException();
    }

    /// <summary> Removes a song. Returns true if a row was actually removed. </summary>
    public bool DeleteSong(int id)
    {
        // TODO: DELETE FROM ... WHERE Id = @id.
        // Tips: ExecuteNonQuery() returns the number of affected rows.
        // The menu in Program.cs should ask "Are you sure? y/n" before this method is called.
        throw new NotImplementedException();
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
