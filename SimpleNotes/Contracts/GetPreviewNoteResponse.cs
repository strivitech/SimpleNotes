namespace SimpleNotes.Contracts;

/// <summary>
/// A response model representing a preview of a note, containing less detail than the full note view.
/// </summary>
/// <param name="Id">The unique identifier of the note.</param>
/// <param name="Title">The title of the note.</param>
/// <param name="CreatedAt">The date and time the note was created.</param>
public record GetPreviewNoteResponse(Guid Id, string Title, DateTime CreatedAt);