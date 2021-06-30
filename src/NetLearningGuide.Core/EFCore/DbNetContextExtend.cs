using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NetLearningGuide.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.EFCore
{
    public partial class DbNetContext
    {
        public override int SaveChanges()
        {
            AsyncHelper.RunSync(() => OnBeforeSaveAsync(true));
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await OnBeforeSaveAsync(true).ConfigureAwait(false);
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private async Task OnBeforeSaveAsync(bool shouldValidate)
        {
            foreach (var entityEntry in ChangeTracker.Entries())
            {
                await ValidateEntity(entityEntry, shouldValidate).ConfigureAwait(false);
            }
        }

        private async Task ValidateEntity(EntityEntry entityEntry, bool shouldValidate)
        {
            if (shouldValidate && (entityEntry.State == EntityState.Modified || entityEntry.State == EntityState.Added))
            {
                var validationContext = _entityBusinessLogicValidationContextProvider();
                var validationErrors = new List<ValidationFailure>();
                _entityFluentValidators.ForEach(x =>
                {
                    if (x.EntityType == entityEntry.Entity.GetType())
                        x.Validate(entityEntry).Errors.ForEach(e =>
                            validationErrors.Add(new ValidationFailure(e.PropertyName, e.ErrorMessage)));
                });
                if (validationErrors.Any())
                    throw new ValidationException(validationErrors);

                var validators = _entityBusinessLogicValidators();
                foreach (var entityBusinessLogicValidator in validators)
                {
                    if (entityBusinessLogicValidator.EntityType == entityEntry.Entity.GetType())
                    {
                        await entityBusinessLogicValidator.BeginValidation(entityEntry, validationContext).ConfigureAwait(false);
                    }
                }
            }
        }
    }
}
