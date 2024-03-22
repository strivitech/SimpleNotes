using FluentValidation;
using SimpleNotes.Common;

namespace SimpleNotes.Contracts;

/// <summary>
/// A request model for updating an existing note.
/// </summary>
/// <param name="Id">The unique identifier of the note to update.</param>
/// <param name="Title">The new title for the note.</param>
/// <param name="Content">The new content for the note.</param>
public record UpdateNoteRequest(Guid Id, string Title, string Content);

public class UpdateNoteRequestValidator : AbstractValidator<UpdateNoteRequest>
{
    public UpdateNoteRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(NoteContractConstants.MaxTitleLength);
        
        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(NoteContractConstants.MaxContentLength);
    }
}