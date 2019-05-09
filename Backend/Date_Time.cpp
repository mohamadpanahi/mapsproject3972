#include "Date_Time.h"


//1379/2/26 20:45
Date_Time::Date_Time(string date)
{
	int k = date.size();
	for (int i = 0; i < k; i++)
		if (date[i] == ':' || date[i] == '/')
			date[i] = ' ';
	stringstream ss;
	ss << date;
	ss >> year >> mon >> day >> hour >> min;
}
Date_Time::~Date_Time() {}

stringstream Date_Time::get()
{
	stringstream temp;
	temp << year << " " << mon << " " << day << " " << hour << " " << min;
	return temp;
}
string Date_Time::show()
{
	stringstream temp;
	temp << year << "/" << mon << "/" << day << " " << hour << ":" << min;
	return temp.str();
}



Date_Time Date_Time::operator-(const Date_Time& t)
{
	Date_Time result;
	return result;
}
