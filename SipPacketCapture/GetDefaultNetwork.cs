using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CaptureSip
{
    class GetDefaultNetwork
    {
        public static string getConnectedAdapter()
        {
            var nic = NetworkInterface
         .GetAllNetworkInterfaces()
         .FirstOrDefault(i => i.NetworkInterfaceType != NetworkInterfaceType.Loopback && i.NetworkInterfaceType != NetworkInterfaceType.Tunnel);
            var device = nic.Description;
            return device;
        }
    }
}
