using MediatR;
using System;
using System.Collections.Generic;

namespace Wings.Domain.Entities
{
    public abstract class AggregateRoot : Entity, IAggregateRoot, IHasConcurrencyStamp
    {
        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();
        public virtual string ConcurrencyStamp { get; set; }

        protected AggregateRoot()
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
        }
        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
    public abstract class AggregateRoot<TKey> : AggregateRoot, IAggregateRoot<TKey>
    {
        public TKey Id { get; set; }
        protected AggregateRoot()
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
        }
    }
}
