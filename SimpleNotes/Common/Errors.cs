using ErrorOr;

namespace SimpleNotes.Common;

public static class Errors
{
    public static class Notes
    {
        public static Error NotFound(Guid id) =>
            Error.NotFound("Note.NotFound", $"Note with id {id.ToString()} not found");

        public static Error CreateFailed(Guid id) => Error.Failure("Note.CreateFailed", $"Failed to create note with id {id.ToString()}");
    }
}