using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Division_of_key
{
    class Program
    {
        static void Main(string[] args)
        {
            (List<List<int>>, List<int[]>, List<List<List<int>>>) resultDivisionOfKey = (new List<List<int>>(), new List<int[]>(), new List<List<List<int>>>());
            int h = 0;
            int k = 0;

            Console.WriteLine("Добро пожаловать.");

            Console.Write("Введите число сотрудников (n): ");
            int n = Convert.ToInt32(Console.ReadLine());            

            Console.Write("Введите простое число (p): ");
            int p = Convert.ToInt32(Console.ReadLine());

            //Console.Write("Введите разделяемый ключ через пробел: ");
            //string[] input = Console.ReadLine().Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            //int[] key = new int[input.Length];
            int[] key = new int[] {26, 13, 21, 8, 0, 18};

            ////for (int i = 0; i < key.Length; i++)
            ////    key[i] = Convert.ToInt32(input[i]);

            KeyDivisionSchemes schema = new KeyDivisionSchemes(n, p, key);

            Console.Write("Введите используемую принцип разделения ключа  (m/e): ");
            char princip = Convert.ToChar(Console.ReadLine());

            if (princip.Equals('m'))
            {
                h = (n + 1) / 2;
                k = schema.Factorial(n) / (schema.Factorial((n + 1) / 2) * schema.Factorial((n - 1) / 2));
                resultDivisionOfKey = schema.MajorityDivisionKey(h, k);
            }
            else
            {
                h = (n + 1) / 2;
                k = schema.Factorial(n) / (schema.Factorial((n + 1) / 2) * schema.Factorial((n - 1) / 2));
                resultDivisionOfKey = schema.EdgeDivisionKey(h, k);
            }

            Console.WriteLine("Фрагменты разделяемого ключа:");
            for (int i = 0; i < resultDivisionOfKey.Item1.Count; i++)
            {
                for (int j = 0; j < resultDivisionOfKey.Item1[i].Count; j++)
                    Console.Write(resultDivisionOfKey.Item1[i][j] + " ");

                Console.WriteLine();
            }

            Console.WriteLine("Таблица сгенерированных равновестных кодов:");
            for (int i = 0; i < resultDivisionOfKey.Item2.Count; i++)
            {
                for (int j = 0; j < resultDivisionOfKey.Item2[i].Length; j++)
                    Console.Write(resultDivisionOfKey.Item2[i][j] + " ");

                Console.WriteLine();
            }

            Console.WriteLine("Фрагменты ключей для каждого сотрудника:");
            for (int numberUser = 0; numberUser < resultDivisionOfKey.Item3.Count; numberUser++)
            {
                Console.WriteLine("Фрагмент " + Convert.ToString(numberUser + 1) + "-го сотрудника");
                for (int i = 0; i < resultDivisionOfKey.Item3[numberUser].Count; i++)
                {
                    for (int j = 0; j < resultDivisionOfKey.Item3[numberUser][i].Count; j++)
                    {
                        Console.Write(resultDivisionOfKey.Item3[numberUser][i][j] + " ");
                    }

                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            //Console.Write("Введите номера тех, кто будет восстанавливать ключ (необходимо ввести " + h + " номеров через пробел): ");
            //input = Console.ReadLine().Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            //int[] numbersUsers = new int[input.Length];

            //for (int i = 0; i < numbersUsers.Length; i++)
            //    numbersUsers[i] = Convert.ToInt32(input[i]);
            int[] numbersUsers = new int[] { 0, 2, 3 };

            Console.WriteLine("Восстановленный ключ:");
            List<List<int>> recoveredKey = new List<List<int>>();
            for (int i = 0; i < numbersUsers.Length; i++)
            {
                for (int j = 0; j < resultDivisionOfKey.Item3[i].Count; j++)
                {
                    if (i == 0)
                    {
                        recoveredKey.Add(resultDivisionOfKey.Item3[numbersUsers[i]][j]);
                    }
                    else if (i != 0 && resultDivisionOfKey.Item3[numbersUsers[i]][j].Count != 1)
                    {
                        recoveredKey[j] = resultDivisionOfKey.Item3[numbersUsers[i]][j];
                    }
                }

            }

            Console.WriteLine();

            for (int j = 0; j < key.Length; j++)
            {
                int buffer = 0;
                for (int i = 0; i < recoveredKey.Count; i++)
                    buffer += recoveredKey[i][j];

                Console.Write(schema.Mod(buffer, p) + " ");
            }

            Console.ReadKey();
        }
    }
}
