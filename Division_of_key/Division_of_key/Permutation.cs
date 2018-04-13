using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Division_of_key
{
    class Permutation
    {
        private void swap(ref int[] str, int i, int j)
        {
            int s = str[i];
            str[i] = str[j];
            str[j] = s;
        }

        public bool GenerationPermutation(ref int[] str)
        {
            int j = str.Length - 2;

            while (j != -1 && str[j] >= str[j + 1]) j--;
            if (j == -1)
                return false; // больше перестановок нет

            int k = str.Length - 1;

            while (str[j] >= str[k]) k--;
            swap(ref str, j, k);

            int l = j + 1, r = str.Length - 1; // сортируем оставшуюся часть последовательности

            while (l < r)
                swap(ref str, l++, r--);

            return true;
        }
    }
}
