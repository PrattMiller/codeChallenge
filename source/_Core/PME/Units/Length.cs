using System;
using Newtonsoft.Json;

namespace PME.Units
{
    public struct Length : IEquatable<Length>, IComparable<Length>, IComparable
    {
        private readonly decimal _inch;

        public static Length Zero = new Length(0);

        private Length(decimal inches)
        {
            if (inches < 0)
            {
                throw new ArgumentNullException("inches");
            }

            _inch = inches;
        }

        public decimal Inches
        {
            get
            {
                return _inch;
            }
        }

        public decimal Feet
        {
            get
            {
                return _inch / 12;
            }
        }

        public decimal Miles
        {
            get
            {
                return _inch / (12 * 5280);
            }
        }

        public static Length FromInches(decimal inches)
        {
            return new Length(inches);
        }

        public static Length FromFeet(decimal feet)
        {
            return new Length(feet * 12);
        }

        public static Length FromMiles(decimal miles)
        {
            return new Length(miles * 12 * 5280);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if ( !(obj is Length))
            {
                return false;
            }

            var right = (Length)obj;

            return _inch == right._inch;
        }

        public override int GetHashCode()
        {
            return _inch.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            if (obj.IsNull())
            {
                return 1;
            }

            if (obj is Length)
            {
                return CompareTo((Length)obj);
            }

            return 1;
        }

        public int CompareTo(Length other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            return _inch.CompareTo(other._inch);
        }

        public static Length operator +(Length left, Length right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
            {
                return Zero;
            }
            if (ReferenceEquals(left, null))
            {
                return right;
            }
            if (ReferenceEquals(right, null))
            {
                return left;
            }

            return new Length(left._inch + right._inch);
        }

        public static Length operator -(Length left, Length right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
            {
                return Zero;
            }
            if (ReferenceEquals(left, null))
            {
                throw new ArgumentNullException("left");
            }
            if (ReferenceEquals(right, null))
            {
                return left;
            }

            var difference = left._inch - right._inch;

            if (difference < 0)
            {
                return Zero;
            }

            return new Length(difference);
        }
        
        public static bool operator ==(Length left, Length right)
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

            return left._inch == right._inch;
        }

        public static bool operator !=(Length left, Length right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
            {
                return false;
            }
            if (ReferenceEquals(left, null))
            {
                return true;
            }
            if (ReferenceEquals(right, null))
            {
                return true;
            }

            return left._inch != right._inch;
        }

        public static implicit operator Length(decimal value)
        {
            return new Length(value);
        }

        public static implicit operator decimal(Length length)
        {
            return length.Inches;
        }

        public bool Equals(Length other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return _inch.Equals(other._inch);
        }

        public override string ToString()
        {
            if (_inch == 0)
            {
                return "0";
            }

            return string.Concat(
                _inch.ToString("0.000"),
                " in"
                );
        }

    }
}

