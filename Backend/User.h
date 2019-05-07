#pragma once
#include "DataBase.h"

class User :public DataBase
{
public:
	User(string filename);
	~User();

	//Users information
	bool signup(string username, json person);
	bool signin(string username, string password);
	bool del(string username, string password);
	bool edit(string username, string password, json input);

	//add favorite
	bool addfavorite(string username, string base, string id);
	bool delfavorite(string username, string base, string id);
	
};
