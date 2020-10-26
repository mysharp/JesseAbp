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

        public string Name { get; }

        public decimal Cost { get; }

        public DateTime ExpiryDate { get; }
    }
}