#pragma once
#include "DataBase.h"

class League : public DataBase
{
public:
	League(string filename);
	~League();

	//league information**************************************************
	bool make_league(string league_id, json league);
	json search(string league_id);
	bool del(string league_id);
	bool edit(string league_id, json input);
	

	//competition information*********************************************
	bool make_competition(string competition_id, json competition);
	bool del_competition(string competition_id);
	bool edit_result(string competition_id, string result);

	//team information****************************************************
	json search_team(string team_id);
	bool addteam(string team_id, json team);
	bool deel(string team_id);
	bool edit_team(string team_id, json input);

	bool add_team_members(string team_id, string id);
	bool del_team_members(string team_id, string id);

};
