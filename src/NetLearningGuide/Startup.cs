using System;
using Autofac;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NetLearningGuide.Core.HangFireJob;
using NetLearningGuide.Core.Module;

namespace NetLearningGuide
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddMemoryCache();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NetLearningGuide", Version = "v1" });
            });
            services.AddHangfire(config => config.UseMemoryStorage());
            services.AddHangfireServer();  //Add the processing server as IHostedService
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new NetLearningGuideModule(new NetLearningGuideModule.DbUpSetting()
            {
                ShouldRunDbUp = true,
                DbUpConnectionString = Configuration.GetConnectionString("Mysql")
            }));

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetLearningGuide v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            HangFireJob.ExecuteRecurringJob();
        }
    }
}
