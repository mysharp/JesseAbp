using System;
using Volo.Abp.Domain.Entities;

namespace AbpLoanDemo.Loan.Domain.Entities
{
    public class Applier : Entity<Guid>
    {
        private Applier()
        {
        }

        public Applier(Guid customerId, string name, string phone, string idNo)
        {
            CustomerId = customerId;
            Name = name;
            Phone = phone;
            IdNo = idNo;
        }

        public Guid CustomerId { get; }

        public string Name { get; }

        public string Phone { get; }

        public string IdNo { get; }
    }
}