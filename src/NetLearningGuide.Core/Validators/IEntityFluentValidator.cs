using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace NetLearningGuide.Core.Validators
{
    public interface IEntityFluentValidator
    {
        Type EntityType { get; }
        ValidationResult Validate(EntityEntry entityEntry);
    }
}
