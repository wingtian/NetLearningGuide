using System;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Services.Demo.Autofac
{
    public class AutofacInstancePerDependencyService : IAutofacInstancePerDependencyService
    {
        private Guid id;

        public AutofacInstancePerDependencyService()
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
