#pragma once
#include "DataBase.h"

class User :public DataBase
{
public:
	User(string filename);
	~User();

	//Users information
	json search(string username);
	bool signup(string username, json person);
	bool signin(string username, string password);
	bool del(string username, string password);
	bool edit(string username, string password, json input);

	//add favorite
};
