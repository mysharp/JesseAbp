using System;
using System.Threading;
using System.Threading.Tasks;
using AbpLoanDemo.Loan.Domain.Events;
using MediatR;

namespace AbpLoanDemo.Loan.Application.DomainEventHandlers
{
    public class LoanRequestAddedDomainEventHandler : INotificationHandler<LoanRequestAddedDomainEvent>
    {
        public Task Handle(LoanRequestAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine(
                $"Add LoanRequest: {notification.LoanRequest.Id} to Customer: {notification.LoanRequest.Applier.Name}");

            return Task.CompletedTask;
        }
    }
}