using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Utils
    {
        public static bool IsBetween(this int value, int min, int max) 
        {
            bool isBetween = false;

            if(min <= value && value <= max) 
            { 
                isBetween = true;
            }

            return isBetween;
        }
    }
}
