using SharpPcap;
using System;
using System.Text;
using System.Threading;

namespace CaptureSip
{
    public class CaptureSipPackets
    {

       
         public void startCapture()
        {


            // Retrieve the device list
            var devices = CaptureDeviceList.Instance;

            var mainadapter = GetDefaultNetwork.getConnectedAdapter();
            // If no devices were found print an error




            int i = 0, k = 0;
            // Scan the list printing every entry
            foreach (var dev in devices)
            {
                if (dev.Description == mainadapter)
                    k = i;
                i++;
            }


            var device = devices[k];

            //Register our handler function to the 'packet arrival' event
            device.OnPacketArrival +=
                new PacketArrivalEventHandler(device_OnPacketArrival);

            //Open the device for capturing
            int readTimeoutMilliseconds = 1000;
            device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);

            // tcpdump filter to capture only TCP/IP packets
            string filter = "tcp";
            device.Filter = filter;



            // Start capture packets
            device.Capture();

            // Close the pcap device
            // (Note: this line will never be called since
            //  we're capturing infinite number of packets
            device.Close();
        }

        /// <summary>
        /// Prints the time and length of each received packet
        /// </summary>
        /// 

        public string OnVarChange()
        {
            return NrTel;
        }
        private string telnr = "";
        public string NrTel
        {
            get
            {
                return telnr;
            }
            set
            {
                this.telnr = value;
                this.OnVarChange();
            }
        }
        public bool debug = false;
        private void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            if (Encoding.GetEncoding("UTF-8").GetString(e.Packet.Data).Contains("sip") & Encoding.GetEncoding("UTF-8").GetString(e.Packet.Data).Contains("CSeq: 1 ACK"))
            {
                var info = Encoding.GetEncoding("UTF-8").GetString(e.Packet.Data);
                foreach (var x in info.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                {
                    if(debug)
                    Console.WriteLine(x + "\n\n");
                    if (x.Contains("From:"))
                    {
                        NrTel = x.Split(' ')[1].Split(':')[0].Replace("\"", "");
                    }
                }

            }

        }
       
    }
}
