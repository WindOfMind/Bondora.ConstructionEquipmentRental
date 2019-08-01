using System;
using Bondora.ConstructionEquipmentRental.Domain.Interfaces;
using Bondora.ConstructionEquipmentRental.Equipment;
using Bondora.ConstructionEquipmentRental.Messages;
using Bondora.ConstructionEquipmentRental.Service.Filters;
using Bondora.ConstructionEquipmentRental.Service.Hubs;
using Bondora.ConstructionEquipmentRental.Service.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.ObjectBuilder.MSDependencyInjection;

namespace Bondora.ConstructionEquipmentRental.Service
{
    public class Startup
    {
        private IEndpointInstance _endpoint;

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvcCore()
                .AddCors()
                .AddJsonFormatters()
                .AddApiExplorer()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services
                .AddSignalR();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            services.AddSingleton<IEquipmentRepository, EquipmentRepository>();
            services.AddScoped<EquipmentFilter>();
            services.AddSingleton(serviceProvider => _endpoint);

            var endpointConfiguration = new EndpointConfiguration("Bondora.ConstructionEquipmentRental.Service");

            // It's used for development, for real case we should use any real transport (RabbitMQ and so on).
            TransportExtensions<LearningTransport> transport = endpointConfiguration.UseTransport<LearningTransport>();

            UpdateableServiceProvider container = null;
            endpointConfiguration.UseContainer<ServicesBuilder>(customizations =>
            {
                customizations.ExistingServices(services);
                customizations.ServiceProviderFactory(serviceCollection =>
                {
                    container = new UpdateableServiceProvider(serviceCollection);
                    return container;
                });
            });

            RoutingSettings<LearningTransport> routing = transport.Routing();
            routing.RouteToEndpoint(
                assembly: typeof(GetInvoiceMessage).Assembly,
                destination: "Bondora.ConstructionEquipmentRental.Billing");

            _endpoint = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

            return container;
        }

        public void Configure(IApplicationBuilder app, IApplicationLifetime applicationLifetime, IHostingEnvironment env)
        {
            applicationLifetime.ApplicationStopping.Register(OnShutdown);

            app.UseMiddleware<RequestLoggingMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseSignalR(routes =>
            {
                routes.MapHub<InvoicesHub>("/invoiceshub");
            });
            app.UseMvc();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        private void OnShutdown()
        {
            _endpoint?.Stop().GetAwaiter().GetResult();
        }
    }
}
