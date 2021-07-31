#pragma once

#ifdef MEMORYMANIPULATION_EXPORTS
#define MEMORYMANIPULATION_API _declspec(dllexport)
#else
#define MEMORYMANIPULATION_API _declspec(dllexport)
#endif

extern "C" MEMORYMANIPULATION_API int InitLink(const char* TargetProcessName);
// Could not overload the writing and read methods because of the extern C linkage
extern "C" MEMORYMANIPULATION_API int WriteIntInMemory(const char* address, int value);
extern "C" MEMORYMANIPULATION_API int WriteFloatInMemory(const char* address, float value);
extern "C" MEMORYMANIPULATION_API int WriteShortInMemory(const char* address, short value);
extern "C" MEMORYMANIPULATION_API int ReadIntFromMemory(const char* address, int value);
extern "C" MEMORYMANIPULATION_API float ReadFloatFromMemory(const char* address, float value);
extern "C" MEMORYMANIPULATION_API short ReadShortFromMemory(const char* address, short value);
