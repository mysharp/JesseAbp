using AbpLoanDemo.Loan.Domain.Entities;
using MediatR;

namespace AbpLoanDemo.Loan.Domain.Events
{
    public class LoanRequestGuaranteeAddedDomainEvent : INotification
    {
        public LoanRequestGuaranteeAddedDomainEvent(LoanRequest loanRequest)
        {
            LoanRequest = loanRequest;
        }

        public LoanRequest LoanRequest { get; }
    }
}