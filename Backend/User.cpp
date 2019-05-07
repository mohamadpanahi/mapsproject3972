#include "User.h"

User::User(string filename) :DataBase(filename) {}
User::~User() {}

bool User::signup(string username, json person)
{
	if (find(j, username))
		return false;
	j.insert(person.begin(), person.end());
	return true;
}

bool User::signin(string username, string password)
{
	if (find(j, username) && j[username]["pass"] == password)
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

bool User::addfavorite(string username, string base, string id)
{
	if (searcharr(j[username]["favorite"][base], id) != -1)
		return false;

	j[username]["favorite"][base].push_back(id);
	return true;
}
bool User::delfavorite(string username, string base, string id)
{
	int k = searcharr(j[username]["favorite"][base], id);
	if (k == -1)
		return false;
	j[username]["favorite"][base].erase(j[username]["favorite"][base].begin() + k);
	return true;
}
