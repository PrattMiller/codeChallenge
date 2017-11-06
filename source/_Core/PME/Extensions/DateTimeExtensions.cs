using PME.Units;
using System;

namespace PME
{
    public static class DateTimeExtensions
    {

        public static DateTime RoundToNearest(this DateTime input, TimeSpan timeInterval)
        {
            // 1:59 becomes 2:00
            // 2:05 becomes 2:00
            // 2:08 becomes 2:15
            // 2:14 becomes 2:15

            if (timeInterval == TimeSpan.Zero)
            {
                throw new InvalidOperationException("Unable to round to zero.");
            }

            // half the interval for rounding purposes
            var half = timeInterval.Ticks / 2;

            // value of timespan left over
            var rm = input.Ticks % timeInterval.Ticks;

            var distanceToNext = timeInterval.Ticks - rm;
            var distanceToPrevious = rm;

            // If we are passed halfwas, then go to the next time interval, otherwise
            // go to the previous time interval
            if (rm > half)
            {
                return new DateTime(
                    input.Ticks + distanceToNext,
                    input.Kind
                    );
            }
            else
            {
                return new DateTime(
                    input.Ticks - distanceToPrevious,
                    input.Kind
                    );
            }
        }

        public static DateTime FloorTo(this DateTime input, TimeSpan timeInterval)
        {
            if (timeInterval == TimeSpan.Zero)
            {
                throw new InvalidOperationException("Unable to round to zero.");
            }
            var inputTicks = input.Ticks;
            var intervalTicks = timeInterval.Ticks;
            var mod = inputTicks % intervalTicks; //What we don't want in the result
            return input.AddTicks(-mod);
        }

    }
}

