namespace MusikApp;

// Songs table has four columns : id, title, duration_seconds, and album_id
// We need a C# Song object to store these values.
// Data class: A Song object represents one row in  the songs table.
// The song object contains the same information as that database row
// Each property represents one column in the SQLite database.
// Need more classes (Album, Artist) for your app? Create them in separate files.

public class Song    // Add properties
{
    // Add one property per column
    public int Id { get; set; }    

    public string Title { get; set; } = "";

    public int DurationSeconds { get; set; }

    public int AlbumId { get; set; }

}

// Summary:
// Added four properties, one for each database column.
// Used the correct C# data types (int and string).