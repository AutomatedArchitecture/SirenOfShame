
#ifndef _usb_packets_h_
#define _usb_packets_h_

#pragma pack(push, 1)

#define USB_REPORTID_OUT_CONTROL      1
#define USB_REPORTID_OUT_DATA_UPLOAD  2
#define USB_REPORTID_OUT_LED_CONTROL  3

#define USB_REPORTID_IN_INFO          1
#define USB_REPORTID_IN_READ_AUDIO    3
#define USB_REPORTID_IN_READ_LED      4

#define USB_NAME_SIZE 20

// audio modes
#define AUDIO_MODE_OFF            0
#define AUDIO_MODE_INTERNAL_START 1

// led modes
#define LED_MODE_OFF            0
#define LED_MODE_MANUAL         1
#define LED_MODE_INTERNAL_START 2

// play duration
#define PLAY_DURATION_FOREVER 0xfffe

// hardware type
#define HARDWARE_TYPE_STANDARD 1
#define HARDWARE_TYPE_PRO      2

#define USB_DATA_SIZE 32

typedef struct _UsbControlPacket {
    uint8_t reportId;
    
	//firmwareUpgrade : 1;
	//echoOn : 1;
	//debug : 1;
	uint8_t controlByte1;
    
	uint8_t audioMode;
	uint8_t ledMode;
	uint16_t audioPlayDuration; // 1/10s
	uint16_t ledPlayDuration;   // 1/10s
	uint8_t readAudioIndex;
	uint8_t readLedIndex;
	uint8_t manualLeds0;
	uint8_t manualLeds1;
	uint8_t manualLeds2;
	uint8_t manualLeds3;
	uint8_t manualLeds4;
} UsbControlPacket;

typedef struct _UsbDataPacket {
	uint32_t address;
	uint8_t data[USB_DATA_SIZE];
} UsbDataPacket;

typedef struct _UsbDataManualControlPacket {
	uint8_t led1 : 1;
	uint8_t led2 : 1;
	uint8_t led3 : 1;
	uint8_t led4 : 1;
} UsbDataManualControlPacket;

typedef struct _UsbReadAudioPacket {
	uint8_t id;
	char name[USB_NAME_SIZE];
} UsbReadAudioPacket;

typedef struct _UsbReadLedPacket {
	uint8_t id;
	char name[USB_NAME_SIZE];
} UsbReadLedPacket;

// from SoS to computer
typedef struct _UsbInfoPacket {
	uint16_t version;
	uint8_t hardwareType;
	uint8_t hardwareVersion;
	uint32_t externalMemorySize;
	uint8_t audioMode;
	uint16_t audioPlayDuration;
	uint8_t ledMode;
	uint16_t ledPlayDuration;
} UsbInfoPacket;

#pragma pack(pop)

#endif