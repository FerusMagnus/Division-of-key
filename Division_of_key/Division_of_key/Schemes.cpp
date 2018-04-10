#include "stdafx.h"
#include "Schemes.h"
#include "Mathematics.h"

Schemes::Schemes(int n, int h)
{
	_n = n;
	_h = h;
}

Mathematics math;

void Schemes::First_Schema(int **fragments_key, int p, int keySize, int *key)
{
	for (int i = 0; i < _h - 1; i++)
		for (int j = 0; j < keySize; j++)
			fragments_key[i][j] = math.Mod(rand(), p);

	for (int j = 0; j < keySize; j++)
	{
		int buffer = key[j];
		for (int i = 0; i < _h - 1; i++)
			buffer -= fragments_key[i][j];

		fragments_key[_h - 1][j] = math.Mod(buffer, p);
	}
}

int Schemes::Second_Schema(int *m, int c, int k, int sizeA, int *A)
{
	// Заполняем массив фрагментов секрета.
	int *a = new int[_n];
	for (int i = 0; i < _n; i++)
		a[i] = math.Mod(c, m[i]);

	// Заполняем буферный массив со значениям m для вебранных участников.
	int *buffer = new int[_n]();
	for (int i = 0; i < sizeA; i++)
		buffer[A[i] - 1] = m[A[i] - 1]; // Все лишние mi заполняем нулями.

	// Заполняем массив M.
	int *M = new int[sizeA]();
	for (int i = 0; i < sizeA; i++)
	{
		M[i] = 1;
		for (int j = 0; j < _n; j++)
		{
			if (A[i] - 1 == j || buffer[j] == 0) // Берём m-ки которые не совпадают по номеру с текущим элементом M.
				continue;
			else
				M[i] *= buffer[j];
		}
	}

	// Заполняем массив N.
	int *N = new int[sizeA];
	for (int i = 0; i < sizeA; i++)
		N[i] = math.ExtendedNOD(M[i], m[A[i] - 1]);

	// Вычисляем х (общее решение сравнений)
	int x = 0;
	for (int i = 0; i < sizeA; i++)
		x += a[A[i] - 1] * M[i] * N[i];

	delete[] a;
	delete[] buffer;
	delete[] M;
	delete[] N;

	return x;
}

Schemes::~Schemes()
{
}
