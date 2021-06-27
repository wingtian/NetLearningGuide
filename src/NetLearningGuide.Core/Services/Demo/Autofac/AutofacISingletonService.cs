using System;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Services.Demo.Autofac
{
    public class AutofacISingletonService : IAutofacISingletonService
    {
        private Guid id;

        public AutofacISingletonService()
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
