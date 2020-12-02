using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models.SmartDevice
{
    public class SmartDeviceData
    {
        public int SmartDeviceDataId { get; set; }
        public int PetId { get; set; }
        public bool IsIll { get; set; }
        public bool IsEnoughWalking { get; set; }
        public DateTime SmartDeviceDataDate { get; set; }
    }
}
