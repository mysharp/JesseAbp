using AbpLoanDemo.Loan.Domain.Entities;
using MediatR;

namespace AbpLoanDemo.Loan.Domain.Events
{
    public class LoanRequestApprovedDomainEvent : INotification
    {
        public LoanRequestApprovedDomainEvent(LoanRequest loanRequest)
        {
            LoanRequest = loanRequest;
        }

        public LoanRequest LoanRequest { get; }
    }
}