using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Division_of_key
{
    class KeyDivisionSchemes
    {
        int _n;
        int _p;
        int[] _key;

        public KeyDivisionSchemes(int n, int p, int[] key)
        {
            _n = n;
            _p = p;
            _key = key;
        }

        public int[,] FragmentationKey(int k)
        {
            Random rand = new Random();

            int[,] keyFragments = new int[k, _key.Length];

            for (int i = 0; i < k - 1; i++)
                for (int j = 0; j < _key.Length; j++)
                    keyFragments[i, j] = Mod(rand.Next(), _p);

            for (int j = 0; j < _key.Length; j++)
            {
                int buffer = _key[j];
                for (int i = 0; i < k - 1; i++)
                    buffer -= keyFragments[i, j];

                keyFragments[k - 1, j] = Mod(buffer, _p);
            }

            return keyFragments;
        }

        public () MajorityDivisionKey()
        {
            int h = (_n + 1) / 2;
            List<int[]> permutationList = new List<int[]>();

            // Создаём первую из возможных перестановок 
            // и определяем начальное количество единиц.
            int[] initCombination = new int[_n];
            for (int i = 0; i < h; i++)
                initCombination[i] = 1;

            // Номера символов в перестановке.
            int[] numbers = new int[_n];
            for (int i = 0; i < _n; i++)
                numbers[i] = i;

            Permutation permutation = new Permutation();

            // Генерим все возможные перестановки исходной комбинации 0 и 1.
            do
            {
                int[] buffer = new int[initCombination.Length];
                for (int i = 0; i < initCombination.Length; i++)
                    buffer[i] = initCombination[numbers[i]];

                permutationList.Add(buffer);
            }
            while (permutation.GenerationPermutation(ref numbers));

            // Удаляем повторяющиеся перестановки.
            for (int i = 0; i < permutationList.Count; i++)
            {
                int count = 0;

                for (int j = 0; j < permutationList.Count; j++)
                {
                    if (permutationList[i].SequenceEqual(permutationList[j]) && count == 0)
                        count++;
                    else if (permutationList[i].SequenceEqual(permutationList[j]) && count > 0)
                    {
                        permutationList.RemoveAt(j);
                        j--;
                    }
                }
            }
        }

        public int Mod(double a, double b)
        {
            double q = Math.Floor(a / b);

            return (int)(a - b * q);
        }
    }
}
