#include "Useraccount.h"

bool Useraccount::signin(string username, string password)
{
	//INCOMPLETE_MAPSH : try catch
	string s = server.getResponse("/?type=usersignin&user=" + username + "&pass=" + password);
	if (s == "error")
	{
		//INCOMPLETE_MAPSH : انواع ارور
		return false;
	}
	else if (s != "0")
	{
		stringstream ss;
		ss << s;
		//INCOMPLETE_MAPSH : add phone, name, etc
		ss >> userinfo.username >> userinfo.password;
		return true;
	}
	return false;
}
//INCOMPLETE_MAPSH : add phone, name, etc
bool Useraccount::signup(string username, string password)
{
	//INCOMPLETE_MAPSH : try catch
	string s = server.getResponse("/?type=usersignup&user=" + username + "&pass=" + password);
	if (s == "error")
	{
		//INCOMPLETE_MAPSH : انواع ارور
		return false;
	}
	else if (s != "0")
	{
		//INCOMPLETE_MAPSH : add phone, name, etc
		userinfo.username = username;
		userinfo.password = password;

		return true;
	}
	return (s != "0");
}
bool Useraccount::_delete(string username, string password)
{
	//INCOMPLETE_MAPSH : try catch
	string s = server.getResponse("/?type=userdelete&user=" + username + "&pass=" + password);
	if (s == "error")
	{
		//INCOMPLETE_MAPSH : انواع ارور
		return false;
	}
	return (s != "0");
}
//INCOMPLETE_MAPSH : add phone, name, etc
bool Useraccount::edit(string oldusername, string oldpass, string username, string password)
{
	//INCOMPLETE_MAPSH : try catch
	string s = server.getResponse("/?type=useredit&olduser=" + oldusername + "&oldpass=" + oldpass + "&user=" + username + "&pass=" + password);
	if (s == "error")
	{
		//INCOMPLETE_MAPSH : انواع ارور
		return false;
	}
	else if (s != "0")
	{
		//INCOMPLETE_MAPSH : add phone, name, etc
		userinfo.username = username;
		userinfo.password = password;

		return true;
	}
	return (s != "0");
}
bool Useraccount::editinfo(string name, string phone, string email)
{
	//INCOMPLETE_MAPSH : try catch
	string s = server.getResponse("/?type=usereditinfo&name=" + removespace(name) + "&id=" + intTOstring(userinfo.id) + "&phone=" + phone + "&email=" + email);
	if (s == "error")
	{
		//INCOMPLETE_MAPSH : انواع ارور
		return false;
	}
	else if (s != "0")
	{
		//INCOMPLETE_MAPSH : add phone, name, etc
		userinfo.name = name;
		userinfo.phone = phone;
		userinfo.email = email;

		return true;
	}
	return (s != "0");
}

Useraccount::Useraccount()
{
}

Useraccount::~Useraccount()
{
}
