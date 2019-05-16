#include "History.h"

History::History(string filename) : DataBase(filename) {}
History::~History() {}

void History::add(string sport, json hist)
{
	j[sport].push_back(hist);
	cout << j << endl;
}

string History::basicbigsearch(json js, string path, string request)
{
	static string a = "";
	if (js.is_object())
	{
		auto it = js.begin();
		for (auto i : js)
		{
			basicbigsearch(i, path + "/" + it.key(), request);
			it++;
		}
	}
	else if (js.is_array())
	{
		int k = 0;
		for (auto i : js)
		{
			basicbigsearch(i, path + "/~" + intTOstring(k), request);
			k++;
		}
	}
	else
	{
		if (js.dump().find(request) != string::npos) //big search
			a += (path + "\n");

		int i = path.length() - 1;
		for (; i >= 0 && path[i] != '/'; i--);
		path.erase(i);
	}
	return a;
}
json History::bigsearch(string search)
{
	stringstream ss(basicbigsearch(j, "", search));
	string t;
	json result = json::parse("{\"hteam\":{},\"hcompetition\":{},\"hleague\":{}}");

	while (getline(ss, t))
	{
		json temp = jsonpath(t);
		if (t.find("/team/") != string::npos)
			result["hteam"].insert(temp.begin(), temp.end());
		else if (t.find("/competition/") != string::npos)
			result["hcompetition"].insert(temp.begin(), temp.end());
		else
			result["hleague"].insert(temp.begin(), temp.end());
	}
	return result;
}

string History::basicexactsearch(json js, string path, string request)
{
	static string a = "";
	if (js.is_object())
	{
		auto it = js.begin();
		for (auto i : js)
		{
			basicexactsearch(i, path + "/" + it.key(), request);
			it++;
		}
	}
	else if (js.is_array())
	{
		int k = 0;
		for (auto i : js)
		{
			basicexactsearch(i, path + "/~" + intTOstring(k), request);
			k++;
		}
	}
	else
	{
		if (js.dump() == request) //exact search
			a += (path + "\n");

		int i = path.length() - 1;
		for (; i >= 0 && path[i] != '/'; i--);
		path.erase(i);
	}
	return a;
}
json History::exactsearch(string search)
{
	stringstream ss(basicexactsearch(j, "", "\"" + search + "\""));
	string t;
	json result = json::parse("{\"hteam\":{},\"hcompetition\":{},\"hleague\":{}}");

	while (getline(ss, t))
	{
		json temp = jsonpath(t);
		if (t.find("/team/") != string::npos)
			result["hteam"].insert(temp.begin(), temp.end());
		else if (t.find("/competition/") != string::npos)
			result["hcompetition"].insert(temp.begin(), temp.end());
		else
			result["hleague"].insert(temp.begin(), temp.end());
	}
	return result;
}
