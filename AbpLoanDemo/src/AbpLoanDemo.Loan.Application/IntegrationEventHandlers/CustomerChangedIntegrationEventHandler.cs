using AbpLoanDemo.Customer.Application.Contracts.Models.Etos;
using AbpLoanDemo.Loan.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace AbpLoanDemo.Loan.Application.IntegrationEventHandlers
{
    public class CustomerChangedIntegrationEventHandler : IDistributedEventHandler<CustomerChangedEto>, ITransientDependency
    {
        private readonly IRepository<LoanRequest, Guid> _loanRequestRepository;

        public CustomerChangedIntegrationEventHandler(IRepository<LoanRequest, Guid> loanRequestRepository)
        {
            _loanRequestRepository = loanRequestRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        /// <remarks>
        ///     virtual：使用代理拦截必须要求方法virtual
        ///     UnitOfWork：会在方法退出前自动保存数据
        /// </remarks>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task HandleEventAsync(CustomerChangedEto eventData)
        {
            Console.WriteLine($"Received CustomerChangedIntegrationEvent: {eventData.Name}");

            var loanRequests = _loanRequestRepository.WithDetails().Where(p =>
                p.Applier.CustomerId == eventData.Id || p.Partners.Any(c => c.CustomerId == eventData.Id)).ToList();

            foreach (var loanRequest in loanRequests)
            {
                if (loanRequest.Applier.CustomerId == eventData.Id)
                    loanRequest.UpdateApplier(eventData.Name, eventData.Phone, eventData.IdNo);

                loanRequest.UpdatePartners(eventData.Id, eventData.Name, eventData.Phone, eventData.IdNo);

                await _loanRequestRepository.UpdateAsync(loanRequest);
            }
        }
    }
}