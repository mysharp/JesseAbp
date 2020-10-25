using AbpLoanDemo.Customer.Domain.Entities;
using MediatR;

namespace AbpLoanDemo.Customer.Domain.Events
{
    public class CustomerLinkmanAddedDomainEvent : INotification
    {
        public CustomerLinkmanAddedDomainEvent(Entities.Customer customer, Linkman linkman)
        {
            Customer = customer;
            Linkman = linkman;
        }

        public  Entities.Customer Customer { get; }

        public Linkman Linkman { get; }
    }
}