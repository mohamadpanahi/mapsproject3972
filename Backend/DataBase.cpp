#include "DataBase.h"

DataBase::DataBase(string filename) : filename(filename)
{
	//INCOMPLETED DataBase: try catch
	ifstream file(filename);
	if (file)
	{
		stringstream ss;
		ss << file.rdbuf();
		//INCOMPLETED DataBase: try catch
		j = json::parse(ss.str());
	}
	else
		cout << "Error reading file :(";
}
DataBase::~DataBase() 
{
	sync();
}

void DataBase::show(initializer_list<const char*> s)
{
	for (auto i : s)
	{
		cout << i;
		for (int l = strlen(i); l < 20; l++)
			cout << ' ';
	}
	cout << endl;

	for(auto k:j)
	{
		for (auto i : s)
		{
			cout << k[i];
			for (int l = k[i].dump().length(); l < 20; l++)
				cout << ' ';
		}
		cout << endl;
	}
}

bool DataBase::sync()
{
	//INCOMPLETED DataBase: try catch
	ofstream file(filename);
	file << j;
	return true;
}
