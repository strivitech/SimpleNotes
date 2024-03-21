using FluentValidation;

namespace SimpleNotes.Contracts;

public record PaginatedFilter
{
    public int Page { get; init; } = 1;
    
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