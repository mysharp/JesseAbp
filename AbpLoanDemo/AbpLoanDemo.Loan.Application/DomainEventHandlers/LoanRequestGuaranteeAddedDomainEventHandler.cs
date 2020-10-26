using System;
using System.Threading;
using System.Threading.Tasks;
using AbpLoanDemo.Loan.Domain.Events;
using MediatR;

namespace AbpLoanDemo.Loan.Application.DomainEventHandlers
{
    public class
        LoanRequestGuaranteeAddedDomainEventHandler : INotificationHandler<LoanRequestGuaranteeAddedDomainEvent>
    {
        public Task Handle(LoanRequestGuaranteeAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}