using System;
using System.Threading;
using System.Threading.Tasks;
using AbpLoanDemo.Customer.Domain.Events;
using MediatR;

namespace AbpLoanDemo.Customer.Application.DomainEventHandlers
{
    public class CustomerLinkmanAddedDomainEventHandler : INotificationHandler<CustomerLinkmanAddedDomainEvent>
    {
        public Task Handle(CustomerLinkmanAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Add Linkman: {notification.Linkman.Name} to Customer: {notification.Customer.Name}");

            return Task.CompletedTask;
        }
    }
}