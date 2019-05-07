#pragma once
#include <iostream>
#include <string>
#include <sstream>
using namespace std;

class Date_Time
{
	int year, mon, day, hour, min;
public:
	Date_Time(string date);
	~Date_Time();

	stringstream get();
	string show();

	Date_Time operator+(const Date_Time& t);
	Date_Time operator-(const Date_Time& t);

};

