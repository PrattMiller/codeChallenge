using System;

namespace PME
{
    public static class NumericalExtensions
    {
        public static string ToUnicodeDirectionalArrow(this short degrees)
        {
            string unicodeFormat = String.Empty;

            if (degrees < 0)
            {
                throw new ArgumentNullException("degrees");
            }
            if (degrees > 360)
            {
                throw new ArgumentOutOfRangeException("degrees");
            }

            if(degrees == 360)
            {
                degrees = 0;
            }

            //N
            if (degrees > 337.5 || degrees <= 22.5)
                unicodeFormat = "\u2191";
            //NE
            else if (degrees > 22.5 && degrees <= 67.5)
                unicodeFormat = "\u2197";
            //E
            else if (degrees > 67.5 && degrees <= 112.5)
                unicodeFormat = "\u2192";
            //SE
            else if (degrees > 112.5 && degrees <= 157.5)
                unicodeFormat = "\u2198";
            //S
            else if (degrees > 157.5 && degrees <= 202.5)
                unicodeFormat = "\u2193";
            //SW
            else if (degrees > 202.5 && degrees <= 247.5)
                unicodeFormat = "\u2199";
            //W
            else if (degrees > 247.5 && degrees <= 292.5)
                unicodeFormat = "\u2190";
            //NW
            else if (degrees > 292.5 && degrees <= 337.5)
                unicodeFormat = "\u2196";

            return unicodeFormat;
        }

    }
}
