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

        public List<List<int>> FragmentationKey(int k)
        {
            Random rand = new Random();

            List<List<int>> keyFragments = new List<List<int>>();
            for (int i = 0; i < k; i++)
                keyFragments.Add(new List<int>());

            for (int i = 0; i < k; i++)
                for (int j = 0; j < _key.Length; j++)
                {
                    if(i == k - 1)
                        keyFragments[i].Add(0);
                    else
                        keyFragments[i].Add(Mod(rand.Next(), _p));
                }

            for (int j = 0; j < _key.Length; j++)
            {
                int buffer = _key[j];
                for (int i = 0; i < k - 1; i++)
                    buffer -= keyFragments[i][j];

                keyFragments[k - 1][j] = Mod(buffer, _p);
            }

            return keyFragments;
        }

        public (List<List<int>>, List<int[]>, List<List<List<int>>>) MajorityDivisionKey()
        {
            int h = (_n + 1) / 2;
            int k = Factorial(_n) / (Factorial((_n + 1) / 2) * Factorial((_n - 1) / 2));
            List<int[]> permutationList = new List<int[]>();

            // Фрагментируем ключ.
            List <List<int>> keyFragments = FragmentationKey(k);

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

            List<List<List<int>>> result = new List<List<List<int>>>();
            for (int i = 0; i < _n; i++)
                result.Add(new List<List<int>>());

            for (int i = 0; i < k; i++)
                result[i].Add(new List<int>());

            List<int> zeroList = new List<int>();
            for (int i = 0; i < keyFragments.Count; i++)
                zeroList.Add(0);

            for (int j = 0; j < k; j++)
                for (int i = 0; i < permutationList.Count; i++)
                {
                    if (permutationList[i][j] == 1)
                        result[i].Add(keyFragments[i]);
                    else
                        result[i].Add(zeroList);
                }

            return (keyFragments, permutationList, result);
        }

        public int Mod(double a, double b)
        {
            double q = Math.Floor(a / b);

            return (int)(a - b * q);
        }

        private int Factorial(int a)
        {
            if (a == 0)
                return 1;
            else
                return a * Factorial(a - 1);
        }
    }
}
