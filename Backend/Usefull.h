#pragma once
#include <nlohmann/json.hpp>
#include <iostream>
#include <string>
#include <fstream>
#include <sstream>
#include <time.h>

using json = nlohmann::json;
using namespace std;

string intTOstring(long long int a);
string randcode(int len);

void sendemail(string To, string subject, string text);