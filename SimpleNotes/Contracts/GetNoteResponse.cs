namespace SimpleNotes.Contracts;

public record GetNoteResponse(Guid Id, string Title, string Content, DateTime CreatedAt);