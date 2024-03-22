using ErrorOr;

namespace SimpleNotes.Common;

public static class Errors
{
    public static class Notes
    {
        public static Error NotFound(Guid id) =>
            Error.NotFound("Note.NotFound", $"Note with id {id.ToString()} not found");

        public static Error CreateFailed(Guid id) => Error.Failure("Note.CreateFailed", $"Failed to create note with id {id.ToString()}");
        
        public static Error GetPreviewsFailed() => Error.Failure("Note.GetPreviewsFailed", "Failed to get note previews");
        
        public static Error GetFailed(Guid id) => Error.Failure("Note.GetFailed", $"Failed to get note with id {id.ToString()}");
        
        public static Error UpdateFailed(Guid id) => Error.Failure("Note.UpdateFailed", $"Failed to update note with id {id.ToString()}");
        
        public static Error CountFailed() => Error.Failure("Note.CountFailed", "Failed to count notes");
    }
}