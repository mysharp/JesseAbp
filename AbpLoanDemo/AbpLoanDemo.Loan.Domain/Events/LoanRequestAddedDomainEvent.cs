using AbpLoanDemo.Loan.Domain.Entities;
using MediatR;

namespace AbpLoanDemo.Loan.Domain.Events
{
    public class LoanRequestAddedDomainEvent : INotification
    {
        public LoanRequestAddedDomainEvent(LoanRequest loanRequest)
        {
            LoanRequest = loanRequest;
        }

        public LoanRequest LoanRequest { get; }
    }
}