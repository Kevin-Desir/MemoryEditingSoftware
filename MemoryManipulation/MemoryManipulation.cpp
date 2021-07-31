#include "pch.h"
#include "MemoryManipulation.h"
#include <iostream>
#include <vector>
#include <TlHelp32.h>
#include <windows.h>
#include <memoryapi.h>
#include <sstream>

// Constants

// DLL internal state variables :
DWORD pid;		// pid of the current app we "connected" to
HANDLE handle;	// handle for the app ?

/*
* This method uses the target process name to retrieve its pid
* TargetProcessName (input) : name of the process that we want to manipulate memory
* ErrorCode (output) : to inform why could not retrieve pid
*/
DWORD GetPid(std::wstring TargetProcessName, int& ErrorCode) {
	// variable that will contain the pid to return
	DWORD pid = 0;

	// temporary variable to store the pids found;
	// vector because we don't want multiple pids matching
	std::vector<DWORD> pids;

	// create a snapshot that will store process names and pid
	HANDLE snap = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0); // all process

	// entry is the current process from the snap Handle
	PROCESSENTRY32W entry;
	// set the dwsize
	entry.dwSize = sizeof entry;

	// this loop tests for each process in the snapshot if the name matches the TargetProcessName
	do {
		if (std::wstring(entry.szExeFile) == TargetProcessName) {
			pids.emplace_back(entry.th32ProcessID); // name matches; add to list
		}
	} while (Process32NextW(snap, &entry)); // keep going until end of snapshot

	// for debug only
	for (int i(0); i < pids.size(); ++i) {
		std::cout << pids[i] << std::endl;
	}

	// check if multiple pids were found matching the TargetProcessName
	if (pids.size() > 0) {
		if (pids.size() > 1) {
			std::cout << "More that one PID has been found" << std::endl;
			ErrorCode = -2;
		}
		// if only one pid was found then its the one we are looking for so return it
		else {
			pid = pids.at(0);
		}
	}
	else {
		ErrorCode = -1;
	}

	return pid;
}

std::wstring StringToWstring(const std::string& str)
{
	int size_needed = MultiByteToWideChar(CP_UTF8, 0, &str[0], (int)str.size(), NULL, 0);
	std::wstring wstrTo(size_needed, 0);
	MultiByteToWideChar(CP_UTF8, 0, &str[0], (int)str.size(), &wstrTo[0], size_needed);
	return wstrTo;
}

/*
* This method initializes the link to the target process
* TargetProcessName (input) : name of the target process .exe (can be obtained via the command tasklist)
*/
int InitLink(const char* TargetProcessName) {
	// to know why could not retrieve pid
	// -1 : could not found matching process
	// -2 : multiple process found with matching name
	int ErrorCode = 0;

	// check if we can retrieve the pid of the target process
	if ((pid = GetPid(StringToWstring(TargetProcessName), ErrorCode)) > 0) {
		if ((handle = OpenProcess(PROCESS_ALL_ACCESS, FALSE, pid)) != NULL) {
			return pid;
		}
	}

	return ErrorCode;
}

int WriteIntInMemory(const char* memoryAddress, int value) {
	intptr_t int_address;
	{
		std::stringstream stream;
		stream << std::hex << memoryAddress;
		stream >> int_address;
	}
	void* address = (void*)int_address;

	WriteProcessMemory(handle, (LPVOID)address, &value, sizeof(int), 0);

	return 0;
}

int WriteFloatInMemory(const char* memoryAddress, float value) {
	intptr_t int_address;
	{
		std::stringstream stream;
		stream << std::hex << memoryAddress;
		stream >> int_address;
	}
	void* address = (void*)int_address;

	WriteProcessMemory(handle, (LPVOID)address, &value, sizeof(float), 0);

	return 0;
}

int WriteShortInMemory(const char* memoryAddress, short value) {
	intptr_t int_address;
	{
		std::stringstream stream;
		stream << std::hex << memoryAddress;
		stream >> int_address;
	}
	void* address = (void*)int_address;
	
	WriteProcessMemory(handle, (LPVOID)address, &value, sizeof(short), 0);

	return 0;
}

int ReadIntFromMemory(const char* memoryAddress, int value) {
	intptr_t int_address;
	{
		std::stringstream stream;
		stream << std::hex << memoryAddress;
		stream >> int_address;
	}
	void* address = (void*)int_address;

	ReadProcessMemory(handle, (LPVOID)address, &value, sizeof(int), 0);

	return value;
}

float ReadFloatFromMemory(const char* memoryAddress, float value) {
	intptr_t int_address;
	{
		std::stringstream stream;
		stream << std::hex << memoryAddress;
		stream >> int_address;
	}
	void* address = (void*)int_address;

	ReadProcessMemory(handle, (LPVOID)address, &value, sizeof(float), 0);
	
	return value;
}

short ReadShortFromMemory(const char* memoryAddress, short value) {
	intptr_t int_address;
	{
		std::stringstream stream;
		stream << std::hex << memoryAddress;
		stream >> int_address;
	}
	void* address = (void*)int_address;

	ReadProcessMemory(handle, (LPVOID)address, &value, sizeof(short), 0);

	return value;
}