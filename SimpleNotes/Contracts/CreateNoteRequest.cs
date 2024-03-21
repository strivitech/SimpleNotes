using FluentValidation;
using SimpleNotes.Common;

namespace SimpleNotes.Contracts;

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