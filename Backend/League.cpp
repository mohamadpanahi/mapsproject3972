#include "League.h"

League::League(string filename) : DataBase(filename) {}
League::~League() {}

bool League::add_sport(string name)
{
	if (find(j, name))
		return false;

	json jj = json::parse("{\"" + name + "\":{}}");
	j.insert(jj.begin(), jj.end());
	return true;
}

bool League::add_league(string sport, string league, string input)
{
	if (!find(j, sport) || find(j[sport], league))
		return false;

	json jj = json::parse("{\"" + league + "\":{" + input + ",\"team\":{},\"competition\":{}}}");
	jj[league]["active"] = true;
	j[sport].insert(jj.begin(), jj.end());
	return true;
}
bool League::edit_league(string sport, string league, string input)
{
	if (!find(j[sport], league))
		return false;

	json jj = json::parse("{" + input + "}");
	j[sport][league].update(jj);
	return true;
}
bool League::del_league(string sport, string league)
{
	if (!find(j, sport) || !find(j[sport], league))
		return false;
	j[sport][league]["active"] = false;
	return true;
}
bool League::active_league(string sport, string league)
{
	//INACTIVE competitions ago
	if (!find(j, sport) || !find(j[sport], league))
		return false;
	j[sport][league]["active"] = true;
	return true;
}
bool League::end_league(string sport, string league)
{
	if (!find(j, sport) || !find(j[sport], league))
		return false;

	History h("h.json");
	h.add(sport, j[sport][league]);
	j[sport].erase(league);
	return true;
}


bool League::add_team(string sport, string league, string team, string input)
{
	if (!find(j, sport) || !find(j[sport], league) || find(j[sport][league]["team"], team))
		return false;

	json jj = json::parse("{\"" + team + "\":{" + input + ",\"member\":{}}}");
	jj[team]["active"] = true;
	j[sport][league]["team"].insert(jj.begin(), jj.end());
	return true;
}
bool League::edit_team(string sport, string league, string team, string input)
{
	if (!find(j, sport) || !find(j[sport], league) || !find(j[sport][league]["team"], team))
		return false;

	json jj = json::parse("{" + input + "}");

	j[sport][league]["team"][team].update(jj);
	return true;
}
bool League::del_team(string sport, string league, string team)
{
	if (!find(j, sport) || !find(j[sport], league) || !find(j[sport][league]["team"], team))
		return false;
	j[sport][league]["team"][team]["active"] = false;
	return true;
}
bool League::active_team(string sport, string league, string team)
{
	if (!find(j, sport) || !find(j[sport], league) || !find(j[sport][league]["team"], team))
		return false;
	j[sport][league]["team"][team]["active"] = true;
	return true;
}

bool League::add_team_members(string sport, string league, string team, string player)
{
	json jj = json::parse("{" + player + "}");
	if (!find(j, sport) || !find(j[sport], league) || !find(j[sport][league]["team"], team) || !find(j[sport][league]["team"][team]["member"], jj.begin().key()))
		return false;
	j[sport][league]["team"][team]["member"].insert(jj.begin(), jj.end());
	return true;
}
bool League::del_team_members(string sport, string league, string team, string player)
{
	json jj = json::parse("{" + player + "}");
	string key = jj.begin().key();
	if (!find(j, sport) || !find(j[sport], league) || !find(j[sport][league]["team"], team) || !find(j[sport][league]["team"][team]["member"], key))
		return false;
	
	j[sport][league]["team"][team]["member"].erase(key);
	return true;
}


bool League::add_competition(string sport, string league, string competition, json input)
{
	if (!find(j, sport) || !find(j[sport], league)  || !find(j[sport][league]["team"], input["team1"]) || !find(j[sport][league]["team"], input["team2"]))
		return false;
	
	string s = getfreeid(j[sport][league]["competition"], 1);
	json jj = json::parse("{\"" + s + "\":" + input.dump() + "}");
	jj[s]["active"] = true;
	j[sport][league]["competition"].insert(jj.begin(), jj.end());
	return true;
}
bool League::edit_competition(string sport, string league, string competition, string input)
{
	if (!find(j, sport) || !find(j[sport], league) || !find(j[sport][league]["competition"], competition))
		return false;

	json jj = json::parse("{" + input + "}");

	j[sport][league]["competition"][competition].update(jj);
	return true;
}
bool League::del_competition(string sport, string league, string competition)
{
	if (!find(j, sport) || !find(j[sport], league) || !find(j[sport][league]["competition"], competition))
		return false;
	j[sport][league]["competition"][competition]["active"] = false;
	return true;
}
bool League::active_competition(string sport, string league, string competition)
{
	if (!find(j, sport) || !find(j[sport], league) || !find(j[sport][league]["competition"], competition))
		return false;
	j[sport][league]["competition"][competition]["active"] = true;
	return true;
}

bool League::edit_result(string sport, string league, string competition, string result)
{
	if (!find(j, sport) || !find(j[sport], league) || !find(j[sport][league]["competition"], competition))
		return false;
	j[sport][league]["competition"][competition]["result"] = result;
}

string League::basicbigsearch(json js, string path, string request)
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
json League::bigsearch(string search)
{
	stringstream ss(basicbigsearch(j, "", search));
	string t;
	json result = json::parse("{\"team\":{},\"competition\":{},\"league\":{}}");

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

string League::basicexactsearch(json js, string path, string request)
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
json League::exactsearch(string search)
{
	stringstream ss(basicexactsearch(j, "", "\"" + search + "\""));
	string t;
	json result = json::parse("{\"team\":{},\"competition\":{},\"league\":{}}");

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

