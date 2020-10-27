using System;
using Volo.Abp.Domain.Entities;

namespace AbpLoanDemo.Loan.Domain.Entities
{
    public class Guarantee : Entity<Guid>
    {
        private Guarantee()
        {
        }

        public Guarantee(string name, decimal cost, DateTime expiryDate)
        {
            Name = name;
            Cost = cost;
            ExpiryDate = expiryDate;
        }

        public string Name { get; private set; }

        public decimal Cost { get; private set; }

        public DateTime ExpiryDate { get; private set; }
    }
}