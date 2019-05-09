#include "DataBase.h".
#include "Date_Time.h"
#include "League.h"
#include "User.h"


#include <conio.h>
/*
classes:
	Result -> number of sets & values of sets & final value(result) <-> stringconvertion
	Date_Time -> hour & minute  ^  Date -> year & month & day & compare <-> stringconvertion
	Place -> country & city & stadium <-> stringconvertion
*/
/*
void user(char* argv[])
{
	User u("j.json");

	if (!strcmp(argv[0], "signin"))
		cout << u.signin(argv[1], argv[2]);
	else if (!strcmp(argv[0], "signup"))
		cout << u.signup(argv[1], json::parse(argv[2]));
	else if (!strcmp(argv[0], "delete"))
		cout << u.del(argv[1], argv[2]);
	else if (!strcmp(argv[0], "edit"))
		cout << u.edit(argv[1], argv[2], json::parse(argv[3]));
	else if (!strcmp(argv[0], "addfav"))
		cout << u.addfavorite(argv[1], argv[2], argv[3]);
	else if (!strcmp(argv[0], "delfav"))
		cout << u.delfavorite(argv[1], argv[2], argv[3]);
	else if (!strcmp(argv[0], "show"))
		u.show({ "name","pass","phone","favorite","email" }, 15);
	else cout << "ERROR 404: not found :(";
}
void sport(char* argv[])
{
	League l("l.json");
	if (!strcmp(argv[0], "add"))
		cout << l.add_sport(argv[1]);
	else cout << "ERROR 404: not found :(";
}
void league(char* argv[])
{
	League l("l.json");
	if (!strcmp(argv[0], "make"))
		cout << l.make_league(argv[1], argv[2], argv[3]);
	else if (!strcmp(argv[0], "delete"));
	else if (!strcmp(argv[0], "edit"))
		cout << l.edit(argv[1], argv[2], json::parse(argv[3]));
	else cout << "ERROR 404: not found :(";
}
void team(char* argv[])
{
	League l("l.json");
	if (!strcmp(argv[0], "add"))
		cout << l.addteam(argv[1], argv[2], argv[3], argv[4]);
	else if (!strcmp(argv[0], "delete"));
	else if (!strcmp(argv[0], "edit"))
		cout << l.edit_team(argv[1], argv[2], argv[3], json::parse(argv[4]));
	else if (!strcmp(argv[0], "addmember"))
		cout << l.add_team_members(argv[1], argv[2], argv[3], argv[4]);
	else if (!strcmp(argv[0], "deletemember"))
		cout << l.del_team_members(argv[1], argv[2], argv[3], argv[4]);
	else cout << "ERROR 404: not found :(";
}
void competition(char* argv[])
{
	League l("l.json");
	if (!strcmp(argv[0], "make"))
		cout << l.make_competition(argv[1], argv[2], argv[3], json::parse(argv[4]));
	else if (!strcmp(argv[0], "delete"));
	else if (!strcmp(argv[0], "edit"));
	else if (!strcmp(argv[0], "result"));
	else cout << "ERROR 404: not found :(";
}
//main
*/
//int main(int argc, char* argv[])
//{
//	if (!strcmp(argv[1], "user"))
//		user(argv + 2);
//	else if (!strcmp(argv[1], "sport"))
//		sport(argv + 2);
//	else if (!strcmp(argv[1], "league"))
//		league(argv + 2);
//	else if (!strcmp(argv[1], "team"))
//		team(argv + 2);
//	else if (!strcmp(argv[1], "competition"))
//		competition(argv + 2);
//	else cout << "ERROR 404: not found :(";
//}


int main()
{
	//json j;
	//ifstream file("j.json");
	//stringstream ss;
	//ss << file.rdbuf();
	////INCOMPLETED DataBase: try catch
	//j = json::parse(ss.str());
	User user("j.json");
	string u, p, s, ss;
	char ch;
	cout << "i: sign in\nu: sign up\nd: delete\ne: edit\na: activation code\ng: generate activation code\nf: add favorite\nF: delete favorite\ns: show\nh: help\n";
	while (true)
	{
		ch = _getch();

		switch (ch)
		{
		case 'h':
			cout << "i: sign in\nu: sign up\nd: delete\ne: edit\na: activation code\ng: generate activation code\nf: add favorite\nF: delete favorite\ns: show\nh: help\n";
			break;
		case 'i':
			cout << "~~~~~~~~~~~ SIGN IN ~~~~~~~~~~~\n";
			cout << "username: ";
			cin >> u;
			cout << "password: ";
			cin >> p;
			cout << user.signin(u, p) << endl;
			break;
		case 'u':
			cout << "~~~~~~~~~~~ SIGN UP ~~~~~~~~~~~\n";
			cout << "username: ";
			cin >> u;
			cout << "password: ";
			cin >> p;
			cout << "email: ";
			cin >> s;
			cout << "other: ";
			cin >> ss;
			cout << user.signup(u, p, s, ss) << "please active your account :)\n";
			break;
		case 'd':
			cout << "~~~~~~~~~~~ DELETE ~~~~~~~~~~~~\n";
			cout << "username: ";
			cin >> u;
			cout << "password: ";
			cin >> p;
			cout << user.del(u, p) << endl;
			break;
		case 'e':
			cout << "~~~~~~~~~~~~ EDIT ~~~~~~~~~~~~~\n";
			cout << "username: ";
			cin >> u;
			cout << "password: ";
			cin >> p;
			cout << "new info: ";
			cin >> s;
			cout << user.edit(u, p, s) << endl;
			break;
		case 'a':
			cout << "~~~~~~~~~~~ ACTIVE ~~~~~~~~~~~~\n";
			cout << "username: ";
			cin >> u;
			cout << "password: ";
			cin >> p;
			cout << "code: ";
			cin >> s;
			cout << user.activation(u, p, s) << endl;
			break;
		case 'g':
			cout << "~~~~~~~~~~~ GENERATE ~~~~~~~~~~\n";
			cout << "username: ";
			cin >> u;
			cout << "password: ";
			cin >> p;
			cout << user.generatecode(u, p) << endl;
			break;
		case 'r':
			cout << "~~~~~~~~ RETRIEVE PASS ~~~~~~~~\n";
			cout << "username: ";
			cin >> u;
			cout << "email: ";
			cin >> s;
			cout << user.retrievepass(u, s) << endl;
			break;
		case 'f':
			cout << "~~~~~~~~~~~ ADD FAV ~~~~~~~~~~~\n";
			cout << "username: ";
			cin >> u;
			cout << "password: ";
			cin >> p;
			cout << "base: ";
			cin >> s;
			cout << "id: ";
			cin >> ss;
			
			cout << user.addfavorite(u, p, s, ss) << endl;
			break;
		case 'F':
			cout << "~~~~~~~~~~~ DEL FAV ~~~~~~~~~~~\n";
			cout << "username: ";
			cin >> u;
			cout << "password: ";
			cin >> p;
			cout << "base: ";
			cin >> s;
			cout << "id: ";
			cin >> ss;
			
			cout << user.delfavorite(u, p, s, ss) << endl;
			break;
		case 's':
			cout << "~~~~~~~~~~~~ SHOW ~~~~~~~~~~~~~\n";
			user.show({ "name","pass","active","phone","email","favorite" }, 15, 30, 2);
			break;
		}
	}
	

}
/*{"ali":{"email":"alii@ali.com","favorite":{"sport":["1"]},"name":"ali","pass":"123"},"aliop":{"name":"aliop","pass":"1234"},"aliop2":{"name":"aliop2","pass":"1234"},"hadi":{"active":"0","email":"alisalemmi@outlook.com","favorite":{"sport":[],"team":["3","5","7","11","2"]},"name":"hadi","pass":"456","phone":"12345678901"},"mahdi":{"name":"mahdi","pass":"456"},"mammad":{"name":"mammad","pass":"1234"},"seyed":{"name":"seyed","pass":"125"}}*/

