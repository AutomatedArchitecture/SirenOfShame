//
//  Hello.cpp
//  MyCppLib
//
//  Created by Lee Richardson on 4/11/15.
//  Copyright (c) 2015 Lee Richardson. All rights reserved.
//

#include <string.h>
#include <usb.h>
#include <stdio.h>
#include "SosDevice.h"
#include "usbPackets.h"

int sosVendorId = 5840;
int sosProductId = 1606;
int sosPacketSize = 1 + 37; // report id + packet length

static const int INTERFACE_NUMBER = 0;

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

void findDevice() {
    
}

extern "C" int GetHelloCount(int xyz) {
    char errorBuffer[1000];

    struct usb_device *dev = findSos();
    if (dev == NULL) {
        return 0;
    }
    usb_dev_handle *devHandle = usb_open(dev);
    if(devHandle == NULL) {
        return -1;
    }

    int detachResult = usb_detach_kernel_driver_np(devHandle, INTERFACE_NUMBER);
    if(detachResult != 0 && detachResult != -61) {
        sprintf(errorBuffer, "usb_detach_kernel_driver_np: %d %s\n", detachResult, usb_strerror());
        return detachResult;
    }
    
    return 1;
}

