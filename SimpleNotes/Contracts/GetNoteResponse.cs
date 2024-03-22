namespace SimpleNotes.Contracts;

/// <summary>
/// A response model representing a detailed view of a note.
/// </summary>
/// <param name="Id">The unique identifier of the note.</param>
/// <param name="Title">The title of the note.</param>
/// <param name="Content">The content of the note.</param>
/// <param name="CreatedAt">The date and time the note was created.</param>
public record GetNoteResponse(Guid Id, string Title, string Content, DateTime CreatedAt);