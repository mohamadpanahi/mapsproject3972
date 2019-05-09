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

void DataBase::show(initializer_list<const char*> s, int len, int mlen, int n)
{
	n++;
	string temp;
	nlohmann::json::iterator it = j.begin();
	//Title
	cout << "key";
	for (int i = 3; i < len; i++)
		cout << ' ';
	int ttt = 0;
	for (auto i : s)
	{
		cout << i;
		for (int l = strlen(i); l < (ttt < s.size() - n ? len : mlen); l++)
			cout << ' ';
		ttt++;
	}
	cout << endl;
	//Json
	int t, tt = 0;
	for(auto k:j)
	{
		//Key
		temp = it.key();
		cout << temp;
		for (int i = temp.length(); i < len; i++)
			cout << ' ';

		//other
		tt = 0;
		for (auto i : s)
		{
			temp = k[i].dump();
			cout << temp;

			if (tt < s.size() - n)
				t = len;
			else t = mlen;
			for (int l = temp.length(); l < t; l++)
				cout << ' ';
			
			tt++;
		}
		cout << endl;
		it++;
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
