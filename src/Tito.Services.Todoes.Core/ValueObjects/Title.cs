using System;
using System.Collections.Generic;
using System.Text;
using Tito.Services.Todoes.Core.Exceptions;

namespace Tito.Services.Todoes.Core.ValueObjects
{
    public class Title : IEquatable<Title>
    {
        public string Value { get; private set; }

        private Title()
        {

        }
        public Title(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
            {
                // throw a domain excwe
                throw new InvalidTitleException(value);
            }
            Value = value;
        }

        public static implicit operator Title(string title) => new Title(title);
        public static implicit operator string(Title title) => title.Value;

        public bool Equals(Title other)
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
            return base.Equals((Title)obj);
        }

        public override int GetHashCode()
        {
            return Value != null ? Value.GetHashCode() : 0;
        }
    }
}
