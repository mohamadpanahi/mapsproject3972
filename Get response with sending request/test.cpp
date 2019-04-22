#include <curl/curl.h>
#include <string>
#include <iostream>
#include "Server.h"
using namespace std;

int main()
{
	Server server;
	cout << server.getResponse("/?appname=notepad&text=HeyHoo");
}