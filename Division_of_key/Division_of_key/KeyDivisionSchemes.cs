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

        public int[,] FragmentationKey()
        {
            Random rand = new Random();

            int[,] keyFragments = new int[_n, _key.Length];

            for (int i = 0; i < _n - 1; i++)
                for (int j = 0; j < _key.Length; j++)
                    keyFragments[i, j] = Mod(rand.Next(), _p);

            for (int j = 0; j < _key.Length; j++)
            {
                int buffer = _key[j];
                for (int i = 0; i < _n - 1; i++)
                    buffer -= keyFragments[i, j];

                keyFragments[_n - 1, j] = Mod(buffer, _p);
            }

            return keyFragments;
        }

        public int Mod(double a, double b)
        {
            double q = Math.Floor(a / b);

            return (int)(a - b * q);
        }
    }
}
