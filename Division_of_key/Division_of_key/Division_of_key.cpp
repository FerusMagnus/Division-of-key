// Division_of_key.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include "Schemes.h"
#include "Mathematics.h"

using namespace std;


int main()
{
	setlocale(LC_ALL, "");

	int n, h;
	bool flag = true;

	cout << "Добро пожаловать." << endl;

	do
	{
		cout << "Введите число сотрудников (n): ";
		cin >> n;

		cout << "Введите число сотрудников, восстанавливающих секрет (h): ";
		cin >> h;

		Schemes schema(n, h);	
		Mathematics metod;

		if (n == h)
		{
			int p, keySize;
			int *key;
			int **fragmentsKey;

			cout << "Введите простое число (p): ";
			cin >> p;

			cout << "Введите размер разделяемого ключа: ";
			cin >> keySize;
			key = new int[keySize];

			cout << "Введите " << keySize << " элементов ключа: ";
			for (int i = 0; i < keySize; i++)
				cin >> key[i];

			// Создание массива фрагментов ключей.
			fragmentsKey = new int *[h];
			for (int i = 0; i < h; i++)
				fragmentsKey[i] = new int[keySize];

			schema.First_Schema(fragmentsKey, p, keySize, key);

			cout << "Фрагменты разделяемого ключа:" << endl;
			for (int i = 0; i < h; i++)
			{
				for (int *j = fragmentsKey[i]; j != fragmentsKey[i] + keySize; j++)
					cout << *j << " ";

				cout << endl;
			}

			cout << "Проверка:" << endl;
			for (int j = 0; j < keySize; j++)
			{
				int buffer = 0;
				for (int i = 0; i < h; i++)
					buffer += fragmentsKey[i][j];

				cout << metod.Mod(buffer, p) << " ";
			}

			for (int i = 0; i < h; i++)
				delete[] fragmentsKey[i];

			delete[] key;
			delete[] fragmentsKey;

			cout << endl;
		}
		else
		{
			int c, k = h, sizeA = h;
			int *A = new int[sizeA];

			cout << "Последовательно введите " << n << " взаимнопростых чисел:" << endl;
			int buffer = 0;
			int *m = new int[n];
			for (int i = 0; i < n; i++)
			{
				cin >> buffer;

				if (i == 0)
					m[i] = buffer;
				else
				{
					// Вставить проверку на взаимную простоту
					if (metod.TestNOD(m[i - 1], buffer) == false)
					{
						cout << "Числа " << m[i - 1] << " и " << buffer << " не взаимнопростые, повторите попытку:" << endl;
						i--;
					}
					else
						m[i] = buffer;
				}
			}

			cout << "Введите разделяемый секрет (от " << metod.Max(k, n, m) << " до " << metod.Min(k, n, m) << "): ";
			cin >> c;

			cout << "Введите номера сотрудников восстанавливающих ключ: ";
			for (int i = 0; i < sizeA; i++)
				cin >> A[i];

			if (metod.TestConditions(c, k, n, m) == false)
			{
				delete[] A;
				delete[] m;
				cout << "Введённые данные не удовлетворяют условиям (1) и (2), повторите попытку." << endl;
				continue;
			}
			else
			{
				int x = schema.Second_Schema(m, c, k, sizeA, A);
				cout << "x = " << x << endl;

				buffer = 1;
				for (int i = 0; i < sizeA; i++)
					buffer *= m[A[i] - 1];

				cout << "Проверка секрета: " << metod.Mod(x, buffer) << endl;
			}
		}

		char ch;
		cout << "Хотите повторить? [y/n]: ";
		cin >> ch;

		if (ch == 'y')
			flag = true;
		else
			flag = false;
	} 
	while (flag);

    return 0;
}

