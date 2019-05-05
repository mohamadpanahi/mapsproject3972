#include "User.h"

User::User(string filename) :DataBase(filename) {}
User::~User() {}

json User::search(string username)
{
	try
	{
		return j.at(username);
	}
	catch (...)
	{
		return 0;
	}
}

bool User::signup(string username, json person)
{
	if (search(username) != 0)
		return false;
	j.insert(person.begin(), person.end());
	return true;
}

bool User::signin(string username, string password)
{
	json temp = search(username);
	if (temp != 0 && temp["pass"] == password)
		return true;
	return false;
}

bool User::del(string username, string password)
{
	if (signin(username, password))
	{
		j.erase(username);
		return true;
	}
	return false;
}

bool User::edit(string username, string password, json input)
{
	if (signin(username, password))
	{
		j[username].update(input);
		return true;
	}
	return false;
}
