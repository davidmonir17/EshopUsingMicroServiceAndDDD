﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastracture.Data.Interceptors
{
    public class DispatchDomainEventInterceptor(IMediator mediator):SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvent(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
           await DispatchDomainEvent(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        public async Task DispatchDomainEvent(DbContext? context)
        {
            if (context == null) return;
           var aggregats= context.ChangeTracker
                .Entries<IAggregate>()
                .Where(a=>a.Entity.DomainEvents.Any())
                .Select(a=>a.Entity);
            
            var domainEvents = aggregats.SelectMany(a => a.DomainEvents).ToList();

            aggregats.ToList().ForEach(a => a.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
               await mediator.Publish(domainEvent);
            }
        }
    }
}
