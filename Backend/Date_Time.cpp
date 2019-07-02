#include "Date_Time.h"

//1379/2/26 20:45
Date_Time::Date_Time(int y, int mo, int d, int h, int mi) :year(y), mon(mo), day(d), hour(h), min(mi) {}
Date_Time::~Date_Time() {}

bool Date_Time::operator<(const Date_Time& t)
{
	return (year < t.year 
		|| (year == t.year && (mon < t.mon 
			|| (mon == t.mon && (day < t.day 
				|| (day == t.day && (hour < t.hour 
					|| (hour == t.hour && (min < t.min)))))))));
}

bool Date_Time::operator>(const Date_Time& t)
{
	return (year > t.year
		|| (year == t.year && (mon > t.mon
			|| (mon == t.mon && (day > t.day
				|| (day == t.day && (hour > t.hour
					|| (hour == t.hour && (min > t.min)))))))));
}

bool Date_Time::operator==(const Date_Time& t)
{
	return (year == t.year && mon == t.mon && day == t.day && hour == t.hour && min == t.min);
}

bool Date_Time::operator>=(const Date_Time& t)
{
	return (year > t.year
		|| (year == t.year && (mon > t.mon
			|| (mon == t.mon && (day > t.day
				|| (day == t.day && (hour > t.hour
					|| (hour == t.hour && (min >= t.min)))))))));
}

bool Date_Time::operator<=(const Date_Time& t)
{
	return (year < t.year
		|| (year == t.year && (mon < t.mon
			|| (mon == t.mon && (day < t.day
				|| (day == t.day && (hour < t.hour
					|| (hour == t.hour && (min <= t.min)))))))));
}

Date_Time Date_Time::now()
{
	tm* timeinfo = new tm;
	time_t rawtime;
	time(&rawtime);
	localtime_s(timeinfo, &rawtime);
	return Date_Time(timeinfo->tm_year + 1900, timeinfo->tm_mon + 1, timeinfo->tm_mday, timeinfo->tm_hour, timeinfo->tm_min);
}
