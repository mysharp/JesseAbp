using System;

namespace AbpLoanDemo.Customer.Application.Contracts.Models.Dtos
{
    public class GuaranteeDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Cost { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}