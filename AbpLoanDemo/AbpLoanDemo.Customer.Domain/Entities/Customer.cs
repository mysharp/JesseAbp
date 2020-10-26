using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AbpLoanDemo.Customer.Domain.Events;
using AbpLoanDemo.Domain.Shared;

namespace AbpLoanDemo.Customer.Domain.Entities
{
    public class Customer : AggregateRootWithEvents<Guid>
    {
        private readonly List<Linkman> _linkman = new List<Linkman>();
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string IdNo { get; set; }

        public ReadOnlyCollection<Linkman> Linkman => _linkman.AsReadOnly();

        public void AddLinkman(Linkman linkman)
        {
            _linkman.Add(linkman);

            AddDomainEvent(new CustomerLinkmanAddedDomainEvent(this, linkman));
        }
    }
}