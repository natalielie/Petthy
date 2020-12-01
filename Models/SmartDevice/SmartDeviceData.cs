using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models.SmartDevice
{
    public class SmartDeviceData
    {
        public int Pulse { get; set; }
        public int Temperature { get; set; }
        public float[] GPS { get; set; }
    }
}
