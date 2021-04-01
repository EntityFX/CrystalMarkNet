// EntityFx.CrystalMark.h : Include file for standard system include files,
// or project specific include files.

#pragma once

#include <iostream>
#include <string>
#include <chrono>

template< typename... Args >
std::string string_sprintf(const char* format, Args... args) {
	int length = std::snprintf(nullptr, 0, format, args...);

	char* buf = new char[length + 1];
	std::snprintf(buf, length + 1, format, args...);

	std::string str(buf);
	delete[] buf;
	return str;
}