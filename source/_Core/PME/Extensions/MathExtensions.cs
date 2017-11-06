using System;
using System.Collections.Generic;
using System.Linq;

namespace PME
{
    public static class MathExtensions
    {

        public static double StandardDeviation(this IEnumerable<double> values)
        {
            if (values.IsNull())
            {
                return 0d;
            }

            double avg = values.Average();

            return Math.Sqrt(values.Average(x => Math.Pow(x - avg, 2)));
        }

    }
}

