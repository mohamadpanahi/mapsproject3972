#include "Useraccount.h"
#include <iostream>
using namespace std;

int main()
{
	//Useraccount u;
	//cout << boolalpha << u.editinfo("~", "12345678901", "panahi.7928@gmail.com");
	Server s;
	cout << s.getResponse("/?type=2000279");
}
