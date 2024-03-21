namespace SimpleNotes.Contracts;

public record GetPreviewNoteResponse(Guid Id, string Title, DateTime CreatedAt);