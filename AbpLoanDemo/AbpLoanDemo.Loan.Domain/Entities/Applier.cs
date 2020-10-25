using System;
using Volo.Abp.Domain.Entities;

namespace AbpLoanDemo.Loan.Domain.Entities
{
    public class Applier : Entity<Guid>
    {
        public string CustomerId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string IdNo { get; set; }
    }
}