using System;
using Volo.Abp.Domain.Entities;

namespace AbpLoanDemo.Customer.Domain.Entities
{
    public class Linkman : Entity<Guid>
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string IdNo { get; set; }
    }
}