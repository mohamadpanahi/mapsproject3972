#pragma once
#include <string>
#include <curl/curl.h>
using namespace std;

class Server
{
	const string IP;
	const string port;

public:
	Server(string ip = "localhost", string p = "1379");
	~Server();

	string getResponse(string request);
};
