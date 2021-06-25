using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NetLearningGuide.Core.Domain.Demo;
using System;

namespace NetLearningGuide.Core.Validators.Demo
{
    public class DbUpValidator : AbstractValidator<TestDbUp>, IEntityFluentValidator
    {
        public DbUpValidator()
        {
            RuleFor(x => x.DescInfo).NotEmpty().Length(1, 45).WithMessage("描述长度在1到45之间!");
        }

        public Type EntityType => typeof(TestDbUp);

        public ValidationResult Validate(EntityEntry entityEntry)
        {
            return Validate(entityEntry.Entity as TestDbUp);
        }
    }
}
