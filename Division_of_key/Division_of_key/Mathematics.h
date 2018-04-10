#pragma once
class Mathematics
{
public:
	Mathematics();

	int Mod(double, double);

	void SortMassive(bool, int, int*);

	int Min(int, int, int*);

	int Max(int, int, int*);

	bool TestNOD(int, int);

	bool TestConditions(int, int, int, int*);

	int ExtendedNOD(int, int);

	~Mathematics();
};

