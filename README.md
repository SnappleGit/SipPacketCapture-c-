# SipPacketCapture
CaptureSipPackets for now returns only Telephone Number

 Usage:
 ```
            CaptureSip.CaptureSipPackets capture = new CaptureSip.CaptureSipPackets();
            Thread t = new Thread(capture.startCapture);
            t.Start();
            string lastnr="";
            while (true)
            {   //OnVarChange return Telephone Number
                if(capture.OnVarChange()!=lastnr){
                Console.WriteLine(capture.OnVarChange());
                lastnr=capture.OnVarChange();
                }
            }
```
