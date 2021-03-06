﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AbpLoanDemo.Loan.Domain.Events;
using MediatR;

namespace AbpLoanDemo.Loan.Application.DomainEventHandlers
{
    public class LoanRequestAmountedDomainEventHandler : INotificationHandler<LoanRequestAmountedDomainEvent>
    {
        public Task Handle(LoanRequestAmountedDomainEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine(
                $"Amounted LoanRequest: {notification.LoanRequest.Id}");

            return Task.CompletedTask;
        }
    }
}