#pragma once
#include "DataBase.h"

class League : public DataBase
{
	string basicbigsearch(json js, string path, string request);
	string basicexactsearch(json js, string path, string request);

public:
	League(string filename);
	~League();

	bool add_sport(string name);

	//league information**************************************************
	bool add_league(string sport, string league, string input);
	bool edit_league(string sport, string league, string input);
	bool del_league(string sport, string league);
	bool active_league(string sport, string league);

	//team information****************************************************
	bool add_team(string sport, string league, string team, string input);
	bool edit_team(string sport, string league, string team, string input);
	bool del_team(string sport, string league, string team);
	bool active_team(string sport, string league, string team);

	bool add_team_members(string sport, string league, string team, string user);
	bool del_team_members(string sport, string league, string team, string user);
	
	//competition information*********************************************
	bool add_competition(string sport, string league, string competition, json input);
	bool edit_competition(string sport, string league, string competition, string input);
	bool del_competition(string sport, string league, string competition);
	bool active_competition(string sport, string league, string competition);

	//5 23-25 24-26 29-27 25-10 10-15 2-3
	bool edit_result(string sport, string league, string competition, string result);

	//search**************************************************************
	json big(string search);
	json exact(string search);
};
