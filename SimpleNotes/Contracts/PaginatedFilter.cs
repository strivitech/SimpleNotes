using FluentValidation;

namespace SimpleNotes.Contracts;

/// <summary>
/// A base model for supporting pagination in requests.
/// </summary>
public record PaginatedFilter
{
    /// <summary>
    /// The page number of the results to retrieve. Default is 1.
    /// </summary>
    public int Page { get; init; } = 1;
    
    /// <summary>
    /// The number of items to include in a page. Default is 10.
    /// </summary>
    public int PageSize { get; init; } = 10;
}

public class PaginatedFilterValidator : AbstractValidator<PaginatedFilter>
{
    public PaginatedFilterValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);
    }
}