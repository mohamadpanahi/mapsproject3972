#pragma once
#include <string>
#include <sstream>
#include <fstream>
using namespace std;

class AES
{
	const char key[16] = "123456789012356";
	unsigned char expandedKey[176];

	void KeyExpansionCore(unsigned char * in, unsigned char i);
	void KeyExpansion(unsigned char expandedKeys[176]);
	void AddRoundKey(unsigned char * state, unsigned char * roundKey);
	void en_SubBytes(unsigned char * state);
	void en_ShiftRows(unsigned char * state);
	void MixColumns(unsigned char * state);
	void en_Round(unsigned char * state);
	string code(unsigned char * message);
	void de_SubBytes(unsigned char * state);
	void de_Round(unsigned char * state);
	string decode(unsigned char * encryptedMessage);
	void SubRoundKey(unsigned char * state, unsigned char * roundKey);
	void InverseMixColumns(unsigned char * state);
	void de_ShiftRows(unsigned char * state);
public:
	AES();
	string AESEncrypt(string message);
	string AESDecrypt(string encryptedMessage);
	bool AESEncryptFile(string text, string file);
	bool AESEncryptFile(string file);
	stringstream AESDecryptFile(string file);
};
