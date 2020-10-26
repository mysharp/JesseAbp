using System;

namespace AbpLoanDemo.Customer.Application.Contracts.Models.Dtos
{
    public class LoanRequestCreateDto
    {
        public Guid CustomerId { get; set; }
    }
}