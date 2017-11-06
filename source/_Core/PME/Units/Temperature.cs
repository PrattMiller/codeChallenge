using System;
using Newtonsoft.Json;

namespace PME.Units
{
    public struct Temperature : IComparable<Temperature>, IComparable
    {

        private decimal _fahrenheit;
        private decimal _celsius;

        public static Temperature Zero = new Temperature(0m);
        
        private Temperature(decimal degreesFahrenheit)
        {
            _fahrenheit = degreesFahrenheit;
            _celsius = (5m / 9m) * (_fahrenheit - 32);
        }

        public decimal Fahrenheit
        {
            get { return _fahrenheit; }
        }

        public decimal Celsius
        {
            get { return _celsius; }
        }

        public TemperatureRange Range
        {
            get
            {
                if (_fahrenheit <= 20)
                {
                    return TemperatureRange.VeryCold;
                }
                else if (_fahrenheit <= 40)
                {
                    return TemperatureRange.Cold;
                }
                else if (_fahrenheit <= 75)
                {
                    return TemperatureRange.Normal;
                }
                else if (_fahrenheit <= 90)
                {
                    return TemperatureRange.Hot;
                }
                else
                {
                    return TemperatureRange.VeryHot;
                }
            }
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (!(obj is Temperature))
            {
                return false;
            }

            var right = (Temperature)obj;

            return _fahrenheit == right._fahrenheit;
        }

        public override int GetHashCode()
        {
            return _fahrenheit.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            if (obj.IsNull())
            {
                return 1;
            }

            if (obj is Temperature)
            {
                return CompareTo((Temperature)obj);
            }

            return 1;
        }

        public int CompareTo(Temperature other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            return _fahrenheit.CompareTo(other._fahrenheit);
        }

        public static bool operator ==(Temperature left, Temperature right)
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

            return left._fahrenheit == right._fahrenheit;
        }

        public static bool operator !=(Temperature left, Temperature right)
        {
            return !(left == right);
        }

        public static implicit operator Temperature(decimal value)
        {
            return new Temperature(value);
        }

        public static implicit operator decimal(Temperature temperature)
        {
            return temperature.Fahrenheit;
        }

        public bool Equals(Temperature other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return _fahrenheit.Equals(other._fahrenheit);
        }

        public override string ToString()
        {
            return string.Concat(
                _fahrenheit,
                " F"
                );
        }

        public string ToString(string formatter)
        {
            if (formatter.IsMissing())
            {
                return ToString();
            }

            if ("C".EqualsIgnoreCase(formatter))
            {
                return _celsius.ToString();
            }

            if ("F".EqualsIgnoreCase(formatter))
            {
                return _fahrenheit.ToString();
            }

            return _fahrenheit.ToString();
        }

        public static Temperature FromFahrenheit(decimal fahrenheit)
        {
            return new Temperature(fahrenheit);
        }

        public static Temperature FromCelsius(decimal celsius)
        {
            var fahrenheit = ((9m / 5m) * celsius) + 32;

            return new Temperature(fahrenheit);
        }

    }
}
