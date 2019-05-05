#include "Server.h"

Server::Server(string ip, string p) :IP(ip), port(p) {}
Server::~Server() {}

size_t writeFunction(void* ptr, size_t size, size_t nmemb, string* data)
{
	data->append((char*)ptr, size * nmemb);
	return size * nmemb;
}
string Server::getResponse(string request)
{
	auto curl = curl_easy_init();
	string response_string;

	if (curl)
	{
		curl_easy_setopt(curl, CURLOPT_URL, (IP + ":" + port + request).c_str());
		curl_easy_setopt(curl, CURLOPT_NOPROGRESS, 1L);
		curl_easy_setopt(curl, CURLOPT_USERPWD, "user:pass");
		curl_easy_setopt(curl, CURLOPT_USERAGENT, "curl/7.42.0");
		curl_easy_setopt(curl, CURLOPT_MAXREDIRS, 50L);
		curl_easy_setopt(curl, CURLOPT_TCP_KEEPALIVE, 1L);

		string header_string;
		curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, writeFunction);
		curl_easy_setopt(curl, CURLOPT_WRITEDATA, &response_string);
		curl_easy_setopt(curl, CURLOPT_HEADERDATA, &header_string);

		char* url;
		long response_code;
		double elapsed;
		curl_easy_getinfo(curl, CURLINFO_RESPONSE_CODE, &response_code);
		curl_easy_getinfo(curl, CURLINFO_TOTAL_TIME, &elapsed);
		curl_easy_getinfo(curl, CURLINFO_EFFECTIVE_URL, &url);

		curl_easy_perform(curl);
		curl_easy_cleanup(curl);
		curl = NULL;
	}
	else
	{
		//INCOMPLETE_MAPSH: try catch
		return "Try later";
	}

	return response_string;
}
