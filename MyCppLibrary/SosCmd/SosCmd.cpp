// SosCmd.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <stdint.h>
#include <string.h>
#include <iostream>
#include <sstream>
#include "hidapi.h"
#include <stdio.h>
#include <stdlib.h>
#include "SosDevice.h"
#include "usbPackets.h"

int _tmain(int argc, _TCHAR* argv[])
{
	GetHelloCount();
	std::cout << "Press any key";
	std::cin.get();
	return 0;
}

