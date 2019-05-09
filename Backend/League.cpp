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

bool League::make_league(string league_name, string sport, string league)
{
	if (!find(j, sport) || find(j[sport], league_name))
		return false;

	json jj = json::parse("{\"" + league_name + "\":{" + league + ",\"team\":{},\"competition\":{}}}");
	jj["active"] = true;
	j[sport].insert(jj.begin(), jj.end());
	return true;
}
bool League::edit(string sport, string league_name, json input)
{
	if (!find(j[sport], league_name))
		return false;

	j[sport][league_name].update(input);
	return true;
}

bool League::del(string sport, string league_name)
{
	if (!find(j, sport) || !find(j[sport], league_name))
		return false;
	j[sport][league_name]["active"] = false;
	return true;
}
bool League::activeleague(string sport, string league_name)
{
	//INACTIVE competitions ago
	if (!find(j, sport) || !find(j[sport], league_name))
		return false;
	j[sport][league_name]["active"] = true;
	return true;
}

bool League::addteam(string sport, string league, string team_name, string team)
{
	if (!find(j, sport) || !find(j[sport], league) || find(j[sport][league]["team"], team_name))
		return false;

	json jj = json::parse("{\"" + team_name + "\":{" + team + ",\"member\":[]}}");
	jj["active"] = true;
	j[sport][league]["team"].insert(jj.begin(), jj.end());
	return true;
}
bool League::edit_team(string sport, string league, string team_name, json input)
{
	if (!find(j, sport) || !find(j[sport], league) || !find(j[sport][league]["team"], team_name))
		return false;

	j[sport][league]["team"][team_name].update(input);
	return true;
}

bool League::del_team(string sport, string league, string team_name)
{
	if (!find(j, sport) || !find(j[sport], league) || !find(j[sport][league]["team"], team_name))
		return false;
	j[sport][league]["team"][team_name]["active"] = false;
	return true;
}
bool League::activeteam(string sport, string league_name, string team_name)
{
	if (!find(j, sport) || !find(j[sport], league_name) || !find(j[sport][league_name]["team"], team_name))
		return false;
	j[sport][league_name]["team"][team_name]["active"] = true;
	return true;
}

bool League::add_team_members(string sport, string league, string team_name, string user)
{
	if (!find(j, sport) || !find(j[sport], league) || !find(j[sport][league]["team"], team_name) || searcharr(j[sport][league]["team"][team_name]["member"],user)!=-1)
		return false;
	j[sport][league]["team"][team_name]["member"].push_back(user);
	return true;
}
bool League::del_team_members(string sport, string league, string team_name, string user)
{
	if (!find(j, sport) || !find(j[sport], league) || !find(j[sport][league]["team"], team_name))
		return false;
	int k = searcharr(j[sport][league]["team"][team_name]["member"], user);
	if (k == -1)
		return false;

	j[sport][league]["team"][team_name]["member"].erase(j[sport][league]["team"][team_name]["member"].begin() + k);
	return true;
}

bool League::make_competition(string sport, string league, string competition_name, json competition)
{
	if (!find(j, sport) || !find(j[sport], league)  || !find(j[sport][league]["team"], competition["team1"]) || !find(j[sport][league]["team"], competition["team2"]))
		return false;
	
	json jj = json::parse("\"" + getfreeid(j[sport][league]["competition"], 1) + "\":" + competition.dump());
	jj["active"] = true;
	j[sport][league]["competition"].insert(jj.begin(), jj.end());
	return true;
}
bool League::edit_competition(string sport, string league, string competition_name, json input)
{
	if (!find(j, sport) || !find(j[sport], league) || !find(j[sport][league]["competition"], competition_name))
		return false;

	j[sport][league]["competition"][competition_name].update(input);
	return true;
}
bool League::del_competition(string sport, string league, string competition_name)
{
	if (!find(j, sport) || !find(j[sport], league) || !find(j[sport][league]["competition"], competition_name))
		return false;
	j[sport][league]["competition"][competition_name]["active"] = false;
	return true;
}
bool League::activecompetition(string sport, string league_name, string competition_name)
{
	if (!find(j, sport) || !find(j[sport], league_name) || !find(j[sport][league_name]["competition"], competition_name))
		return false;
	j[sport][league_name]["competition"][competition_name]["active"] = true;
	return true;
}

bool League::edit_result(string sport, string league, string competition_name, string result)
{
	if (!find(j, sport) || !find(j[sport], league) || !find(j[sport][league]["competition"], competition_name))
		return false;
	j[sport][league]["competition"][competition_name]["result"] = result;
}
