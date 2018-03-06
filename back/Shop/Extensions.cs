using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.ConsoleClient
{
    public static class Extensions
    {
        public static bool IsValidCollectionSelect(this string input, int collectionLength, out int select)
        {
            return int.TryParse(input, out select)
                && select >= 0
                && select - 1 < collectionLength;
        }
    }
}
