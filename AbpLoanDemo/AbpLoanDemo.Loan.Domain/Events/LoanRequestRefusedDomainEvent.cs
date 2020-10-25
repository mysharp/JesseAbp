using AbpLoanDemo.Loan.Domain.Entities;
using MediatR;

namespace AbpLoanDemo.Loan.Domain.Events
{
    public class LoanRequestRefusedDomainEvent : INotification
    {
        public LoanRequestRefusedDomainEvent(LoanRequest loanRequest)
        {
            LoanRequest = loanRequest;
        }

        public LoanRequest LoanRequest { get; }
    }
}