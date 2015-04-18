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
#include <usb.h>
#include <stdio.h>
#include "SosDevice.h"
#include "usbPackets.h"

int sosVendorId = 5840;
int sosProductId = 1606;
int sosPacketSize = 1 + 37; // report id + packet length

// From the HID spec:
static const int HID_REPORT_GET = 0x01;
static const int HID_REPORT_SET = 0x09;
static const int HID_REPORT_TYPE_INPUT = 0x01;
static const int HID_REPORT_TYPE_OUTPUT = 0x02;
static const int HID_REPORT_TYPE_FEATURE = 0x03;

static const int INTERFACE_NUMBER = 0;

static const int CONTROL_REQUEST_TYPE_IN = USB_ENDPOINT_IN | USB_TYPE_CLASS | USB_RECIP_INTERFACE;
static const int CONTROL_REQUEST_TYPE_OUT = USB_ENDPOINT_OUT | USB_TYPE_CLASS | USB_RECIP_INTERFACE;

static struct usb_device *findSos() {
    struct usb_bus *bus;
    struct usb_device *dev;
    struct usb_bus *busses;

    usb_init();
    usb_find_busses();
    usb_find_devices();
    
    busses = usb_get_busses();
    
    for (bus = busses; bus; bus = bus->next){
        for (dev = bus->devices; dev; dev = dev->next) {
            if ((dev->descriptor.idVendor == sosVendorId) && (dev->descriptor.idProduct == sosProductId)) {
                return dev;
            }
        }
    }
    return NULL;
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

std::string setOutputReport(int reportId, char* buf, int bufSize, usb_dev_handle *devHandle) {
    std::ostringstream strstrm;
    
    int claimResult = usb_claim_interface(devHandle, INTERFACE_NUMBER);
    if(claimResult != 0) {
        strstrm << "usb_claim_interface: " << " " << claimResult << " " << usb_strerror() << std::endl;
        return strstrm.str();
    }
    
    int bytesSent = usb_control_msg(
                                    devHandle,
                                    CONTROL_REQUEST_TYPE_OUT,
                                    HID_REPORT_SET,
                                    (HID_REPORT_TYPE_INPUT << 8) | reportId,
                                    INTERFACE_NUMBER,
                                    buf,
                                    bufSize,
                                    10000);
    if(bytesSent < 0) {
        strstrm << "usb_control_msg: " << " " << bytesSent << " " << usb_strerror();
        return strstrm.str();
    }
    
    int releaseResult = usb_release_interface(devHandle, INTERFACE_NUMBER);
    if(releaseResult != 0) {
        strstrm << "usb_release_interface: " << " " << releaseResult << " " << usb_strerror();
        return strstrm.str();
    }
    return std::string("Success!");
}

std::string sendControlPacket(usb_dev_handle *devHandle) {
    UsbControlPacket usbControlPacket;
    initControlPacket(&usbControlPacket);
    
    usbControlPacket.manualLeds0 = 200;
    usbControlPacket.manualLeds2 = 200;
    usbControlPacket.manualLeds4 = 200;
    try {
        return setOutputReport(USB_REPORTID_OUT_CONTROL, (char*)&usbControlPacket, sizeof(usbControlPacket), devHandle);
    } catch(std::exception &ex) {
        return ex.what();
    }
}

const std::string lightLights() {
    std::ostringstream strstrm;
    struct usb_device *dev = findSos();
    if (dev == NULL) {
        return std::string("No device found");
    }
    usb_dev_handle *devHandle = usb_open(dev);
    if(devHandle == NULL) {
        strstrm << "dev handle was null: " << " " << usb_strerror();
        return strstrm.str();
    }
    
    int detachResult = usb_detach_kernel_driver_np(devHandle, INTERFACE_NUMBER);
    if(detachResult != 0 && detachResult != -61) {
        strstrm << "usb_detach_kernel_driver_np: " << " " << detachResult << " " << usb_strerror();
        return strstrm.str();
    }
    
    return sendControlPacket(devHandle);
}

extern "C" std::string *GetHelloCount(void) {
    const std::string result = lightLights();
    std::cout << result << std::endl;
    return new std::string(result);
}