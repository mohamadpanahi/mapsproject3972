#pragma once
#include "DataBase.h"

class User :public DataBase
{
public:
	User(string filename);
	~User();

	//Users information

	//1: active 0: inactive -1: invalid username other : activation code
	int signin(string username, string password);
	bool signup(string username, string password, string email,  string otherinfo);
	bool del(string username, string password);
	bool edit(string username, string password, string input);

	bool generatecode(string username, string password);
	bool activation(string username, string password, string code);
	bool retrievepass(string username, string email);

	//add favorite
	bool addfavorite(string username, string password, string base, string id);
	bool delfavorite(string username, string password, string base, string id);
	
};
