#include "Usefulls.h"

string intTOstring(long int a)
{
	if (a == 0)
		return "0";
	string s;
	if (a < 0)
		s = '-', a = -a;

	for (int l = pow(10, (int)log10(a)); l; a %= l, l /= 10)
		s += (a / l + '0');
	return s;
}


string addspace(string s)
{
	for (int i = 0; s[i] != '\0'; i++)
	{
		if (s[i] == '*')
			s[i] = ' ';
	}
	return s;
}
string removespace(string s)
{
	for (int i = 0; s[i] != '\0'; i++)
	{
		if (s[i] == ' ')
			s[i] = '*';
	}
	return s;
}
