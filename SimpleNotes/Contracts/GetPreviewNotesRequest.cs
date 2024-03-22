using FluentValidation;

namespace SimpleNotes.Contracts;

/// <summary>
/// A request model for retrieving a paginated list of note previews, optionally filtered by a search query.
/// </summary>
public record GetPreviewNotesRequest : PaginatedFilter
{
    /// <summary>
    /// An optional search query to filter the notes by their title or content.
    /// </summary>
    public string? SearchQuery { get; init; }
}

public class GetPreviewNotesRequestValidator : AbstractValidator<GetPreviewNotesRequest>
{
}