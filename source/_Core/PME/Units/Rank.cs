using System;
using Newtonsoft.Json;

namespace PME.Units
{
    public struct Rank : IEquatable<Rank>, IComparable<Rank>, IComparable
    {

        private int _rank;

        public static Rank First = new Rank { _rank = 0 };
        public static Rank Second = new Rank { _rank = 1 };
        public static Rank Third = new Rank { _rank = 2 };

        [JsonConstructor]
        private Rank(int value)
        {
            _rank = value;
        }

        public int Value
        {
            get { return _rank; }
        }

        public override int GetHashCode()
        {
            return _rank.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (obj is Rank)
            {
                _rank.Equals(((Rank)obj)._rank);
            }

            return false;
        }

        public bool Equals(Rank rank)
        {
            if (ReferenceEquals(rank, null))
            {
                return false;
            }

            return _rank.Equals(rank._rank);
        }

        public int CompareTo(object obj)
        {
            if (obj.IsNull())
            {
                return 1;
            }

            if (obj is Rank)
            {
                return CompareTo((Rank)obj);
            }

            return 1;
        }

        public int CompareTo(Rank other)
        {
            return _rank.CompareTo(other._rank);
        }

        public static bool operator ==(Rank v1, Rank v2)
        {
            if (ReferenceEquals(v1, null))
            {
                return ReferenceEquals(v2, null);
            }

            return v1.Equals(v2);
        }

        public static bool operator !=(Rank v1, Rank v2)
        {
            return !(v1 == v2);
        }

        public static implicit operator Rank(int value)
        {
            return new Rank(value);
        }

        public static implicit operator int(Rank rank)
        {
            return rank.Value;
        }

        public override string ToString()
        {
            var nPlusOne = _rank + 1;

            if (nPlusOne <= 0) return nPlusOne.ToString();

            switch (nPlusOne % 100)
            {
                case 11:
                case 12:
                case 13:
                    return nPlusOne + "th";
            }

            switch (nPlusOne % 10)
            {
                case 1:
                    return nPlusOne + "st";
                case 2:
                    return nPlusOne + "nd";
                case 3:
                    return nPlusOne + "rd";
                default:
                    return nPlusOne + "th";
            }
        }

        public static Rank From(int value)
        {
            return new Rank(value);
        }

    }
}