#include "DataBase.h".
#include "Date_Time.h"
#include "League.h"
#include "User.h"
#include "MapsEncrypt.h"

void user(char* info[])
{
	User user(PATH_USER);
	if (!strcmp(info[0], "signin"))
	{
		cout << user.signin(info[1], info[2]); // username password
	}
	else if (!strcmp(info[0], "signup"))
	{
		cout << user.signup(info[1], info[2], info[3], correctargv(info[4]));// username password email otherinfo
	}
	else if (!strcmp(info[0], "find"))
	{
		cout << user.find(info[1]);// username
	}
	else if (!strcmp(info[0], "delete"))
	{
		cout << user.del(info[1], info[2]);// username password
	}
	else if (!strcmp(info[0], "edit"))
	{
		cout << user.edit(info[1], info[2], info[3]);// username password newinfo
	}
	else if (!strcmp(info[0], "active"))
	{
		cout << user.activation(info[1], info[2], info[3]);// username password activecode
	}
	else if (!strcmp(info[0], "generate"))
	{
		cout << user.generatecode(info[1], info[2]);// username password 
	}
	else if (!strcmp(info[0], "retrieve"))
	{
		cout << user.retrievepass(info[1], info[2]);// username email
	}
	else if (!strcmp(info[0], "addfavorite"))
	{
		cout << user.addfavorite(info[1], info[2], info[3], info[4]);// username password base id
	}
	else if (!strcmp(info[0], "delfavorite"))
	{
		cout << user.delfavorite(info[1], info[2], info[3], info[4]);// username password base id
	}
	else if (!strcmp(info[0], "show"))
	{
		user.show({ "name","pass","active","phone","email","favorite" }, 15, 30, 2);
	}
	else if (!strcmp(info[0], "showjs"))
	{
		user.show();
	}
	else
		throw invalid_argument("Error 300: invalid argument in user");
}
void sport(char* info[])
{
	League league(PATH_LEAGUE);

	if (!strcmp(info[0], "add"))
		cout << league.add_sport(info[1]);//sport
	else
		throw invalid_argument("Error 302: invalid argument in sport");
}
void league(char* info[])
{
	League league(PATH_LEAGUE);
	if (!strcmp(info[0], "add"))
	{
		cout << league.add_league(info[1], info[2], info[3]);//sport league info
	}
	else if (!strcmp(info[0], "delete"))
	{
		cout << league.del_league(info[1], info[2]);//sport league
	}
	else if (!strcmp(info[0], "active"))
	{
		cout << league.active_league(info[1], info[2]); //sport league
	}
	else if (strcmp(info[0], "edit"))
	{
		cout << league.edit_league(info[1], info[2], info[3]);// sport league newinfo
	}
	else if (!strcmp(info[0], "show"))
		league.show();
	else
		throw invalid_argument("Error 303: invalid argument in league");
	
}
void team(char* info[])
{
	League league(PATH_LEAGUE);
	if (!strcmp(info[0], "add"))
	{
		cout << league.add_team(info[1], info[2], info[3], info[4]);//sport league team info
	}
	else if (!strcmp(info[0], "delete"))
	{
		cout << league.del_team(info[1], info[2], info[3]);//sport league team
	}
	else if (!strcmp(info[0], "active"))
	{
		cout << league.active_team(info[1], info[2], info[3]);//sport league team
	}
	else if (strcmp(info[0], "edit"))
	{
		cout << league.edit_team(info[1], info[2], info[3], info[4]);//sport league team newinfo
	}
	else if (strcmp(info[0], "addmemmber"))
	{
		cout << league.add_team_members(info[1], info[2], info[3], info[4]);//sport league team player
	}
	else if (strcmp(info[0], "delmemmber"))
	{
		cout << league.del_team_members(info[1], info[2], info[3], info[4]);//sport league team player
	}
	else
		throw invalid_argument("Error 304: invalid argument in team");
}
void competition(char* info[])
{
	League league(PATH_LEAGUE);
	if (!strcmp(info[0], "add"))
	{
		cout << league.add_competition(info[1], info[2], info[3], json::parse("{" + (string)info[4] + "}"));//sport league competition info
	}
	else if (!strcmp(info[0], "delete"))
	{
		cout << league.del_competition(info[1], info[2], info[3]);//sport league competition
	}
	else if (!strcmp(info[0], "active"))
	{
		cout << league.active_competition(info[1], info[2], info[3]);//sport league competition
	}
	else if (!strcmp(info[0], "result"))
	{
		cout << league.edit_result(info[1], info[2], info[3], info[4]);//sport league competition result
	}
	else if (!strcmp(info[0], "edit"))
	{
		cout << league.edit_competition(info[1], info[2], info[3], info[4]);//sport league competition newinfo
	}
	else
		throw invalid_argument("Error 305: invalid argument in competition");
}

int main(int argc, char* argv[])
{
	try
	{
		if (!strcmp(argv[1], "user"))
			user(argv + 2);
		else if (!strcmp(argv[1], "sport"))
			sport(argv + 2);
		else if (!strcmp(argv[1], "league"))
			league(argv + 2);
		else if (!strcmp(argv[1], "team"))
			team(argv + 2);
		else if (!strcmp(argv[1], "competition"))
			competition(argv + 2);
		else
			throw invalid_argument("Error 301: invalid argument");
	}
	catch (exception & e)
	{
		cout << "Error 200: " << e.what();
		for (int i = 1; i < argc; i++)
			cout << "\nargv[" << i << "] -> " << argv[i];
		
	}
}

//a global exception : if string a = "asd2" then stoi(a) throws invalid_argument exception
//INACTIVE competitions ago -> league.cpp/active_league
//fault : active a team or competition that it's league is inactive or ended.
//date: year month day

//int main()
//{
//	char a[] = "$name$:$mohammad panahi$,$phone$:$09037351447$,$gender$:$male$,$isplayer$:false";
//	cout << correctargv(a);
//}