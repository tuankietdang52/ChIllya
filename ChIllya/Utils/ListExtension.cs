using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya.Utils
{
    public static class ListExtension
    {
        public static bool IsEmpty<TItem>(this List<TItem> items)
        {
            return items.Count == 0;
        }

        /// <summary>
        /// Shuffle a List
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="items"></param>
        public static void Shuffle<TItem>(this List<TItem> items)
        {
            Random random = new();

            int n = items.Count;
            while (n > 1)
            {
                n--;
                int index = random.Next(n);
                (items[index], items[n]) = (items[n], items[index]);
            }
        }
    }
}
