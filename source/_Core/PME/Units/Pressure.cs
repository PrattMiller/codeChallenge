using System;
using Newtonsoft.Json;

namespace PME.Units
{
    public struct Pressure : IComparable<Pressure>
    {

        private decimal _hectopascals;

        public static Pressure Zero = new Pressure(0m);
        
        private Pressure(decimal hectopascals)
        {
            _hectopascals = hectopascals;
        }

        public decimal Atmospheres
        {
            get { return Math.Round(_hectopascals * 0.000986923m, 6); }
        }
        
        public decimal Millibars
        {
            get { return _hectopascals; }
        }

        public decimal Pascals
        {
            get { return _hectopascals * 100; }
        }

        public decimal Hectopascals
        {
            get { return _hectopascals; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if ( !(obj is Pressure))
            {
                return false;
            }

            var right = (Pressure)obj;
            
            return _hectopascals == right._hectopascals;
        }

        public override int GetHashCode()
        {
            return _hectopascals.GetHashCode();
        }

        public int CompareTo(Pressure other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            return _hectopascals.CompareTo(other._hectopascals);
        }

        public static bool operator ==(Pressure left, Pressure right)
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

            return left._hectopascals == right._hectopascals;
        }

        public static bool operator !=(Pressure left, Pressure right)
        {
            return !(left == right);
        }

        public static implicit operator Pressure(decimal value)
        {
            return new Pressure(value);
        }

        public static implicit operator decimal(Pressure pressure)
        {
            return pressure.Hectopascals;
        }

        public bool Equals(Pressure other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return _hectopascals.Equals(other._hectopascals);
        }

        public override string ToString()
        {
            return string.Concat(
                _hectopascals,
                " mbar"
                );
        }
        
        public static Pressure FromMillibars(decimal millibars)
        {
            return new Pressure(millibars);
        }

        public static Pressure FromPascals(decimal pascals)
        {
            return new Pressure(pascals / 100);
        }

        public static Pressure FromHectopascals(decimal hectopascals)
        {
            return new Pressure(hectopascals);
        }

    }
}
