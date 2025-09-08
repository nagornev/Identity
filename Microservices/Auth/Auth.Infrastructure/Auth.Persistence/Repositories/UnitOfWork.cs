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
            LinkedList<OutboxMessage> outboxMessages = new LinkedList<OutboxMessage>();

            foreach (AggregateRoot aggregate in _context.ChangeTracker.Entries<AggregateRoot>()
                                                                     .Select(x => x.Entity))
            {
                foreach (OutboxMessage outboxMessage in aggregate.GetDomainEvents()
                                                                .Select(domainEvent => OutboxMessage.Create(domainEvent.AggregateId,
                                                                                                            domainEvent.GetType().Name,
                                                                                                            JsonConvert.SerializeObject(domainEvent,
                                                                                                                                        new JsonSerializerSettings
                                                                                                                                        {
                                                                                                                                            TypeNameHandling = TypeNameHandling.All
                                                                                                                                        }),
                                                                                                            domainEvent.OccurredOn)))
                {
                    outboxMessages.AddLast(outboxMessage);
                }
                aggregate.ClearDomainEvents();
            }

            await _context.Outbox.AddRangeAsync(outboxMessages);

            _ = await _context.SaveChangesAsync(cancellation);
        }
    }
}
