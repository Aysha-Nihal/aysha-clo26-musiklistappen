namespace MusikApp;

// Your data class: a Song object = a row in your song table.
//
// TODO: add one property per column you want to display in the app,
// e.g. Id, Title, and Duration. Names and types should match your database.
//
// Do you need more classes (Album, Artist) for your app? Create them in separate files.
public class Song
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public int DurationSeconds { get; set; }

    public int AlbumId { get; set; }

}
