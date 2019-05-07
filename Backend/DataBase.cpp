#include "DataBase.h"

string intTOstring(long long int a)
{
	if (a == 0)
		return "0";
	string s;
	if (a < 0)
		s = '-', a = -a;

	for (int l = pow(10, (int)log10(a)); l; a %= l, l /= 10)
		s += (a / l + '0');
	return s;
}

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

int DataBase::searcharr(json arr, string s)
{
	int k = 0;
	for (auto i : arr)
	{
		if (i == s)
			break;
		k++;
	}
	if (k < arr.size())
		return k;
	else
		return -1;
}
bool DataBase::find(const json& jj, string key)
{
	try
	{
		jj.at(key);
		return true;
	}
	catch (...)
	{
		return 0;
	}
}

string DataBase::getfreeid(const json& jj, long long int startid)
{
	for (int i = startid;; i++)
		if (!find(jj, intTOstring(i)))
			return intTOstring(i);
}
