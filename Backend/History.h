#pragma once
#include "DataBase.h"

class History : public DataBase
{
	string basicbigsearch(json js, string path, string request);
	string basicexactsearch(json js, string path, string request);
public:
	History(string filname);
	~History();

	void add(string sport, json hist);
	//search**************************************************************
	json bigsearch(string search);
	json exactsearch(string search);
};
