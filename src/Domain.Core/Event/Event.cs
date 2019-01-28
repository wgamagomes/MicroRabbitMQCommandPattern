using System;

namespace Domain.Core
{
    public abstract class Event
    {
        protected Event()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; }

        public DateTime CreatedAt { get; }

        public abstract bool IsValid();
    }
}
