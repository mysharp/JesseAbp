using System;
using Volo.Abp.Domain.Entities;

namespace AbpLoanDemo.Customer.Domain.Entities
{
    public class Linkman : Entity<Guid>
    {
        private Linkman(string name, string phone, string idNo)
        {
            Name = name;
            Phone = phone;
            IdNo = idNo;
        }

        public string Name { get; private set; }

        public string Phone { get; private set; }

        public string IdNo { get; private set; }
    }
}