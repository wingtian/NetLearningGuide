using System;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Services.Demo.Autofac
{
    public class AutofacInstancePerLifetimeService : IAutofacInstancePerLifetimeService
    {
        private Guid id;

        public AutofacInstancePerLifetimeService()
        {
            id = Guid.NewGuid();
        }
        public async Task<Guid> GetGuid()
        {
            return await Task.Run(() =>
            {
                return id;
            }).ConfigureAwait(false);
        }
    }
}
