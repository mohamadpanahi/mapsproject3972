#pragma once
#include <nlohmann/json.hpp>
#include <iostream>
#include <string>
#include <fstream>
#include <sstream>
//#include <conio.h>
//#include <Windows.h>
//#include <stdlib.h>
//#include <stdio.h>
//#include <cstdlib>
//#include <ctime>
//#include <time.h>
//#include <vector>
//#include <list>
using json = nlohmann::json;
using namespace std;


class DataBase
{
	string filename;
protected:
	json j;
public:
	DataBase(string file);
	~DataBase();

	void show(initializer_list<const char*> s);
	bool sync();
	
};

