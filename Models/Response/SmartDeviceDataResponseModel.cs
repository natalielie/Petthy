using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petthy.Models.Response
{
    public class SmartDeviceDataResponseModel
    {
        public SmartDeviceDataResponseModel(int smartDeviceDataId, int petId, bool isIll, 
            bool isEnoughWalking, DateTime smartDeviceDataDate)
        {
            SmartDeviceDataId = smartDeviceDataId;
            PetId = petId;
            IsIll = isIll;
            IsEnoughWalking = isEnoughWalking;
            SmartDeviceDataDate = smartDeviceDataDate;
        }

        public int SmartDeviceDataId { get; set; }
        public int PetId { get; set; }
        public bool IsIll { get; set; }
        public bool IsEnoughWalking { get; set; }
        public DateTime SmartDeviceDataDate { get; set; }
    }
}
