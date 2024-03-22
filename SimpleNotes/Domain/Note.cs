namespace SimpleNotes.Domain;

/// <summary>
/// Represents a note entity with details such as title, content, and creation date.
/// </summary>
public class Note
{
    /// <summary>
    /// Gets or sets the unique identifier for the note.
    /// </summary>
    /// <value>The unique identifier as a Guid.</value>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the note. This property cannot be null.
    /// </summary>
    /// <value>The title of the note as a string.</value>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the content of the note. This property cannot be null.
    /// </summary>
    /// <value>The content of the note as a string.</value>
    public string Content { get; set; } = null!;

    /// <summary>
    /// Gets or sets the date and time when the note was created.
    /// </summary>
    /// <value>The creation date and time as a DateTime.</value>
    public DateTime CreatedAt { get; set; }
}
