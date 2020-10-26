using System;
using System.Collections.Generic;

namespace AbpLoanDemo.Customer.Application.Contracts.Models.Dtos
{
    public class CustomerDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string IdNo { get; set; }

        public List<LinkmanDto> Linkmans { get; set; }
    }
}