﻿using System;
using System.Collections.Generic;
using System.Linq;
using AbpLoanDemo.Domain.Shared;
using AbpLoanDemo.Loan.Domain.Events;

namespace AbpLoanDemo.Loan.Domain.Entities
{
    public class LoanRequest : AggregateRootWithEvents<Guid>
    {
        private readonly List<Applier> _partners = new List<Applier>();

        private LoanRequest()
        {
        }

        public LoanRequest(Applier applier)
        {
            Applier = applier;

            AddDomainEvent(new LoanRequestAddedDomainEvent(this));
        }

        public Applier Applier { get; private set; }

        public IReadOnlyCollection<Applier> Partners => _partners;

        public LoanStatus Status { get; private set; }

        public decimal Score { get; private set; }

        public Guarantee Guarantee { get; private set; }

        public decimal Amount { get; private set; }

        public void AddPartner(Applier partner)
        {
            _partners.Add(partner);
        }

        public void SetScore(decimal score)
        {
            Score = score;
            if (score > 7.0m)
            {
                Status = LoanStatus.Approved;
                AddDomainEvent(new LoanRequestApprovedDomainEvent(this));
            }
            else
            {
                Status = LoanStatus.Refused;
                AddDomainEvent(new LoanRequestRefusedDomainEvent(this));
            }
        }

        public void SetGuarantee(Guarantee guarantee)
        {
            Guarantee = guarantee;
            Status = LoanStatus.Guaranteed;

            AddDomainEvent(new LoanRequestGuaranteeAddedDomainEvent(this));
        }

        public void UpdatePartners(Guid customerId, string name, string phone, string idNo)
        {
            if (Status != LoanStatus.Request || Partners.Any() == false)
                return;

            foreach (var partner in _partners)
            {
                if (partner.CustomerId == customerId)
                {
                    partner.SetName(name);
                    partner.SetPhone(phone);
                    partner.SetIdNo(idNo);
                }
            }
        }

        public void UpdateApplier(string name, string phone, string idNo)
        {
            if(Status != LoanStatus.Request || Applier == null)
                return;
            
            Applier.SetName(name);
            Applier.SetPhone(phone);
            Applier.SetIdNo(idNo);
        }

        public void SetAmount(decimal amount)
        {
            Amount = amount;
            Status = LoanStatus.Amounted;

            AddDomainEvent(new LoanRequestAmountedDomainEvent(this));
        }
    }
}