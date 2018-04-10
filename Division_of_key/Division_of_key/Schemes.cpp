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

Schemes::~Schemes()
{
}
