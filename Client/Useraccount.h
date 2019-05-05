#pragma once
#include "Server.h"
#include <iostream>
#include <sstream>
#include "Usefulls.h"

using namespace std;

struct Userinfo
{
	long int id;
	string name, username, password, email, phone;
};

class Useraccount
{
	Server server;
	Userinfo userinfo;
public:
	bool signin(string username, string password);
	bool signup(string username, string password);
	bool _delete(string username, string password);
	bool edit(string oldusername, string oldpass, string username, string password);
	bool editinfo(string name, string phone, string email);

	Useraccount();
	~Useraccount();
};

