using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface IDeviceModel : IBaseModel
    {
        long? DeviceId { get; set; }
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
        decimal? TempLowerLimit { get; set; }
        decimal? TempUpperLimit { get; set; }
        decimal? WarnLowerLimit { get; set; }
        decimal? WarnUpperLimit { get; set; }
        decimal? EmergencyLowerLimit { get; set; }
        decimal? EmergencyUpperLimit { get; set; }
        int? AlertInterval { get; set; }
        long MeasureUnitId { get; set; }
        Int16? TempRecInterval { get; set; }
        //Int16? TempDisplayOnCTC { get; set; }
        string TempNotes { get; set; }
        //string TempOffset { get; set; }
        DateTime? CalibrationDate { get; set; }
        DateTime? NextServiceDateTime { get; set; }
        //bool ResetPin { get; set; }
        //DateTime? ResetDate { get; set; }
        //Int16 AlarmOnOff { get; set; }
        //int AlarmTypeId { get; set; }
        string FirmwareVer { get; set; }
        DateTime FirmwareUpdateTime { get; set; }
        decimal? SysAccuracy { get; set; }
        //int? DisplayResolution { get; set; }
        //Int16? LiveIntervalForCTC { get; set; }
        Int16? EscalRecInterval { get; set; }
        bool DeviceInstructionComplete { get; set; }
        DateTime AddDate { get; set; }
        long AddUserId { get; set; }
        DateTime UpdateDate { get; set; }
        long UpdateUserId { get; set; }
        bool IsActive { get; set; }
        string Location { get; set; }
        List<ISelectListItem> SystemList { get; set; }
        List<ISelectListItem> OrganizationList { get; set; }
        List<ISelectListItem> WorkstationList { get; set; }
        List<ISelectListItem> TimeZoneList { get; set; }
        List<ISelectListItem> MeasureUnitList { get; set; }
        List<ISelectListItem> DisplayCTCList { get; set; }
        List<ISelectListItem> DisplayResolutionList { get; set; }
        List<ISelectListItem> AlarmTypeList { get; set; }
        List<ISelectListItem> AlarmOnOffList { get; set; }
        List<ISelectListItem> ProbeList { get; set; }

        ListBoxSettingsModel ProbeModel { get; set; }
        ListBoxSettingsModel GroupModel { get; set; }
    }
}
