using Petthy.Models.SmartDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models.SmartDevice
{
    public class SmartDevice
    {
        public int SmartDeviceId { get; set; }
        public int PetId { get; set; }
        public SmartDeviceData SmartDeviceData { get; set; }


    }
}
