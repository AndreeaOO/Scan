import usb.core
import usb.util
import sys

#--FIND DEVICE
dev =usb.core.find (idVendor=0x040b, idProduct=0x6543)
if dev is None:
    raise ValueError('Device not found') 
    print ("device not found!")
else:
    print ("Device found!")
cfg=dev.get_active_configuration()
intf=cfg[0,0]
#--Find interface
ep = usb.util.find_descriptor(
        intf,
        custom_match = \
                lambda e: \
                    usb.util.endpoint_direction(e.bEndpointAddress) == \
                    usb.util.ENDPOINT_OUT)
assert ep is not None
#--Find Descriptors
alt = usb.util.find_descriptor(cfg, find_all=True, bInterfaceNumber=1)            
for cfg in alt: sys.stdout.write(str(cfg) + '\n')

#find configuration value
for cfg in dev:
    sys.stdout.write(str(cfg.bConfigurationValue) + '\n')

# Detach Kernel driver    
reattach = False
if dev.is_kernel_driver_active(1):
    reattach = True
    dev.detach_kernel_driver(1) 
    usb.util.claim_interface(dev, 1)

#--Set Configuration
cfg = usb.util.find_descriptor(dev, bConfigurationValue=1)
#print(cfg)
dev.set_configuration(cfg)

state = 0
while state == 0:
    try:
        data=dev.read(0x82,0x40,100)
        RxData = ''.join([str(x) for x in data])
        print (RxData)
    except usb.core.USBError as e:
         if e.args == ('Operation timed out',):
            print(e)
            continue

#msg = 'test'
#assert len(dev.write(1, msg, 100)) == len(msg)
#ret = dev.read(0x81, len(msg), 100)
#sret = ''.join([chr(x) for x in ret])
#assert sret == msg

#print (ep)
#idVendor=040b, idProduct=6543

#bottle barcode: 5701598028791 (plus newline) 
