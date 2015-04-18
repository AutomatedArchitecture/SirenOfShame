//
//  Hello.cpp
//  MyCppLib
//
//  Created by Lee Richardson on 4/11/15.
//  Copyright (c) 2015 Lee Richardson. All rights reserved.
//

#include <string.h>
#include <iostream>
#include <sstream>
#include "hidapi.h"
#include <stdio.h>
#include <stdlib.h>
#include "SosDevice.h"
#include "usbPackets.h"

uint16_t sosVendorId = 5840;
uint16_t sosProductId = 1606;
int sosPacketSize = 1 + 37; // report id + packet length

#define MAX_STR 255

// From the HID spec:
static const int HID_REPORT_GET = 0x01;
static const int HID_REPORT_SET = 0x09;
static const int HID_REPORT_TYPE_INPUT = 0x01;
static const int HID_REPORT_TYPE_OUTPUT = 0x02;
static const int HID_REPORT_TYPE_FEATURE = 0x03;

static const int INTERFACE_NUMBER = 0;

void initControlPacket(UsbControlPacket *packet) {
    memset(packet, 0, sizeof(UsbControlPacket));
    packet->reportId = 1;
    packet->controlByte1 = 0;
    packet->audioMode = 0xff;
    packet->ledMode = 0xff;
    packet->audioPlayDuration = 0xffff;
    packet->ledPlayDuration = 0xffff;
    packet->readAudioIndex = 0xff;
    packet->readLedIndex = 0xff;
    packet->manualLeds0 = 0xff;
    packet->manualLeds1 = 0xff;
    packet->manualLeds2 = 0xff;
    packet->manualLeds3 = 0xff;
    packet->manualLeds4 = 0xff;
}

std::string setOutputReport(int reportId, unsigned char *buf, int bufSize, hid_device *devHandle) {
    std::ostringstream strstrm;
    
    int result = hid_write(devHandle, buf, bufSize);
    std::cout << "hid_write: " << result;
    
    return std::string("Success!");
}

std::string sendControlPacket(hid_device *devHandle) {
    UsbControlPacket usbControlPacket;
    initControlPacket(&usbControlPacket);
    
    usbControlPacket.ledMode = 4;
    usbControlPacket.ledPlayDuration = 100;
    usbControlPacket.audioMode = 1;
    usbControlPacket.audioPlayDuration = 100;
    try {
        return setOutputReport(USB_REPORTID_OUT_CONTROL, (unsigned char*)&usbControlPacket, sizeof(usbControlPacket), devHandle);
    } catch(std::exception &ex) {
        return ex.what();
    }
}

const std::string lightLights() {
    std::ostringstream strstrm;
    int res;
	hid_device *handle;
    wchar_t wstr[MAX_STR];
    
    res = hid_init();
    
    // Open the device using the VID, PID,
	// and optionally the Serial number.
	handle = hid_open(sosVendorId, sosProductId, NULL);

    res = hid_get_manufacturer_string(handle, wstr, MAX_STR);
    wprintf(L"Manufacturer String: %s\n", wstr);
    
    // Read the Product String
	res = hid_get_product_string(handle, wstr, MAX_STR);
	wprintf(L"Product String: %s\n", wstr);
    
    // Read the Serial Number String
	res = hid_get_serial_number_string(handle, wstr, MAX_STR);
	wprintf(L"Serial Number String: (%d) %s\n", wstr[0], wstr);
    
    // Read Indexed String 1
	res = hid_get_indexed_string(handle, 1, wstr, MAX_STR);
	wprintf(L"Indexed String 1: %s\n", wstr);
    
    sendControlPacket(handle);
    
    // Finalize the hidapi library
	res = hid_exit();
    
	return std::string("Success");
}

extern "C" std::string *GetHelloCount(void) {
    const std::string result = lightLights();
    std::cout << result << std::endl;
    return new std::string(result);
}
