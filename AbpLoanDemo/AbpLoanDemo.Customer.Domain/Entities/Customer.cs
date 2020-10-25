using AbpLoanDemo.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AbpLoanDemo.Customer.Domain.Events;

namespace AbpLoanDemo.Customer.Domain.Entities
{
    public class Customer : AggregateRootWithEvents<Guid>
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string IdNo { get; set; }


        private readonly List<Linkman> _linkman = new List<Linkman>();

        public ReadOnlyCollection<Linkman> Linkman => _linkman.AsReadOnly();

        public void AddLinkman(Linkman linkman)
        {
            this._linkman.Add(linkman);

            AddDomainEvent(new CustomerLinkmanAddedDomainEvent(this, linkman));
        }
    }
}