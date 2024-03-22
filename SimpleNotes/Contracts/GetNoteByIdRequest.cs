using FluentValidation;

namespace SimpleNotes.Contracts;

/// <summary>
/// A request model for retrieving a note by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the note to retrieve.</param>
public record GetNoteByIdRequest(Guid Id);

public class GetNoteByIdRequestValidator : AbstractValidator<GetNoteByIdRequest>
{
    public GetNoteByIdRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}