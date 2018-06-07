import usb.core
import usb.util
import sys

dev =usb.core.find (idVendor=0x040b, idProduct=0x6543)
if dev is None:
    raise ValueError('Device not found') 
    print ("device not found!")
else:
    print ("Device found!")
cfg=dev.get_active_configuration()
intf=cfg[0,0]

ep = usb.util.find_descriptor(
        intf,
        custom_match = \
                lambda e: \
                    usb.util.endpoint_direction(e.bEndpointAddress) == \
                    usb.util.ENDPOINT_OUT)
assert ep is not None

for cfg in dev:
    sys.stdout.write(str(cfg.bConfigurationValue) + '\n')
    for intf in cfg:
        sys.stdout.write('\t' + "interface number: " + \
                         str(intf.bInterfaceNumber) + \
                         ',' + \
                         str(intf.bAlternateSetting) + \
                         '\n')
        for ep in intf:
            sys.stdout.write('\t\t' + "EndPoint Address: " + \
                             str(ep.bEndpointAddress) + \
                             '\n')
alt = usb.util.find_descriptor(cfg, find_all=True, bInterfaceNumber=1)            

for cfg in alt: sys.stdout.write(str(cfg) + '\n')
#print (ep)

#idVendor=040b, idProduct=6543
