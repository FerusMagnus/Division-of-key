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

        /// <summary>
        /// Класс, реализующий схемы разделения ключа.
        /// </summary>
        /// <param name="n">Количество сотрудников, разделяющих ключ.</param>
        /// <param name="p">Простое число.</param>
        /// <param name="key">Разделяемый ключ.</param>
        public KeyDivisionSchemes(int n, int p, int[] key)
        {
            _n = n;
            _p = p;
            _key = key;
        }

        /// <summary>
        /// Функция первичного разделения ключа.
        /// </summary>
        /// <param name="k"></param>
        /// <returns>Структура фрагментов первично разделённого ключа.</returns>
        private List<List<int>> FragmentationKey(int k)
        {
            Random rand = new Random();

            List<List<int>> keyFragments = new List<List<int>>();
            for (int i = 0; i < k; i++)
                keyFragments.Add(new List<int>());

            for (int i = 0; i < k; i++)
                for (int j = 0; j < _key.Length; j++)
                {
                    if (i == k - 1)
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

        /// <summary>
        /// Функция разделения ключа по мажоритарному принципу.
        /// </summary>
        /// <param name="h">Минимальный порог лиц для восстановления ключа.</param>
        /// <param name="k">Колличество фрагментов ключа.</param>
        /// <returns>Кортеж: 1) Структура фрагментов первично разделённого ключа; 2) Двумерный массив сгенерированных перестановок фрагментов ключа; 3) Структура фрагментов ключа, разделённых по сотрудникам.</returns>
        public (List<List<int>>, List<int[]>, List<List<List<int>>>) MajorityDivisionKey(int h, int k)
        {
            List<int[]> permutationList = new List<int[]>();

            // Фрагментируем ключ.
            List<List<int>> keyFragments = FragmentationKey(k);

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

                // Сравниваем перестановки и удаляем совпадающие более одного раза.
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

            // Собираем результат деления ключа.
            List<List<List<int>>> resultDivisionOfKey = new List<List<List<int>>>();
            for (int i = 0; i < _n; i++)
                resultDivisionOfKey.Add(new List<List<int>>());

            // Нулевая сторока, для навигации по разделённому ключу.
            List<int> zeroList = new List<int>() { 0 };

            for (int j = 0; j < _n; j++)
                for (int i = 0; i < k; i++)
                {
                    if (permutationList[i][j] == 1)
                        resultDivisionOfKey[j].Add(keyFragments[i]);
                    else
                        resultDivisionOfKey[j].Add(zeroList);
                }

            return (keyFragments, permutationList, resultDivisionOfKey);
        }

        public (List<List<int>>, List<int[]>, List<List<List<int>>>) EdgeDivisionKey(int h, int k)
        {
            List<int[]> permutationList = new List<int[]>();

            // Фрагментируем ключ.
            List<List<int>> keyFragments = FragmentationKey(k);

            // Собираем результат деления ключа.
            List<List<List<int>>> resultDivisionOfKey = new List<List<List<int>>>();

            return (keyFragments, permutationList, resultDivisionOfKey);
        }

        public int Mod(double a, double b)
        { 
            double q = Math.Floor(a / b);

            return (int)(a - b * q);
        }

        public int Factorial(int a)
        {
            if (a == 0)
                return 1;
            else
                return a * Factorial(a - 1);
        }
    }
}
