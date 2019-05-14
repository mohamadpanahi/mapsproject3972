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

	virtual string basicbigsearch(json js, string path, string request) = 0;
	virtual string basicexactsearch(json js, string path, string request) = 0;
	virtual json big(string search) = 0;
	virtual json exact(string search) = 0;

	string getfreeid(const json& jj, long long int startid)const;

};
