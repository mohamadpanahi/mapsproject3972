#pragma once
#include "Usefull.h"

class DataBase
{
	string filename;
protected:
	json j;
public:
	DataBase(string file);
	~DataBase();

	void show(initializer_list<const char*> s, int len, int mlen = 25, int n = 2);
	bool sync();
	
	int searcharr(json arr, string s);
	bool find(const json& jj, string key);

	string getfreeid(const json& jj, long long int startid);
};
