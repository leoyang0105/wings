using System;

namespace Wings.Domain.Entities
{
    public abstract class AggregateRoot : IAggregateRoot, IHasConcurrencyStamp
    {
        public virtual string ConcurrencyStamp { get; set; }
        protected AggregateRoot()
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
        }
    }
    public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>, IHasConcurrencyStamp
    {
        public virtual string ConcurrencyStamp { get; set; }

        protected AggregateRoot()
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
        }
        protected AggregateRoot(TKey id) : base(id)
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
        }
    }
}
