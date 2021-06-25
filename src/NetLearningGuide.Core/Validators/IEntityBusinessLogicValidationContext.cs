using NetLearningGuide.Core.MySqlDomain;
using NetLearningGuide.Core.Services.ServiceLifetime;
using System;
using System.Collections.Generic;

namespace NetLearningGuide.Core.Validators
{
    public interface IEntityValidationContext : IInstancePerLifetimeService
    {
        Dictionary<Guid, IEntity> CachedEntity { get; set; }
    }

    class EntityValidationContext : IEntityValidationContext
    {
        public EntityValidationContext()
        {
            CachedEntity = new Dictionary<Guid, IEntity>();
        }

        public Dictionary<Guid, IEntity> CachedEntity { get; set; }
    }
}
