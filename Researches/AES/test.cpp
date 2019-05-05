#include <conio.h>
#include <iostream>
#include <string>
#include "AES.h"

using namespace std;

int main()
{
	string temp = "MAPS virtual preject to make POWERSHELL!";
	AES obj;
	while (1)
	{
		string t = obj.AESEncrypt(temp);
		cout << t << endl << obj.AESDecrypt(t) << endl << endl;
		getline(cin, temp);
	}
	return 0;
}