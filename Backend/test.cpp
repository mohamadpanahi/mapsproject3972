#include "DataBase.h".
#include "User.h"

int main()
{
	User m("j.json");
	m.show({ "name","pass","email" });
	cout << "\n\n";

	cout<<m.edit("ali", "123",json::parse("{\"email\":\"alii@ali.com\"}"));

	m.show({ "name","pass","email","phone" });
	//json j;
	//ifstream file("j.json");
	//if (file)
	//{
	//	stringstream ss;
	//	ss << file.rdbuf();
	//	//INCOMPLETED DataBase: try catch
	//	j = json::parse(ss.str());
	//}
}