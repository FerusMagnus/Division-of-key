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

            // Фрагментируем ключ.
            int[,] keyFragments = schema.FragmentationKey();

            Console.WriteLine("Фрагменты разделяемого ключа:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write(keyFragments[i, j] + " ");

                Console.WriteLine();
            }

            Console.WriteLine("Проверка:");
            for (int j = 0; j < key.Length; j++)
            {
                int buffer = 0;
                for (int i = 0; i < n; i++)
                    buffer += keyFragments[i, j];

                Console.Write(schema.Mod(buffer, p) + " ");
            }

            schema.MajorityDivisionKey();

            Console.ReadKey();
        }
    }
}
