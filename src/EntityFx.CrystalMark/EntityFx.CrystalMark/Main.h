// EntityFx.CrystalMark.h : Include file for standard system include files,
// or project specific include files.

#pragma once

#include <iostream>
#include <string>
#include <chrono>
#include <cstdio>
#include <cstdarg>
#include <vector>

#define _CRT_NO_VA_START_VALIDATION

std::string string_sprintf(const char* format, ...) {
    char buf[100];     // this should really be sized appropriately
                       // possibly in response to a call to vsnprintf()
    va_list vl;
    va_start(vl, format);

    vsnprintf(buf, sizeof(buf), format, vl);

    va_end(vl);

    return std::string(buf);
}