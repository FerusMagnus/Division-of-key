#include "stdafx.h"
#include "Mathematics.h"


Mathematics::Mathematics()
{
}

int Mathematics::Mod(double a, double b)
{
	double q = floor(a / b);

	return (int)(a - b * q);
}

void Mathematics::SortMassive(bool flag, int size, int *mSort)
{
	for (int repeat_counter = 0; repeat_counter < size; repeat_counter++)
	{
		int temp = mSort[0]; // временная переменная для хранения значения перестановки
		for (int element_counter = repeat_counter + 1; element_counter < size; element_counter++)
		{
			if (flag)
			{
				if (mSort[repeat_counter] > mSort[element_counter])
				{
					temp = mSort[repeat_counter];
					mSort[repeat_counter] = mSort[element_counter];
					mSort[element_counter] = temp;
				}
			}
			else
				if (mSort[repeat_counter] < mSort[element_counter])
				{
					temp = mSort[repeat_counter];
					mSort[repeat_counter] = mSort[element_counter];
					mSort[element_counter] = temp;
				}
		}
	}
}

int Mathematics::Min(int k, int size, int *m)
{
	int min = 1;
	int *mSort = new int[size];
	for (int i = 0; i < size; i++)
		mSort[i] = m[i];

	SortMassive(true, size, mSort);

	for (int i = 0; i < k; i++)
		min *= mSort[i];

	delete[] mSort;
	return min;
}

int Mathematics::Max(int k, int size, int *m)
{
	int max = 1;
	int *mSort = new int[size];
	for (int i = 0; i < size; i++)
		mSort[i] = m[i];

	SortMassive(false, size, mSort);

	for (int i = 0; i < k - 1; i++)
		max *= mSort[i];

	delete[] mSort;
	return max;
}

bool Mathematics::TestNOD(int a, int b)
{
	while (a != 0 && b != 0)
	{
		if (a >= b)
			a = Mod(a, b);
		else
			b = Mod(b, a);
	}

	if ((a + b) == 1)
		return true;
	else
		return false;
}

bool Mathematics::TestConditions(int c, int k, int size, int *m)
{
	int min = Min(k, size, m);
	int max = Max(k, size, m);

	if (min - max >= 3 * max && max < c && c < min)
		return true;
	else
		return false;
}

int Mathematics::ExtendedNOD(int a, int b)
{
	int x, y, d;
	int q, r, x1, x2, y1, y2;

	if (b == 0)
	{
		x = 1;
		return x;
	}

	x1 = 0;
	x2 = 1;
	y1 = 1;
	y2 = 0;

	while (b > 0)
	{
		q = floor(a / b);
		r = a - q * b;

		x = x2 - q * x1;
		y = y2 - q * y1;

		a = b;
		b = r;

		x2 = x1;
		x1 = x;
		y2 = y1;
		y1 = y;
	}

	x = x2;
	return x;
}

Mathematics::~Mathematics()
{
}
