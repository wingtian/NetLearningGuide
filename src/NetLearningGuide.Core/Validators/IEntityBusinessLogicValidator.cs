using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Validators
{
    public interface IEntityBusinessLogicValidator
    {
        Type EntityType { get; }
        Task BeginValidation(EntityEntry entityEntry, IEntityValidationContext context);
    }
}
