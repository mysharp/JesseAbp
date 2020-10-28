using System;
using System.Collections.Generic;
using AbpLoanDemo.Loan.Domain.Entities;

namespace AbpLoanDemo.Customer.Application.Contracts.Models.Dtos
{
    public class LoanRequestDto
    {
        public Guid Id { get; set; }

        public ApplierDto Applier { get; set; }

        public List<ApplierDto> Partners { get; set; }

        public LoanStatus Status { get; set; }

        public decimal Score { get; set; }

        public GuaranteeDto Guarantee { get; set; }

        public decimal Amount { get; set; }
    }
}