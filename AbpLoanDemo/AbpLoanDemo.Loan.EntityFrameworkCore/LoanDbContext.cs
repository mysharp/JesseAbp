using System.Threading;
using System.Threading.Tasks;
using AbpLoanDemo.EntityFrameworkCore.Shared;
using AbpLoanDemo.Loan.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace AbpLoanDemo.Loan.EntityFrameworkCore
{
    [ConnectionStringName("LoanConnString")]
    public class LoanDbContext : AbpDbContext<LoanDbContext>
    {
        private readonly IMediator _mediator;

        public LoanDbContext(DbContextOptions<LoanDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<LoanRequest> LoanRequests { get; set; }

        public DbSet<Guarantee> Guarantees { get; set; }

        public DbSet<Applier> Appliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureLoanStore();

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            await _mediator.DispatchDomainEventsAsync(this);

            return result;
        }
    }
}