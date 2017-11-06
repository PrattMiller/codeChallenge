using System;

namespace PME.Units
{
    public struct Percentage : IEquatable<Percentage>, IComparable<Percentage>, IComparable
    {
        private decimal _value;
        private int _intValue;

        public static Percentage Zero = new Percentage(0m);
        
        private Percentage(decimal value)
        {
            _value = value;
            _intValue = (int)(_value * 100);
        }

        public decimal Value
        {
            get
            {
                return _value;
            }
        }

        public int IntValue
        {
            get { return _intValue; }
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if ( !(obj is Percentage))
            {
                return false;
            }

            var right = (Percentage)obj;

            return _value == right._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(Percentage left, Percentage right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
            {
                return true;
            }
            if (ReferenceEquals(left, null))
            {
                return false;
            }
            if (ReferenceEquals(right, null))
            {
                return false;
            }

            return left._value == right._value;
        }

        public static bool operator !=(Percentage left, Percentage right)
        {
            return !(left == right);
        }

        public static implicit operator Percentage(decimal value)
        {
            return new Percentage(value);
        }

        public static implicit operator decimal(Percentage percentage)
        {
            return percentage.Value;
        }

        public bool Equals(Percentage other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return _value.Equals(other._value);
        }

        public int CompareTo(object obj)
        {
            if (obj.IsNull())
            {
                return 1;
            }

            if (obj is Percentage)
            {
                return CompareTo((Percentage)obj);
            }

            return 1;
        }

        public int CompareTo(Percentage other)
        {
            return _value.CompareTo(other._value);
        }

        public override string ToString()
        {
            return string.Concat(
                _intValue,
                "%"
                );
        }

        public static Percentage FromDecimal(decimal value)
        {
            return new Percentage(value);
        }

        public static Percentage FromInt(int value)
        {
            var trueValue = value / 100m;

            return new Percentage(trueValue);
        }

    }
}


