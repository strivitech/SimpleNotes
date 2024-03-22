using FluentValidation;
using SimpleNotes.Common;

namespace SimpleNotes.Contracts;

/// <summary>
/// A request model for creating a new note.
/// </summary>
/// <param name="Title">The title of the note to be created.</param>
/// <param name="Content">The content of the note to be created.</param>
public record CreateNoteRequest(string Title, string Content);

public class CreateNoteRequestValidator : AbstractValidator<CreateNoteRequest>
{
    public CreateNoteRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(NoteContractConstants.MaxTitleLength);
        
        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(NoteContractConstants.MaxContentLength);
    }
}