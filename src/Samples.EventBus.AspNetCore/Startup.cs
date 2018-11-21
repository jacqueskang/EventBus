using JKang.EventBus.Samples.InMemory.AspNetCore.EventHandlers;
using JKang.EventBus.Samples.InMemory.AspNetCore.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JKang.EventBus.Samples.InMemory.AspNetCore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc();

            services
                .AddMemoryCache();

            services
                .AddEventBus(builder =>
                {
                    builder
                        .AddInMemoryEventBus()
                        .AddAmazonSnsEventPublisher(x => x.Region = "eu-west-3")
                        .AddEventHandler<MessageSent, MessageSentEventHandler>()
                        ;
                })
                ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
