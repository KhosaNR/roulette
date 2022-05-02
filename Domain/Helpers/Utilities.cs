using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    internal static class Utilities
    {
        private const int minimumRangeValue = 0;
        private const int maximumRangeValue = 36;
        internal static int GenerateRandomNumberWithinRange()
        {
            var random = new Random();
            var rNum = random.Next(minimumRangeValue, maximumRangeValue);
            return rNum;
        }
    }
}
