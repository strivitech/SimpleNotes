using FluentValidation;

namespace SimpleNotes.Contracts;

public record GetPreviewNotesRequest : PaginatedFilter;

public class GetPreviewNotesRequestValidator : AbstractValidator<GetPreviewNotesRequest>
{
}