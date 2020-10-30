using AbpLoanDemo.Customer.Application.Contracts.Models.Etos;
using AbpLoanDemo.Customer.Domain.Events;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectMapping;

namespace AbpLoanDemo.Customer.Application.DomainEventHandlers
{
    public class CustomerChangedDomainEventHandler : INotificationHandler<CustomerChangedDomainEvent>
    {
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IObjectMapper<AppCustomerApplicationModule> _objectMapper;

        public CustomerChangedDomainEventHandler(IDistributedEventBus distributedEventBus,
            IObjectMapper<AppCustomerApplicationModule> objectMapper)
        {
            _distributedEventBus = distributedEventBus;
            _objectMapper = objectMapper;
        }

        public async Task Handle(CustomerChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Customer Changed: {notification.Customer.Name}");

            var eto = _objectMapper.Map<Domain.Entities.Customer, CustomerChangedEto>(notification.Customer);
            await _distributedEventBus.PublishAsync(eto);
        }
    }
}