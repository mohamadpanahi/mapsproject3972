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
	sendemail(j[username]["email"], "Activation code","This email has sent to you for your request to make account in MAPS.\\nIf you don't know about this ignore it.\\nActivation code: " + rnc + "\\nUse this link to active your account:\\nhttp://localhost:1379/?type=useractivation&user=" + username + "&code=" + rnc);
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

json User::bigsearch(string search)
{
	json result = json::parse("[]");
	for (auto i : j)
	{
		try
		{
			if (i["isplayer"] && i["name"].dump().find(search) != string::npos)
				result.push_back(i);
		}
		catch (...) {}
	}
	return result;
}
json User::exactsearch(string search)
{
	json result = json::parse("[]");
	for (auto i : j)
	{
		try
		{
			if (i["isplayer"] && i["name"] == search)
				result.push_back(i);
		}
		catch (...) {}
	}
	return result;
}
