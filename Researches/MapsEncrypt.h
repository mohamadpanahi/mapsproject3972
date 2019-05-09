#pragma once
#include <iostream>
#include <string>
#include <fstream>
#include <sstream>
using namespace std;

/*
plaintext -> swap -> x -> f
swap: Encrypt=Decrypt

*/

class MapsEncrypt
{
	const string key;
	const string filename;

	void swap(char* plaintext, int size);
	void x(char* plaintext, int size);
	void f(char* plaintext, int size);
public:
	
	MapsEncrypt(string key);
	MapsEncrypt(string key, string filename);
	~MapsEncrypt();

	string encrypt(string plaintext);
	string decrypt(string chipertext);

	void encryptfile(string plaintext);
	//void encryptfile();
	string decryptfile();
};

