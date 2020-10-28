using System;
using System.Threading;
using System.Threading.Tasks;
using AbpLoanDemo.Loan.Domain.Events;
using MediatR;

namespace AbpLoanDemo.Loan.Application.DomainEventHandlers
{
    public class LoanRequestRefusedDomainEventHandler : INotificationHandler<LoanRequestRefusedDomainEvent>
    {
        public Task Handle(LoanRequestRefusedDomainEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine(
                $"Refused LoanRequest: {notification.LoanRequest.Id}");

            return Task.CompletedTask;
        }
    }
}