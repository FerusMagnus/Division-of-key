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

