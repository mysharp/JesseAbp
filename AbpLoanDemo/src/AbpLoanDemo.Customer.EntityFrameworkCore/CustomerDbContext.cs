using System.Threading;
using System.Threading.Tasks;
using AbpLoanDemo.Customer.Domain.Entities;
using AbpLoanDemo.EntityFrameworkCore.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace AbpLoanDemo.Customer.EntityFrameworkCore
{
    [ConnectionStringName("CustomerConnString")]
    public class CustomerDbContext : AbpDbContext<CustomerDbContext>
    {
        private readonly IMediator _mediator;

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Domain.Entities.Customer> Customers { get; set; }

        public DbSet<Linkman> Linkmen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureCustomerStore();

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            await _mediator.DispatchDomainEventsAsync(this, cancellationToken);

            return result;
        }
    }
}