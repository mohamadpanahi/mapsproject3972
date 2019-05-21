#pragma once
#include <nlohmann/json.hpp>
#include <iostream>
#include <string>
#include <fstream>
#include <sstream>
#include <time.h>
#include <exception>

//#include "exFile.h"


using json = nlohmann::json;
using namespace std;

constexpr auto PATH_EMAIL = "E:/PROJECT/emailjs/sever_P1_req/email.js";
constexpr auto PATH_USER = "E:/VS-projects/2019/query/j.json";
constexpr auto PATH_LEAGUE = "E:/VS-projects/2019/query/l.json";
constexpr auto PATH_HISTORY = "E:/VS-projects/2019/query/h.json";

string intTOstring(long long int a);
string randcode(int len);

void sendemail(string To, string subject, string text); 
