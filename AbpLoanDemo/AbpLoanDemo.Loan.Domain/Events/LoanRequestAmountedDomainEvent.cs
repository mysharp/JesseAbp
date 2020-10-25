using AbpLoanDemo.Loan.Domain.Entities;
using MediatR;

namespace AbpLoanDemo.Loan.Domain.Events
{
    public class LoanRequestAmountedDomainEvent : INotification
    {
        public LoanRequestAmountedDomainEvent(LoanRequest loanRequest)
        {
            LoanRequest = loanRequest;
        }

        public LoanRequest LoanRequest { get; }
    }
}