﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using ErrorOr;
using FluentValidation;
using FluentValidation.Results;

namespace SimpleNotes.Validation;

public sealed class RequestValidator : IRequestValidator
{
    private readonly IServiceProvider _serviceProvider;

    public RequestValidator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public List<Error> Validate<T>([NotNull]T model, [CallerArgumentExpression("model")] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(model, paramName);

        var validationResult = DoValidation(model);
        
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => Error.Validation(x.PropertyName, x.ErrorMessage)).ToList();
            return errors;
        }

        return new List<Error>();
    }

    private ValidationResult DoValidation<T>(T model)
    {
        var validator = GetValidator<T>();

        return validator.Validate(model);
    }
    
    private IValidator<T> GetValidator<T>()
    {
        var validator = _serviceProvider.GetService<IValidator<T>>();

        return validator ?? throw new InvalidOperationException($"Validator for type {typeof(T).FullName} was not found. Check if it's already registered.");
    }
}