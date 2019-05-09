#pragma once
#include "DataBase.h"

class League : public DataBase
{
public:
	League(string filename);
	~League();

	bool add_sport(string name);//

	//league information**************************************************
	bool make_league(string league_name, string sport, string league);
	bool edit(string sport, string league_name, json input);
	bool del(string sport, string league_name);
	bool activeleague(string sport, string league_name);

	//team information****************************************************
	bool addteam(string sport, string league, string team_name, string team);
	bool edit_team(string sport, string league, string team_name, json input);
	bool del_team(string sport, string league, string team_name);
	bool activeteam(string sport, string league_name, string team_name);

	bool add_team_members(string sport, string league, string team_name, string user);
	bool del_team_members(string sport, string league, string team_name, string user);
	
	//competition information*********************************************
	bool make_competition(string sport, string league, string competition_name, json competition);
	bool edit_competition(string sport, string league, string competition_name, json input);
	bool del_competition(string sport, string league, string competition_name);
	bool activecompetition(string sport, string league_name, string competition_name);

	//5 23-25 24-26 29-27 25-10 10-15 2-3
	bool edit_result(string sport, string league, string competition_name, string result);
};
