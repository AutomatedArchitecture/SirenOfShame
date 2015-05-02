//
//  sos_kext.c
//  sos-kext
//
//  Created by Lee Richardson on 5/1/15.
//  Copyright (c) 2015 Lee Richardson. All rights reserved.
//

#include <mach/mach_types.h>

kern_return_t sos_kext_start(kmod_info_t * ki, void *d);
kern_return_t sos_kext_stop(kmod_info_t *ki, void *d);

kern_return_t sos_kext_start(kmod_info_t * ki, void *d)
{
    return KERN_SUCCESS;
}

kern_return_t sos_kext_stop(kmod_info_t *ki, void *d)
{
    return KERN_SUCCESS;
}
