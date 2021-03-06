﻿using System;
using System.Threading.Tasks;
using Bondora.ConstructionEquipmentRental.Repository;
using NServiceBus;

namespace Bondora.ConstructionEquipmentRental.Billing
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "ConstructionEquipmentRental.Billing";

            var endpointConfiguration = new EndpointConfiguration("Bondora.ConstructionEquipmentRental.Billing");

            // It's used for development, for real case we should use any real transport (RabbitMQ and so on).
            endpointConfiguration.UseTransport<LearningTransport>();

            endpointConfiguration.RegisterComponents(
                configureComponents =>
                {
                    configureComponents
                        .ConfigureComponent<IEquipmentRepository>(() => new EquipmentRepository(), DependencyLifecycle.SingleInstance);
                });

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
