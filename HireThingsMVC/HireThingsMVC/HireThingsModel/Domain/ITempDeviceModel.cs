using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEPS.MEPSTemp.Model
{
    public interface ITempDeviceModel 
    {
        long DeviceId { get; set; }
        string MacAddress { get; set; }
        string DeviceName { get; set; }
        string DeviceIP { get; set; }
        string DeviceSrNo { get; set; }
        long TimeZoneId { get; set; }
        long SystemId { get; set; }
        string SystemName { get; set; }
        long OrganizationId { get; set; }
        string OrganizationName { get; set; }
        long WSId { get; set; }
        string WsName { get; set; }
        decimal TempLowLimit { get; set; }
        decimal TempUpperlimit { get; set; }
        decimal WarnLowerLimit { get; set; }
        decimal WarnUpperLimit { get; set; }
        decimal EmergencyLowLimit { get; set; }
        decimal EmergencyUpperLimit { get; set; }
        long MeasureUnitId { get; set; }
        Int16 TempRecInterval { get; set; }
        Int16 TempDisplayOnCTC { get; set; }
        string TempNotes { get; set; }
        Single TempOffset { get; set; }
        DateTime CalibrationDate { get; set; }
        DateTime NextServiceDateTime { get; set; }
        bool ResetPin { get; set; }
        DateTime ResetDate { get; set; }
        Int16 AlarmOnOff { get; set; }
        long AlarmTypeId { get; set; }
        string FirmwareVer { get; set; }
        DateTime FirmwareUpdateTime { get; set; }
        Single SysAccuracy { get; set; }
        Single DisplayResolution { get; set; }
        byte LiveIntervalForCTC { get; set; }
        Int16 EscalRecInterval { get; set; }
        DateTime AddDate { get; set; }
        long AddUserId { get; set; }
        DateTime UpdateDate { get; set; }
        long UpdateUserId { get; set; }
        bool IsActive { get; set; }
    }
}
