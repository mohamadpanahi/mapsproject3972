#include "MapsEncrypt.h"

void MapsEncrypt::swap(char* plaintext, int size)
{
	size /= 2;
	char temp;
	for (int i = 0; i < size; i++)
		temp = plaintext[i], plaintext[i] = plaintext[size + i], plaintext[size + i] = temp;
}
void MapsEncrypt::x(char* plaintext, int size)
{
	for (int i = key.length() - 1; i >= 0; i--)
		for (int j = 0; j < size; j++)
			plaintext[j] ^= key[i];
}
void MapsEncrypt::f(char* plaintext, int size)
{
	for (int i = 0; i < size; i += 2)
		plaintext[i] ^= plaintext[i + 1];
}

MapsEncrypt::MapsEncrypt(string key) : key(key) {}
MapsEncrypt::MapsEncrypt(string key, string filename) : key(key), filename(filename) {}
MapsEncrypt::~MapsEncrypt() {}

string MapsEncrypt::encrypt(string plaintext)
{
	int t = plaintext.length();
	if (t % 2 == 1)
		plaintext += '~', t++;

	char* ptext = new char[t];
	for (int i = 0; i < t; i++)
		ptext[i] = plaintext[i];

	swap(ptext, t);
	x(ptext, t);
	f(ptext, t);

	string s;
	for (int i = 0; i < t; i++)
		s += ptext[i];
	return s;
}
string MapsEncrypt::decrypt(string chipertext)
{
	int t = chipertext.length();

	char* ctext = new char[t];
	for (int i = 0; i < t; i++)
		ctext[i] = chipertext[i];

	f(ctext,t);
	x(ctext,t);
	swap(ctext,t);

	string s;
	for (int i = 0; i < t - 1; i++)
		s += ctext[i];
	if (ctext[t - 1] != '~')
		s += ctext[t - 1];

	return s;
}

void MapsEncrypt::encryptfile(string plaintext)
{
	ofstream file(filename);
	file << encrypt(plaintext);
}
string MapsEncrypt::decryptfile()
{
	ifstream file(filename);
	stringstream ss;
	ss << file.rdbuf();
	return decrypt(ss.str());
}
void MapsEncrypt::encryptfile()
{
	stringstream ss;
	ifstream file(filename);
	ss << file.rdbuf();
	file.close();

	ofstream ofile(filename);
	ofile << encrypt(ss.str());
}