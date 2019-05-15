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
	void show()const;
	bool sync()const;
	json jsonpath(string path)const;

	//search**************************************************************
	int searcharr(json arr, string s)const;
	bool find(const json& jj, string key)const;

	virtual json bigsearch(string search) = 0;
	virtual json exactsearch(string search) = 0;

	string getfreeid(const json& jj, long long int startid)const;

};
