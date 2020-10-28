using System;

namespace AbpLoanDemo.Customer.Application.Contracts.Models.Dtos
{
    public class ApplierDto
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string IdNo { get; set; }
    }
}