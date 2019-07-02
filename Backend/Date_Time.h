#pragma once
#include "Usefull.h"

class Date_Time
{
	int year, mon, day, hour, min;
public:
	Date_Time(int y, int mo, int d, int h, int mi);
	~Date_Time();

	bool operator<(const Date_Time& t);
	bool operator>(const Date_Time& t);
	bool operator==(const Date_Time& t);
	bool operator>=(const Date_Time& t);
	bool operator<=(const Date_Time& t);
	static Date_Time now();
};

