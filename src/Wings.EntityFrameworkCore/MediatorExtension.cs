using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Wings.Domain.Entities;

namespace Wings.EntityFrameworkCore
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync<TContext>(this IMediator mediator, TContext context) where TContext : DbContext
        {
            var domainEntities = context.ChangeTracker
                .Entries<AggregateRoot>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
