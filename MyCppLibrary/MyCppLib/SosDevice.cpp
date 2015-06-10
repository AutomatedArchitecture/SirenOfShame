//
//  Hello.cpp
//  MyCppLib
//
//  Created by Lee Richardson on 4/11/15.
//  Copyright (c) 2015 Lee Richardson. All rights reserved.
//

#include <chrono>
#include <thread>

#ifdef WIN32
#include <windows.h>
#endif

#include <stdint.h>
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

std::string setOutputReport(uint8_t reportId, unsigned char *buf, int bufSize, hid_device *devHandle) {
    std::ostringstream strstrm;
    
    buf[0] = reportId;
    int result = hid_write(devHandle, buf, sosPacketSize);
    std::cout << "hid_write: " << result << std::endl;
    
    return std::string("Success!");
}

std::string sendControlPacket(hid_device *devHandle) {
    UsbControlPacket usbControlPacket;
    initControlPacket(&usbControlPacket);
    
    usbControlPacket.controlByte1 = CONTROL_BYTE1_IGNORE;
    usbControlPacket.ledMode = 4;
    usbControlPacket.ledPlayDuration = 15;
    //usbControlPacket.audioMode = 1;
    //usbControlPacket.audioPlayDuration = 100;
    try {
        return setOutputReport(USB_REPORTID_OUT_CONTROL, (unsigned char*)&usbControlPacket, sizeof(usbControlPacket), devHandle);
    } catch(std::exception &ex) {
        return ex.what();
    }
}

void printBuf(unsigned char* buf) {
    for (int i = 0; i < sosPacketSize; i++)
        printf("%d", buf[i]);
    printf("\n");
}

void getInputReport(uint8_t reportId, unsigned char* bufParent, hid_device *handle) {
    unsigned char buf[38];
    //for (int i = 0; i < 38; i++)
    //{
    //    buf[i] = 0;
    //}
	memset(buf, 0, 38);
	buf[0] = reportId;

    int res = 0;
    std::cout << "begin hid_read" << std::endl;
    hid_set_nonblocking(handle, 0);
    //buf[0] = reportId;

    printBuf(buf);

    //hid_send_feature_report(handle, buf, sosPacketSize);
    //hid_write(handle, buf, sosPacketSize);
    
    //printBuf(buf);

	//res = hid_get_feature_report(handle, buf, sizeof(buf));
	std::this_thread::sleep_for(std::chrono::milliseconds(500));
    res = hid_read(handle, buf, sizeof(buf));
    std::cout << "res = " << res << " bytes read" << std::endl;
    
    printBuf(buf);
}

void initReadLedPacket(UsbReadLedPacket *packet) {
    memset(packet, 0, sizeof(UsbReadLedPacket));
}

void readLedPatterns(hid_device *handle) {
    UsbControlPacket usbControlPacket;
    initControlPacket(&usbControlPacket);
	usbControlPacket.controlByte1 = CONTROL_BYTE1_IGNORE;
    usbControlPacket.readLedIndex = 0;
    setOutputReport(USB_REPORTID_OUT_CONTROL, (unsigned char*)&usbControlPacket, sizeof(usbControlPacket), handle);

    UsbReadLedPacket usbReadLedPacket;
    initReadLedPacket(&usbReadLedPacket);
    getInputReport(USB_REPORTID_IN_READ_LED, (unsigned char*)&usbReadLedPacket, handle);
    std::cout << "LED1: " << usbReadLedPacket.name << std::endl;
}

void initInfoPacket(UsbInfoPacket *packet) {
    memset(packet, 0, sizeof(UsbInfoPacket));
}

void readInfo(hid_device *handle) {
    UsbInfoPacket usbInfoPacket;
    initInfoPacket(&usbInfoPacket);

    try {
        getInputReport(USB_REPORTID_IN_INFO, (unsigned char*)&usbInfoPacket, handle);
    }
    catch (std::exception &ex) {
        std::cout << ex.what();
    }
    
    //std::cout << "version: " << usbInfoPacket.version << std::endl;
    //std::cout << "hardwareVersion: " << usbInfoPacket.hardwareVersion << std::endl;
    //std::cout << "externalMemorySize: " << usbInfoPacket.externalMemorySize << std::endl;
}

const std::string lightLights() {
    std::ostringstream strstrm;
    int res;
    hid_device *handle;
    wchar_t wstr[MAX_STR];

    res = hid_init();
    
    // Open the device using the VID, PID,
    // and optionally the Serial number.

	hid_device_info *devices = hid_enumerate(sosVendorId, sosProductId);
	std::wcout << devices[0].manufacturer_string << std::endl;
	std::wcout << devices[0].vendor_id << std::endl;
	std::wcout << devices[0].product_id;
	hid_free_enumeration(devices);

    handle = hid_open(sosVendorId, sosProductId, NULL);

    if (handle == nullptr) {
        return "No siren found";
    }

    res = hid_get_manufacturer_string(handle, wstr, MAX_STR);
    std::wcout << L"Manufacturer String: " << wstr << std::endl;
    
    // Read the Product String
    res = hid_get_product_string(handle, wstr, MAX_STR);
    std::wcout << L"Product String: " << wstr << std::endl;
    
    // Read the Serial Number String
    res = hid_get_serial_number_string(handle, wstr, MAX_STR);
    std::wcout << L"Serial Number String: " << wstr << std::endl;
    
    // Read Indexed String 1
    res = hid_get_indexed_string(handle, 1, wstr, MAX_STR);
    std::wcout << L"Indexed String 1: " << wstr << std::endl;
    
    //sendControlPacket(handle);
    //readInfo(handle);
    readLedPatterns(handle);
    
    // Finalize the hidapi library
    hid_close(handle);
    res = hid_exit();
    
    return std::string("Success");
}

extern "C" void ReadLedPatterns(char* ledPatterns, int& ledId) {
    auto str = std::string("Hello World");

    strcpy(ledPatterns, str.c_str());
    
    ledId = 53;
}

extern "C" void GetHelloCount(void) {
    const std::string result = lightLights();
    std::cout << result << std::endl;
    //return new std::string(result);
}
