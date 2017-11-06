using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace PME
{
    public static class ObjectExtensions
    {

        [DebuggerStepThrough]
        public static bool IsNull(this object input)
        {
            return ReferenceEquals(input, null);
        }

        [DebuggerStepThrough]
        public static bool IsNotNull(this object input)
        {
            return !ReferenceEquals(input, null);
        }
        
    }
}

