using System.Collections.Generic;
using MediatR;

namespace AbpLoanDemo.Domain.Shared
{
    public interface IHasDomainEvent
    {
        IReadOnlyCollection<INotification> DomainEvents { get; }

        void ClearDomainEvents();
    }
}