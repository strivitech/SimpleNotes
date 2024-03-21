using FluentValidation;

namespace SimpleNotes.Contracts;

public record GetNoteByIdRequest(Guid Id);

public class GetNoteByIdRequestValidator : AbstractValidator<GetNoteByIdRequest>
{
    public GetNoteByIdRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}