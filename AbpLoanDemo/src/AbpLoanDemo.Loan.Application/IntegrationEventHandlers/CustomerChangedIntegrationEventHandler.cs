using System;
using System.Threading.Tasks;
using AbpLoanDemo.Customer.Application.Contracts.Models.Etos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace AbpLoanDemo.Loan.Application.IntegrationEventHandlers
{
    public class CustomerChangedIntegrationEventHandler : IDistributedEventHandler<CustomerChangedEto>, ITransientDependency
    {
        public Task HandleEventAsync(CustomerChangedEto eventData)
        {
            Console.WriteLine($"Received CustomerChangedIntegrationEvent: {eventData.Name}");

            return Task.CompletedTask;
        }
    }
}