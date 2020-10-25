using System;
using System.Collections.Generic;
using MediatR;
using Volo.Abp.Domain.Entities;

namespace AbpLoanDemo.Domain.Shared
{
    public class AggregateRootWithEvents<TKey> : AggregateRoot<TKey>,IHasDomainEvent
    {
        private List<INotification> _domainEvents;

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        protected void AddDomainEvent(INotification evt)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(evt);
        }

        protected void RemoveDomainEvent(INotification evt)
        {
            _domainEvents?.Remove(evt);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
