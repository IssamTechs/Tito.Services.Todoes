using System;
using Tito.Services.Todoes.Core.Exceptions;

namespace Tito.Services.Todoes.Core.ValueObjects
{
    public class Description : IEquatable<Description>
    {
        public string Value { get; private set; }

        private Description()
        {
                
        }

        public Description(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
            {
                // throw a domain excwe
                throw new InvalidDescriptionException(value);
            }
            Value = value;
        }

        public static implicit operator Description(string description) => new Description(description);
        public static implicit operator string(Description description) => description.Value;

        public bool Equals(Description other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Value == this.Value;

        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return base.Equals((Description)obj);
        }

        public override int GetHashCode()
        {
            return Value != null ? Value.GetHashCode() : 0;
        }
    }
}
