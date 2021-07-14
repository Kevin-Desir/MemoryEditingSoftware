#pragma once

#ifdef MEMORYMANIPULATION_EXPORTS
#define MEMORYMANIPULATION_API _declspec(dllexport)
#else
#define MEMORYMANIPULATION_API _declspec(dllexport)
#endif

extern "C" MEMORYMANIPULATION_API int TestMe(const char* Text);