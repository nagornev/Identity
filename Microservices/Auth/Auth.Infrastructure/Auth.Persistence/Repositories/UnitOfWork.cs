using Auth.Persistence.Contexts;
using Auth.Persistence.Entities;
using DDD.Primitives;
using DDD.Repositories;
using Newtonsoft.Json;

namespace Auth.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(CancellationToken cancellation = default)
        {
            var outboxMessages = _context.ChangeTracker.Entries<AggregateRoot>()
                                                       .Select(x => x.Entity)
                                                       .SelectMany(aggregateRoot =>
                                                       {
                                                           var domainEvents = aggregateRoot.GetDomainEvents();

                                                           aggregateRoot.ClearDomainEvents();

                                                           return domainEvents;
                                                       })
                                                       .Select(domainEvent =>
                                                               OutboxMessage.Create(domainEvent.AggregateId,
                                                                                    domainEvent.GetType().Name,
                                                                                    JsonConvert.SerializeObject(domainEvent,
                                                                                                                new JsonSerializerSettings
                                                                                                                {
                                                                                                                    TypeNameHandling = TypeNameHandling.All
                                                                                                                }),
                                                                                    domainEvent.OccurredOn))
                                                       .ToArray();

            await _context.Outbox.AddRangeAsync(outboxMessages);

            _ = await _context.SaveChangesAsync(cancellation);
        }
    }
}
