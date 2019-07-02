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

json League::leaguenames()
{
	json result;

	for (auto iter = j.begin(); iter != j.end(); iter++)
		for (auto it = (*iter).begin(); it != (*iter).end(); it++)
			result[iter.key()].push_back(it.key());

	return result;
}
json League::leagueactivenames()
{
	json result = json::parse("{}");

	for (auto iter = j.begin(); iter != j.end(); iter++)
	{
		result[iter.key()] = json::parse("{}");
		for (auto it = (*iter).begin(); it != (*iter).end(); it++)
		{
			json t = json::parse("{\"" + it.key() + "\":" + ((*it)["active"] ? "true" : "false") + "}");
			string s = t.dump();
			result[iter.key()].insert(t.begin(), t.end());
		}
	}
	return result;
}
json League::sendrank(string sport, string league)
{
	if (!find(j, sport) || !find(j[sport], league))
		throw invalid_argument("sport or league doesn't exist! " + sport + "/" + league);

	json result = j[sport][league];
	json temp = result["team"];
	int size = temp.size();

	string* team = new string[size];
	int i = 0;
	for (auto it = temp.begin(); it != temp.end(); it++, i++)
		team[i] = it.key();

	sort(team, team + size, [temp](string s1, string s2) {return (temp[s1]["win"] * 3 + temp[s1]["equal"]) > (temp[s2]["win"] * 3 + temp[s2]["equal"]); });

	for (int i = 0; i < size; i++)
		result["rank"].push_back(team[i]);
	return result;
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
	//may construction of history throw
	History h(PATH_HISTORY);
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
	jj[team]["win"] = 0;
	jj[team]["lost"] = 0;
	jj[team]["equal"] = 0;

	//t1:{fault, wk, lk, lg, wg},t2:{fault, wk, lk, lg, wg}
	if (sport == "clp ba DFAgl")
	{
		jj[team]["fault"] = 0;
	}
	else if (sport == "wDdbaA jodF CabFCaFaC")
	{
		jj[team]["wg"] = 0;
		jj[team]["lg"] = 0;
	}
	else if (sport == "yaCdl pbyE")
	{
		jj[team]["wk"] = 0;
		jj[team]["lk"] = 0;
	}

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

bool League::add_competition(string sport, string league, string competition, string info)
{
	json input = json::parse("{\"" + competition + "\":{" + info + ",\"result\":false}}");
	string t1, t2;
	stringstream ss;
	ss << competition;
	getline(ss, t1, '-');
	getline(ss, t2, '-');

	if (!find(j, sport) || !find(j[sport], league) || !find(j[sport][league]["team"], t1) || !find(j[sport][league]["team"], t2) || find(j[sport][league]["competition"], competition))
		return false;

	//t1:{fault, wk, lk, lg, wg},t2:{fault, wk, lk, lg, wg}
	if (sport == "clp ba DFAgl")
	{
		input[competition]["t1"]["fault"] = 0;
		input[competition]["t2"]["fault"] = 0;
	}
	else if (sport == "wDdbaA jodF CabFCaFaC")
	{
		input[competition]["t1"]["wg"] = 0;
		input[competition]["t2"]["wg"] = 0;
		input[competition]["t1"]["lg"] = 0;
		input[competition]["t2"]["lg"] = 0;
	}
	else if (sport == "yaCdl pbyE")
	{
		input[competition]["t1"]["wk"] = 0;
		input[competition]["t2"]["wk"] = 0;
		input[competition]["t1"]["lk"] = 0;
		input[competition]["t2"]["lk"] = 0;
	}

	j[sport][league]["competition"].insert(input.begin(), input.end());
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
	j[sport][league]["competition"].erase(competition);
	return true;
}
bool League::active_competition(string sport, string league, string competition)
{
	if (!find(j, sport) || !find(j[sport], league) || !find(j[sport][league]["competition"], competition))
		return false;
	j[sport][league]["competition"][competition]["active"] = true;
	return true;
}
json League::unresulted_past_competition(string sport)
{
	if (!find(j, sport))
		throw invalid_argument("sport or league doesn't exist! " + sport);

	json result = json::parse("{}");

	for (auto l = j[sport].begin(); l != j[sport].end(); l++)
	{
		for (auto c = (*l)["competition"].begin(); c != (*l)["competition"].end(); c++)
		{
			if ((*c)["result"] == false && Date_Time((*c)["date"]["y"], (*c)["date"]["m"], (*c)["date"]["d"], (*c)["time"]["h"] + 2, (*c)["time"]["m"]) < Date_Time::now())
			{
				result[l.key()].push_back(c.key());
			}
		}
	}
	return result;
}

bool League::edit_result(string sport, string league, string competition, string result, string teaminfo)
{
	string t1, t2;
	stringstream ss;
	ss << competition;
	getline(ss, t1, '-');
	getline(ss, t2, '-');

	if (!find(j, sport) || !find(j[sport], league) || !find(j[sport][league]["competition"], competition) || !find(j[sport][league]["team"], t1) || !find(j[sport][league]["team"], t2))
		return false;

	int r1, r2;
	string s;
	stringstream rr;
	rr << result;
	getline(rr, s, '-');
	r1 = stoi(s);
	getline(rr, s, '-');
	r2 = stoi(s);

	//اعمال تغییرات برد و باخت
	if (j[sport][league]["competition"][competition]["result"] == false)
	{
		if (r1 > r2)
		{
			j[sport][league]["team"][t1]["win"] = j[sport][league]["team"][t1]["win"] + 1;
			j[sport][league]["team"][t2]["lost"] = j[sport][league]["team"][t2]["lost"] + 1;
		}
		else if (r1 < r2)
		{
			j[sport][league]["team"][t2]["win"] = j[sport][league]["team"][t2]["win"] + 1;
			j[sport][league]["team"][t1]["lost"] = j[sport][league]["team"][t1]["lost"] + 1;
		}
		else
		{
			j[sport][league]["team"][t1]["equal"] = j[sport][league]["team"][t1]["equal"] + 1;
			j[sport][league]["team"][t2]["equal"] = j[sport][league]["team"][t2]["equal"] + 1;
		}
	}
	else
	{
		string oldresult = j[sport][league]["competition"][competition]["result"];
		int or1, or2;
		stringstream orr;
		orr << oldresult;
		getline(orr, s, '-');
		or1 = stoi(s);
		getline(orr, s, '-');
		or2 = stoi(s);

		int oldwinner, newwinner;
		oldwinner = or1 - or2;
		newwinner = r1 - r2;

		if (oldwinner > 0)
		{
			if (newwinner == 0)
			{
				j[sport][league]["team"][t1]["win"] = j[sport][league]["team"][t1]["win"] - 1;
				j[sport][league]["team"][t2]["lost"] = j[sport][league]["team"][t2]["lost"] - 1;
				j[sport][league]["team"][t1]["equal"] = j[sport][league]["team"][t1]["equal"] + 1;
				j[sport][league]["team"][t2]["equal"] = j[sport][league]["team"][t2]["equal"] + 1;
			}
			else if (newwinner < 0)
			{
				j[sport][league]["team"][t1]["win"] = j[sport][league]["team"][t1]["win"] - 1;
				j[sport][league]["team"][t2]["lost"] = j[sport][league]["team"][t2]["lost"] - 1;
				j[sport][league]["team"][t1]["lost"] = j[sport][league]["team"][t1]["lost"] + 1;
				j[sport][league]["team"][t2]["win"] = j[sport][league]["team"][t2]["win"] + 1;
			}
		}
		else if (oldwinner < 0)
		{
			if (newwinner == 0)
			{
				j[sport][league]["team"][t1]["lost"] = j[sport][league]["team"][t1]["lost"] - 1;
				j[sport][league]["team"][t2]["win"] = j[sport][league]["team"][t2]["win"] - 1;
				j[sport][league]["team"][t1]["equal"] = j[sport][league]["team"][t1]["equal"] + 1;
				j[sport][league]["team"][t2]["equal"] = j[sport][league]["team"][t2]["equal"] + 1;
			}
			else if (newwinner > 0)
			{
				j[sport][league]["team"][t1]["lost"] = j[sport][league]["team"][t1]["lost"] - 1;
				j[sport][league]["team"][t2]["win"] = j[sport][league]["team"][t2]["win"] - 1;
				j[sport][league]["team"][t1]["win"] = j[sport][league]["team"][t1]["win"] + 1;
				j[sport][league]["team"][t2]["lost"] = j[sport][league]["team"][t2]["lost"] + 1;
			}
		}
		else//==
		{
			if (newwinner > 0)
			{
				j[sport][league]["team"][t1]["equal"] = j[sport][league]["team"][t1]["equal"] - 1;
				j[sport][league]["team"][t2]["equal"] = j[sport][league]["team"][t2]["equal"] - 1;
				j[sport][league]["team"][t1]["win"] = j[sport][league]["team"][t1]["win"] + 1;
				j[sport][league]["team"][t2]["lost"] = j[sport][league]["team"][t2]["lost"] + 1;
			}
			else if (newwinner < 0)
			{
				j[sport][league]["team"][t1]["equal"] = j[sport][league]["team"][t1]["equal"] - 1;
				j[sport][league]["team"][t2]["equal"] = j[sport][league]["team"][t2]["equal"] - 1;
				j[sport][league]["team"][t1]["lost"] = j[sport][league]["team"][t1]["lost"] + 1;
				j[sport][league]["team"][t2]["win"] = j[sport][league]["team"][t2]["win"] + 1;
			}
		}
	}

	//اعمال تغییرات اطلاعات تکمیلی
	//t1:{fault, wk, lk, lg, wg},t2:{fault, wk, lk, lg, wg}
	json info = json::parse("{" + teaminfo + "}");
	int diffrential;

	if (sport == "clp ba DFAgl")
	{
		diffrential = (int)info["t1"]["fault"] - (int)j[sport][league]["competition"][competition]["t1"]["fault"];
		j[sport][league]["competition"][competition]["t1"]["fault"] = info["t1"]["fault"];
		j[sport][league]["team"][t1]["fault"] = j[sport][league]["team"][t1]["fault"] + diffrential;

		diffrential = (int)info["t2"]["fault"] - (int)j[sport][league]["competition"][competition]["t2"]["fault"];
		j[sport][league]["competition"][competition]["t2"]["fault"] = info["t2"]["fault"];
		j[sport][league]["team"][t2]["fault"] = j[sport][league]["team"][t2]["fault"] + diffrential;
	}
	else if (sport == "wDdbaA jodF CabFCaFaC")
	{
		diffrential = (int)info["t1"]["wg"] - (int)j[sport][league]["competition"][competition]["t1"]["wg"];
		j[sport][league]["competition"][competition]["t1"]["wg"] = info["t1"]["wg"];
		j[sport][league]["team"][t1]["wg"] = j[sport][league]["team"][t1]["wg"] + diffrential;

		diffrential = (int)info["t1"]["lg"] - (int)j[sport][league]["competition"][competition]["t1"]["lg"];
		j[sport][league]["competition"][competition]["t1"]["lg"] = info["t1"]["lg"];
		j[sport][league]["team"][t1]["lg"] = j[sport][league]["team"][t1]["lg"] + diffrential;


		diffrential = (int)info["t2"]["wg"] - (int)j[sport][league]["competition"][competition]["t2"]["wg"];
		j[sport][league]["competition"][competition]["t2"]["wg"] = info["t2"]["wg"];
		j[sport][league]["team"][t2]["wg"] = j[sport][league]["team"][t2]["wg"] + diffrential;

		diffrential = (int)info["t2"]["lg"] - (int)j[sport][league]["competition"][competition]["t2"]["lg"];
		j[sport][league]["competition"][competition]["t2"]["lg"] = info["t2"]["lg"];
		j[sport][league]["team"][t2]["lg"] = j[sport][league]["team"][t2]["lg"] + diffrential;
	}
	else if (sport == "yaCdl pbyE")
	{
		diffrential = (int)info["t1"]["wk"] - (int)j[sport][league]["competition"][competition]["t1"]["wk"];
		j[sport][league]["competition"][competition]["t1"]["wk"] = info["t1"]["wk"];
		j[sport][league]["team"][t1]["wk"] = j[sport][league]["team"][t1]["wk"] + diffrential;

		diffrential = (int)info["t1"]["lk"] - (int)j[sport][league]["competition"][competition]["t1"]["lk"];
		j[sport][league]["competition"][competition]["t1"]["lk"] = info["t1"]["lk"];
		j[sport][league]["team"][t1]["lk"] = j[sport][league]["team"][t1]["lk"] + diffrential;


		diffrential = (int)info["t2"]["wk"] - (int)j[sport][league]["competition"][competition]["t2"]["wk"];
		j[sport][league]["competition"][competition]["t2"]["wk"] = info["t2"]["wk"];
		j[sport][league]["team"][t2]["wk"] = j[sport][league]["team"][t2]["wk"] + diffrential;

		diffrential = (int)info["t2"]["lk"] - (int)j[sport][league]["competition"][competition]["t2"]["lk"];
		j[sport][league]["competition"][competition]["t2"]["lk"] = info["t2"]["lk"];
		j[sport][league]["team"][t2]["lk"] = j[sport][league]["team"][t2]["lk"] + diffrential;
	}

	j[sport][league]["competition"][competition]["result"] = result;
	return true;
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
		//path = "0" or "f" or "c" that c=one charachter
		int i = path.length() - 1;
		for (; i >= 0 && path[i] != '/'; i--);
		path.erase(i);
		//if the above cindition is true this will throw out_of_range because i = -1
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
		//path = "0" or "f" or "c" that c=one charachter
		int i = path.length() - 1;
		for (; i >= 0 && path[i] != '/'; i--);
		path.erase(i);
		//if the above cindition is true this will throw out_of_range because i = -1
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
