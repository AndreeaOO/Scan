import usb.core
import usb.util

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
print (ep)

#idVendor=040b, idProduct=6543
