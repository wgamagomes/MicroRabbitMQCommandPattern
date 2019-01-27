using System;
using System.Collections.Generic;

namespace Domain.Core
{
    public class Entity
    {
        public Guid Id { get; protected set; }

        public override bool Equals(object other)
        {
            Entity casted = other as Entity;

            if (ReferenceEquals(this, casted))
                return true;

            if (ReferenceEquals(null, casted))
                return false;

            return Id.Equals(casted.Id);
        }

        public override int GetHashCode()
        {
            return 2108858624 + EqualityComparer<Guid>.Default.GetHashCode(Id);
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
                return true;

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
