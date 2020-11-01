using AbpLoanDemo.Customer.Application.Contracts.Models.Etos;
using AbpLoanDemo.Loan.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;

namespace AbpLoanDemo.Loan.Application.IntegrationEventHandlers
{
    public class CustomerChangedIntegrationEventHandler : IDistributedEventHandler<CustomerChangedEto>, ITransientDependency
    {
        private readonly IRepository<LoanRequest, Guid> _loanRequestRepository;

        public CustomerChangedIntegrationEventHandler(IRepository<LoanRequest, Guid> loanRequestRepository)
        {
            _loanRequestRepository = loanRequestRepository;
        }

        public async Task HandleEventAsync(CustomerChangedEto eventData)
        {
            Console.WriteLine($"Received CustomerChangedIntegrationEvent: {eventData.Name}");

            var loanRequests = _loanRequestRepository.WithDetails().Where(p =>
                p.Applier.CustomerId == eventData.Id || p.Partners.Any(c => c.CustomerId == eventData.Id)).ToList();

            foreach (var loanRequest in loanRequests)
            {
                if (loanRequest.Applier.CustomerId == eventData.Id)
                    loanRequest.UpdateApplier(eventData.Name, eventData.Phone, eventData.IdNo);

                loanRequest.UpdatePartners(eventData.Id, eventData.Name, eventData.Phone, eventData.IdNo);

                await _loanRequestRepository.UpdateAsync(loanRequest, true);
            }
        }
    }
}