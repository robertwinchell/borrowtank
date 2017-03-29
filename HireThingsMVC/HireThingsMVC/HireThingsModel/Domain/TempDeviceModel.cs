using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEPS.MEPSTemp.Model
{
    public class TempDeviceModel : IBaseModel, ITempDeviceModel
    {
        public long DeviceId { get; set; }
        public string MacAddress { get; set; }
        public string DeviceName { get; set; }
        public string DeviceIP { get; set; }
        public string DeviceSrNo { get; set; }
        public long TimeZoneId { get; set; }
        public long SystemId { get; set; }
        public string SystemName { get; set; }
        public long OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public long WSId { get; set; }
        public string WsName { get; set; }
        public decimal TempLowLimit { get; set; }
        public decimal TempUpperlimit { get; set; }
        public decimal WarnLowerLimit { get; set; }
        public decimal WarnUpperLimit { get; set; }
        public decimal EmergencyLowLimit { get; set; }
        public decimal EmergencyUpperLimit { get; set; }
        public long MeasureUnitId { get; set; }
        public Int16 TempRecInterval { get; set; }
        public Int16 TempDisplayOnCTC { get; set; }
        public string TempNotes { get; set; }
        public Single TempOffset { get; set; }
        public DateTime CalibrationDate { get; set; }
        public DateTime NextServiceDateTime { get; set; }
        public bool ResetPin { get; set; }
        public DateTime ResetDate { get; set; }
        public Int16 AlarmOnOff { get; set; }
        public long AlarmTypeId { get; set; }
        public string FirmwareVer { get; set; }
        public DateTime FirmwareUpdateTime { get; set; }
        public Single SysAccuracy { get; set; }
        public Single DisplayResolution { get; set; }
        public byte LiveIntervalForCTC { get; set; }
        public Int16 EscalRecInterval { get; set; }
        public DateTime AddDate { get; set; }
        public long AddUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public long UpdateUserId { get; set; }
        public bool IsActive { get; set; }
    }
}
