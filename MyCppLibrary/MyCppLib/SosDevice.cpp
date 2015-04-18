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
#include <libusb.h>
#include <stdio.h>
#include "SosDevice.h"
#include "usbPackets.h"

uint16_t sosVendorId = 5840;
uint16_t sosProductId = 1606;
int sosPacketSize = 1 + 37; // report id + packet length

// From the HID spec:
static const int HID_REPORT_GET = 0x01;
static const int HID_REPORT_SET = 0x09;
static const int HID_REPORT_TYPE_INPUT = 0x01;
static const int HID_REPORT_TYPE_OUTPUT = 0x02;
static const int HID_REPORT_TYPE_FEATURE = 0x03;

static const int INTERFACE_NUMBER = 0;

static const int CONTROL_REQUEST_TYPE_IN = LIBUSB_ENDPOINT_IN | LIBUSB_REQUEST_TYPE_CLASS | LIBUSB_RECIPIENT_INTERFACE;
static const int CONTROL_REQUEST_TYPE_OUT = LIBUSB_ENDPOINT_OUT | LIBUSB_REQUEST_TYPE_CLASS | LIBUSB_RECIPIENT_INTERFACE;

static int perr(char const *format, ...)
{
	va_list args;
	int r;
    
	va_start (args, format);
	r = vfprintf(stderr, format, args);
	va_end(args);
    
	return r;
}

static struct libusb_device *findSos() {
	libusb_device_handle *handle;
	libusb_device *dev;
	handle = libusb_open_device_with_vid_pid(NULL, sosVendorId, sosProductId);
	dev = libusb_get_device(handle);
    return dev;
}

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

std::string setOutputReport(int reportId, unsigned char *buf, int bufSize, libusb_device_handle *devHandle) {
    std::ostringstream strstrm;
    
    int claimResult = libusb_claim_interface(devHandle, INTERFACE_NUMBER);
    if(claimResult != 0) {
        strstrm << "usb_claim_interface: " << " " << claimResult << " " << libusb_strerror((enum libusb_error)claimResult) << std::endl;
        return strstrm.str();
    }
    
//    int LIBUSB_CALL libusb_control_transfer(libusb_device_handle *dev_handle,
//                                            uint8_t request_type, uint8_t bRequest, uint16_t wValue, uint16_t wIndex,
//                                            unsigned char *data, uint16_t wLength, unsigned int timeout);

    
    int bytesSent = libusb_control_transfer(
                                    devHandle,
                                    CONTROL_REQUEST_TYPE_OUT,
                                    HID_REPORT_SET,
                                    (HID_REPORT_TYPE_INPUT << 8) | reportId,
                                    INTERFACE_NUMBER,
                                    buf,
                                    bufSize,
                                    10000);
    if(bytesSent < 0) {
        strstrm << "usb_control_msg: " << " " << bytesSent; // << " " << libusb_strerror();
        return strstrm.str();
    }
    
    int releaseResult = libusb_release_interface(devHandle, INTERFACE_NUMBER);
    if(releaseResult != 0) {
        strstrm << "usb_release_interface: " << " " << releaseResult << " " << libusb_strerror((enum libusb_error)releaseResult);
        return strstrm.str();
    }
    return std::string("Success!");
}

std::string sendControlPacket(libusb_device_handle *devHandle) {
    UsbControlPacket usbControlPacket;
    initControlPacket(&usbControlPacket);
    
    usbControlPacket.manualLeds0 = 200;
    usbControlPacket.manualLeds2 = 200;
    usbControlPacket.manualLeds4 = 200;
    try {
        return setOutputReport(USB_REPORTID_OUT_CONTROL, (unsigned char*)&usbControlPacket, sizeof(usbControlPacket), devHandle);
    } catch(std::exception &ex) {
        return ex.what();
    }
}

const std::string lightLights() {
    int r;
    std::ostringstream strstrm;
//    libusb_device *dev = findSos();
//    if (dev == NULL) {
//        return std::string("No device found");
//    }
	r = libusb_init(NULL);
    libusb_device_handle *devHandle = libusb_open_device_with_vid_pid(NULL, sosVendorId, sosProductId);
    if(devHandle == NULL) {
		perr("  Failed.\n");
        strstrm << "dev handle was null: "; // << " " << libusb_strerror();
        return strstrm.str();
    }
    
    int detachResult = libusb_detach_kernel_driver(devHandle, INTERFACE_NUMBER);
    if(detachResult != 0 && detachResult != -61) {
        strstrm << "usb_detach_kernel_driver_np: " << " " << detachResult << " " << libusb_strerror((enum libusb_error)detachResult);
        return strstrm.str();
    }
    
    return sendControlPacket(devHandle);

 	libusb_exit(NULL);
}

extern "C" std::string *GetHelloCount(void) {
    const std::string result = lightLights();
    std::cout << result << std::endl;
    return new std::string(result);
}
