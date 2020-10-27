using MediatR;

namespace AbpLoanDemo.Customer.Domain.Events
{
    public class CustomerChangedDomainEvent : INotification
    {
        public CustomerChangedDomainEvent(Entities.Customer customer)
        {
            Customer = customer;
        }

        public Entities.Customer Customer { get; }
    }
}