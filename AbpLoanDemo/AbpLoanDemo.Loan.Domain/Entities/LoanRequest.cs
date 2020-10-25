using System;
using System.Collections.Generic;
using AbpLoanDemo.Domain.Shared;
using AbpLoanDemo.Loan.Domain.Events;

namespace AbpLoanDemo.Loan.Domain.Entities
{
    public class LoanRequest : AggregateRootWithEvents<Guid>
    {
        public Applier Applier { get; private set; }

        public IEnumerable<Applier> Partners { get; set; }

        public LoanStatus Status { get; private set; }

        public decimal Score { get; private set; }

        public Guarantee Guarantee { get; private set; }

        public decimal Amount { get; private set; }

        private LoanRequest()
        {
        }

        public LoanRequest(Applier applier)
        {
            Applier = applier;
            
            AddDomainEvent(new LoanRequestAddedDomainEvent(this));
        }

        public void SetScore(decimal score)
        {
            this.Score = score;
            if (score > 7.0m)
            {
                this.Status = LoanStatus.Approved;
                AddDomainEvent(new LoanRequestApprovedDomainEvent(this));
            }
            else
            {
                this.Status = LoanStatus.Refused;
                AddDomainEvent(new LoanRequestRefusedDomainEvent(this));
            }
        }

        public void SetGuarantee(Guarantee guarantee)
        {
            this.Guarantee = guarantee;
            this.Status = LoanStatus.Guaranteed;

            AddDomainEvent(new LoanRequestGuaranteeAddedDomainEvent(this));
        }

        public void SetAmount(decimal amount)
        {
            this.Amount = amount;
            this.Status = LoanStatus.Amounted;

            AddDomainEvent(new LoanRequestAmountedDomainEvent(this));
        }
    }
}