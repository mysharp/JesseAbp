using System;
using System.Threading;
using System.Threading.Tasks;
using AbpLoanDemo.Loan.Domain.Events;
using MediatR;

namespace AbpLoanDemo.Loan.Application.DomainEventHandlers
{
    public class LoanRequestApprovedDomainEventHandler : INotificationHandler<LoanRequestApprovedDomainEvent>
    {
        public Task Handle(LoanRequestApprovedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}