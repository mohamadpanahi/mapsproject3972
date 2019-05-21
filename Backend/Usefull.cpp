#include "Usefull.h"

string intTOstring(long long int a)
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
string randcode(int len)
{
	string rnc;
	srand(time(NULL));
	for (int i = 0; i < len; i++)
		rnc += ((rand() / 56) % 9 + '1');
	return rnc;
}
void sendemail(string To, string subject, string text)
{
	string s = "node " + string(PATH_EMAIL) + " \"" + To + "\" \"" + subject + "\" \"" + text + "\"";
	system(s.c_str());
}
