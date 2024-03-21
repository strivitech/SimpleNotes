using FluentValidation;

namespace SimpleNotes.Contracts;

public record GetPreviewNotesRequest : PaginatedFilter
{
    public string? SearchQuery { get; init; }
}

public class GetPreviewNotesRequestValidator : AbstractValidator<GetPreviewNotesRequest>
{
}