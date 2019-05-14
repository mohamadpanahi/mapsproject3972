#include "User.h"

User::User(string filename) :DataBase(filename) {}
User::~User() {}

int User::signin(string username, string password)
{
	if (find(j, username) && j[username]["pass"] == password)
	{
		if (j[username]["active"] == "1") //active
			return 1;
		else if (j[username]["active"] == "0") //inactive -> send email to active
		{
			generatecode(username, password);
			return 0;
		}
		else //wait for active
		{
			string s = j[username]["active"];
			return stoi(s);
		}
	}
	return -1;
}
bool User::signup(string username, string password, string email, string otherinfo)
{
	if (find(j, username))
		return false;

	json jj = json::parse("{\"" + username + "\":{" + otherinfo + "}}");
	jj[username]["active"] = "0";
	jj[username]["pass"] = password;
	jj[username]["email"] = email;

	j.insert(jj.begin(), jj.end());
	generatecode(username, password);
	return true;
}
bool User::del(string username, string password)
{
	if (signin(username, password) == 1)
	{
		j[username]["active"] = "0";
		return true;
	}
	return false;
}
bool User::edit(string username, string password, string input)
{
	if (signin(username, password) == 1)
	{
		json jj = json::parse("{" + input + "}");
		j[username].update(jj);
		return true;
	}
	return false;
}

bool User::generatecode(string username, string password)
{	
	string rnc = randcode(6);
	j[username]["active"] = rnc;
	// send rnc with email
	sendemail(j[username]["email"], "Activation code", rnc + " use this link to active your account: http://localhost:1379/?type=useractivation&user=" + username + "&code=" + rnc);
	return true;
}
bool User::activation(string username, string password, string code)
{
	int t = signin(username, password);
	if (t == -1)
		return false;
	else if (t == 1)
		return true;
	else if (t == 0)
	{
		generatecode(username, password);
		return false;
	}
	else if (j[username]["active"] == code)
	{
		j[username]["active"] = "1";
		return true;
	}
	else
		return false;
}
bool User::retrievepass(string username, string email)
{
	if (!find(j, username) || j[username]["email"] != email)
		return false;

	string s = randcode(10);
	sendemail(j[username]["email"], "Retrieve Password", "new password: " + s);
	j[username]["pass"] = s;
	return true;
}

bool User::addfavorite(string username, string password, string base, string id)
{
	if (signin(username, password) != 1 || searcharr(j[username]["favorite"][base], id) != -1)
		return false;

	j[username]["favorite"][base].push_back(id);
	return true;
}
bool User::delfavorite(string username, string password, string base, string id)
{
	if (signin(username, password) != 1)
		return false;
	int k = searcharr(j[username]["favorite"][base], id);
	if (k == -1)
		return false;
	j[username]["favorite"][base].erase(j[username]["favorite"][base].begin() + k);
	return true;
}

string User::basicbigsearch(json js, string path, string request)
{
	static string a = "";
	if (js.is_object())
	{
		auto it = js.begin();
		for (auto i : js)
		{
			if (i["isplayer"] == true)
			{
				basicbigsearch(i, path + "/" + it.key(), request);
				it++;
			}
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
json User::big(string search)
{
	stringstream ss(basicbigsearch(j, "", search));
	string t;
	json result;

	while (getline(ss, t))
	{
		json temp = jsonpath(t);
		if (t.find("/team/") != string::npos)
			result["team"].insert(temp.begin(), temp.end());
		else if (t.find("/competition/") != string::npos)
			result["competition"].insert(temp.begin(), temp.end());
		else
			result["league"].insert(temp.begin(), temp.end());
	}
	return result;
}

string User::basicexactsearch(json js, string path, string request)
{
	static string a = "";
	if (js.is_object())
	{
		auto it = js.begin();
		for (auto i : js)
		{
			if (i["isplayer"] == true)
			{
				basicbigsearch(i, path + "/" + it.key(), request);
				it++;
			}
		}
	}
	else
	{
		if (js.dump() == ("\"" + request + "\"")) //exact search
			a += (path + "\n");

		int i = path.length() - 1;
		for (; i >= 0 && path[i] != '/'; i--);
		path.erase(i);
	}
	return a;
}
json User::exact(string search)
{
	stringstream ss(basicexactsearch(j, "", search));
	string t;
	json result;

	while (getline(ss, t))
	{
		json temp = jsonpath(t);
		if (t.find("/team/") != string::npos)
			result["team"].insert(temp.begin(), temp.end());
		else if (t.find("/competition/") != string::npos)
			result["competition"].insert(temp.begin(), temp.end());
		else
			result["league"].insert(temp.begin(), temp.end());
	}
	return result;
}
