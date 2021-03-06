﻿using System;
using System.Collections.Generic;
using AbpLoanDemo.Customer.Domain.Events;
using AbpLoanDemo.Domain.Shared;

namespace AbpLoanDemo.Customer.Domain.Entities
{
    public class Customer : AggregateRootWithEvents<Guid>
    {
        private readonly List<Linkman> _linkman = new List<Linkman>();

        public Customer(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }

        public string Name { get; private set; }

        public string Phone { get; private set; }

        public string Address { get; private set; }

        public string IdNo { get; private set; }

        public IReadOnlyCollection<Linkman> Linkman => _linkman;

        public void Update(string name, string phone, string address, string idNo)
        {
            Name = name;
            Phone = phone;
            Address = address;
            IdNo = idNo;

            AddDomainEvent(new CustomerChangedDomainEvent(this));
        }

        public void AddLinkman(Linkman linkman)
        {
            _linkman.Add(linkman);

            AddDomainEvent(new CustomerLinkmanAddedDomainEvent(this, linkman));
        }
    }
}