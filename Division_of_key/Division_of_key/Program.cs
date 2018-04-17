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
            bool flag = true;

            Console.WriteLine("Добро пожаловать.");

            Console.Write("Введите число сотрудников (n): ");
            int n = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите простое число (p): ");
            int p = Convert.ToInt32(Console.ReadLine());           

            Console.Write("Введите разделяемый ключ через пробел: ");
            string[] input = Console.ReadLine().Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            int[] key = new int[input.Length];

            for (int i = 0; i < key.Length; i++)
                key[i] = Convert.ToInt32(input[i]);

            KeyDivisionSchemes schema = new KeyDivisionSchemes(n, p, key);           

            (List<List<int>>, List<int[]>, List<List<List<int>>>) turple = schema.MajorityDivisionKey();

            Console.WriteLine("Фрагменты разделяемого ключа:");
            for (int i = 0; i < turple.Item1.Count; i++)
            {
                for (int j = 0; j < turple.Item1[i].Count; j++)
                    Console.Write(turple.Item1[i][j] + " ");

                Console.WriteLine();
            }

            Console.WriteLine("Таблица сгенерированных равновестных кодов:");
            for (int i = 0; i < turple.Item2.Count; i++)
            {
                for (int j = 0; j < turple.Item2[i].Length; j++)
                    Console.Write(turple.Item2[i][j] + " ");

                Console.WriteLine();
            }

            Console.WriteLine("Фрагменты ключей для каждого сотрудника:");
            for (int tabCount = 0; tabCount < turple.Item3.Count; tabCount++)
            {
                Console.WriteLine("Фрагмент " + Convert.ToString(tabCount + 1) + "-го сотрудника");
                for (int i = 0; i < turple.Item3[tabCount].Count; i++)
                {
                    for(int j = 0; j < turple.Item3[tabCount][i].Count; j++)
                    {
                        Console.Write(turple.Item3[tabCount][i][j]  + " ");
                    }

                    Console.WriteLine();
                }
            }

            Console.ReadKey();
        }
    }
}
