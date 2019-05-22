#pragma once
#include "Usefull.h"
using namespace std;

class exFile :public exception
{
	string message;
public:
	exFile(const string& filename);
	~exFile();

	const char* what() const;
};
