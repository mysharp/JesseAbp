using System;
using System.Threading;
using System.Threading.Tasks;
using AbpLoanDemo.Customer.Domain.Events;
using MediatR;

namespace AbpLoanDemo.Customer.Application.DomainEventHandlers
{
    public class CustomerChangedDomainEventHandler : INotificationHandler<CustomerChangedDomainEvent>
    {
        public Task Handle(CustomerChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Customer Changed: {notification.Customer.Name}");

            return Task.CompletedTask;
        }
    }
}