using FluentValidation;
using SimpleNotes.Common;

namespace SimpleNotes.Contracts;

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