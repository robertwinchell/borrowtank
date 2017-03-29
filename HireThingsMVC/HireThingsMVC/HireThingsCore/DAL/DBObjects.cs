

namespace ASOL.HireThings.Core
{
    public class DBObjects
    {
        // Add all SPs to be used in this Enum as member and use it in DAL operations instead of using string name.
        public enum StoredProcedures
        {
            hspAdvertisementSelectById,
            hspAdvertisementSelectForHomePage,
            hspAdvertisementSave,
            hspAdvertisementSelect,
            hspAdvertisementUpdate,
            hspAdvertisementSearch,
            hspDDLCountry,
            hspDDLCategory,
            hspDDLCategoryForHomePage,
            hspDDLCategoryByParent,
            hspDDLLocation,
            pspDDLWSModel,
            pspGetDashboardErrorLog,
            pspDDLCACorrectiveActionSetting,
            pspDashCorrActionTakenSelectByDashErrorIDForChange,
            pspGetExportGridMetaInfo,
            pspUserProfileUpdate,
            spGetUserOrganizationsPORTAL,
            pspUserSave,
            pspValidateUserPORTAL,
            spGetUserSecurityPORTAL,
            UpdatePassword,
            pspUserSearch,
            pspTimeZoneSelect,
            hspCountrySelect,
            hspCountry,
            hspCountrySearch,
            pspDDLSystem,
            pspDDLSystemForReadingSample,
            pspDDLOrganization,
            hspLocationSelect,
            hspSelectLocationByID,
            hspLocationSave,
            hspLocationUpdate,
            hspLocationSearch,
            hspCountrySave,
            hspCountryUpdate,
            pspThemeSearch,
            pspThemeSelect,
            pspThemeSave,
            pspThemeUpdate,
            pspThemeById,
            pspCategorySearch,
            pspCategorySelect,
            pspCategorySave,
            pspCategoryUpdate,
            pspCategoryById,
            pspDDLTheme,
            pspDDLManufacturer,
            pspDDLThermostatType,
            pspDeviceSelect,
            pspDeviceSave,
            pspDeviceUpdate,
            pspDeviceById,
            pspDDLWorkstation,
            pspSelectDeviceByID,
            pspDeviceByID,
            pspAssignedCategory,
            pspUnassignedCategory,
            pspMeasureUnitSelect,
            pspEmailGroupSelect,
            pspEmailGroupSave,
            pspEmailGroupUpdate,
            pspEmailGroupSelectByID,
            pspGroupSelect,
            pspEmailServerSave,
            pspEmailServerUpdate,
            pspEmailServerSelect,
            pspEmailServerByID,
            pspDeviceAssignedToGroup,
            pspDeviceUnassignedToGroup,
            pspGroupSave,
            pspGroupUpdate,
            pspGroupByID,
            pspEmailServersGetActiveServers,
            pspEscalationSelect,
            pspDDLGroup,
            pspSecurityEmailVerify,
            pspDDLSDeviceGroup,
            pspDDLAvailableEscalation,
            pspSecurityAnswerVerify,
            pspEmailTemplateForgotPassword,
            pspGetEmailGroupUnassigned,
            pspGetEmailGroupAssigned,
            pspEscalationSave,
            pspEscalationUpdate,
            pspEscalationSelectById,
            pspCategoryManufacturerSelect,
            pspCategoryManufacturerById,
            pspCategoryManufacturerUpdate,
            pspCategoryManufacturerSave,
            pspSecurityQuestionSave,
            pspSecurityQuestionUpdate,
            pspSecurityQuestionSelectById,
            pspSecurityQuestionSelect,
            pspChangePassword,
            pspEmailTemplateChangePassword,
            pspChangeSecurityInfo,
            pspDDLSecurityQuestion,
            pspUserUpdate,
            pspRoleSave,
            pspRoleUpdate,
            pspRoleSelect,
            pspRoleSelectById,
            pspObjectSave,
            pspObjectUpdate,
            pspObjectSelectById,
            pspObjectSelect,
            pspDDLModule,
            pspDDLRole,
            pspEmailTemplate,
            pspEmailLog,
            pspAuthorization,
            pspDDLObjectLevel,
            pspDDLParentObjectList,
            pspDDLBusinessTypeList,
            pspSetEmailConfirmed,
            pspUpdatePINHash,
            pspRoleObjectSelect,
            pspRoleObjectSave,
            pspSelectUserById,
            pspGetMenuList,
            pspDDLGender,
            pspDDLPageSize,
            pspAlarmSoundSave,
            pspAlarmSoundUpdate,
            pspAlarmSoundSelectByID,
            pspAlarmSoundSelect,
            pspAlarmSave,
            pspAlarmUpdate,
            pspAlarmSelectByID,
            pspAlarmSelect,
            pspDDLAlarmList,
            pspCorrectiveActionListSave,
            pspCorrectiveActionListUpdate,
            pspCorrectiveActionListByID,
            pspCorrectiveActionListSelect,
            pspGetCorrectiveActionTypeAssignedToList,
            pspGetCorrectiveActionTypeUnassigned,
            pspCorrectiveActionTypeSelect,
            pspDDLEmailGroup,
            pspDDLAlertType,
            pspMailListSave,
            pspMailListSearch,
            pspMailListSelectById,
            pspMailListUpdate,
            pspGetDashboardError,
            pspGetTempReading,
            pspDashCorrectiveActionTakenSelectByDashboardErrorID,
            pspDDLCorrectiveActionList,
            pspSelectDashboardErrorById,
            pspDashboardErrorSelectByID,
            pspCorrActionTakenSave,
            pspDashErrorUpdate,
            pspGetDashboardWidgets,
            pspDDLSuppressInterval,
            pspDDLMeasureUnit,
            pspDisplayResoulation,
            pspDDLAlarmOnOff,
            pspDDLDisplayONCTC,
            pspDDLAlarmType,
            pspGetDashboardErrorCorrAction,
            pspCorrActionTakenUpdate,
            pspDDLOrganizationLevelList,
            pspDDLParentOrganizationList,
            pspGetDashboardErrorNotifications,
            pspGetDashboardErrorInProgressNotifications,
            pspGetTempReadingNotifications,
            spActivityLogInsertUpdate,
            pspDDLFixedAlarm,
            pspEscalationUnassignedToGroup,
            pspEscalationAssignedToGroup,
            pspAssignedGroup,
            pspUnassignedGroup,
            pspDeviceGroupUnassignedToUser,
            pspDeviceGroupAssignedToUser,
            pspDDLRank,
            pspDeleteData,
            pspDashboardStatusAlertSearch,
            pspDashboardCorrActionSearch,
            pspDashboardCorrActionTakenSearch,
            pspGetTempReadingSearch,
            pspGetDashboardErrorCorrActionTakenNotifications,
            pspGetDashboardTicker_ErrorAlerts,

            pspServiceObjectUpdate,
            pspServiceObjectSearch,
            pspServiceObjectSelect,
            pspServiceObjectById,

            pspTCPServerSelect,
            pspTCPServerSearchById,
            pspTCPServerSearch,
            pspTCPServerSave,
            pspTCPServerUpdate,

            pspCACorrectiveActionSave,
            pspCACorrectiveActionSelect,
            pspCACorrectiveActionSearch,
            pspCACorrectiveActionSearchById,
            pspCACorrectiveActionUpdate,

            pspCACategorySave,
            pspCACategorySelect,
            pspCACategorySearch,
            pspCACategorySearchById,
            pspCACategoryUpdate,

            pspCARootCauseSave,
            pspCARootCauseSelect,
            pspCARootCauseSearch,
            pspCARootCauseSearchById,
            pspCARootCauseUpdate,

            pspCASymptomSave,
            pspCASymptomSelect,
            pspCASymptomSearch,
            pspCASymptomSearchById,
            pspCASymptomUpdate,

            pspCACorrectiveActionSettingSave,
            pspCACorrectiveActionSettingSelect,
            pspCACorrectiveActionSettingSearch,
            pspCACorrectiveActionSettingSearchById,
            pspCACorrectiveActionSettingUpdate,

            pspDDLCACategory,
            pspDDLCARootCause,
            pspDDLCASymptom,
            pspDDLCACorrectiveAction,

            pspChartTempReading,
            pspGetDashboardErrorByDashboradErrorId,

            pspDDLCategoryCalibrationFrequency,
            pspDDLDeviceByOrganization,

            webApi_pspRoleSave,
            webApi_pspRoleUpdate,
            webApi_pspRoleSelect,
            webApi_pspRoleSelectById,
            webApi_pspObjectSave,
            webApi_pspObjectSelect,
            webApi_pspObjectSelectById,
            webApi_pspObjectUpdate,
            webApi_pspUserSave,
            webApi_pspUserUpdate,
            webApi_pspUserSearch,
            webApi_pspSelectUserById,
            pspDDLWebAPIRole,
            pspWebAPI_RoleObjectSave,

            spResetDeviceSpecialInstructionInsert,
            pspWebAPI_RoleObjectSelect,
            pspReadingSampleSearch,
            pspReadingSampleSelect,
            pspReadingSampleSave,
            pspReadingSampleUpdate,
            pspReadingSampleById,
            pspDDLDevice
        }

        public class SPParameter
        {
            // Add all possible SP parameters here. Avoid duplication


            
            public const string ShowOnHomepage = "@ShowOnHomepage";

            public const string PriceList = "@PriceList";
            public const string IdList = "@IdList";
            public const string ParentId = "@ParentId";
            
            public const string AdvertiserId = "@AdvertiserId";
            public const string LocationId = "@LocationId";
            public const string VisibilityType = "@VisibilityType";
            public const string SpecialTerms = "@SpecialTerms";
            public const string FixedRateLabel = "@FixedRateLabel";
            public const string FixedRateCharges = "@FixedRateCharges";
            public const string DepositCharges = "@DepositCharges";
            public const string BondCharges = "@BondCharges";

            public const string MinimumCharges = "@MinimumCharges";
            public const string DefaultTimeChargingId = "@DefaultTimeChargingId";
            public const string ChargingTypeId = "@ChargingTypeId";
            public const string IsVisibleToPublic = "@IsVisibleToPublic";
            public const string Quantity = "@Quantity";
            public const string IsMoreThanOne = "@IsMoreThanOne";
            public const string Description = "@Description";            
            public const string Address = "@Address";
            public const string PhoneNumber = "@PhoneNumber";
            public const string AdvertisementId = "@AdvertisementId";
            public const string Web = "@Web";
            public const string Email = "@Email";            
            public const string CountryId = "@CountryId";
            public const string CountryName = "@CountryName";
            public const string LocationID = "@LocationID";
            public const string LocationName = "@LocationName";


            // New end here



            public const string ColumnName = "@ColumnName";
            public const string Location = "@Location";
            public const string TempReadingSampleId = "@TempReadingSampleId";
            public const string FromDate = "@FromDate";
            public const string ToDate = "@ToDate";
            public const string CorrActionSettingSelected = "@CorrActionSettingSelected";
            public const string ReturnData = "@ReturnData";
            public const string UserIDs = "@UserIDs";
            public const string GROUPID = "@GROUPID";
            public const string FIRSTNAME = "@FIRSTNAME";
            public const string MIDDLENAME = "@MIDDLENAME";
            public const string LASTNAME = "@LASTNAME";
            public const string PHONENUMBER = "@PHONENUMBER";
            public const string TitleID = "@TitleID";
            public const string DeptID = "@DeptID";
            public const string EMAIL = "@EMAIL";
            public const string PIN = "@PIN";
            public const string Active = "@Active";
            public const string RFIDPIN = "@RFIDPIN";
            public const string StationId = "@StationId";
            public const string TimeZoneID = "@TimeZoneID";
            public const string OrganizationID = "@OrganizationID";
            public const string UserId = "@UserId";
            public const string RetVal = "@RetVal";
            public const string OrganizationType = "@OrganizationType";
            public const string ModType = "@ModType";
            public const string PINHash = "@PINHash";
            public const string EmailConfirmed = "@EmailConfirmed";
            public const string Predicate = "@Predicate";
            public const string SystemId = "@SystemId";
            public const string SystemControlNo = "@SystemControlNo";
            public const string SystemName = "@SystemName";
            public const string IsActive = "@IsActive";
            public const string AddUserId = "@AddUserId";
            public const string UpdateUserId = "@UpdateUserId";
            public const string CustomerOrganizationId = "@CustomerOrganizationId";
            public const string BusinessTypeId = "@BusinessTypeId";
            public const string OrganizationRefNo = "@OrganizationRefNo";
            public const string OrganizationName = "@OrganizationName";
            public const string City = "@City";
            public const string ZipCode = "@ZipCode";
            public const string State = "@State";
            public const string Country = "@Country";
            public const string Phone = "@Phone";
            public const string Fax = "@Fax";
            public const string Website = "@Website";
            public const string ParentOrganizationID = "@ParentOrganizationID";
            public const string OrganizationLevelID = "@OrganizationLevelID";
            public const string DEANo = "@DEANo";
            public const string AddDate = "@AddDate";
            public const string UpdateDate = "@UpdateDate";
            public const string CustomerWSId = "@CustomerWSId";
            public const string WSName = "@WSName";
            public const string WSSerialNo = "@WSSerialNo";
            public const string ModelNo = "@ModelNo";
            public const string TempWSId = "@TempWSId";
            public const string StorageTypeID = "@StorageTypeID";
            public const string Name = "@Name";
            public const string Code = "@Code";
            public const string ThemeId = "@ThemeId";
            public const string ThermoStateTypeName = "@Name";
            public const string ThermostatTypeId = "@ThermostatTypeId";
            public const string CategoryId = "@CategoryId";
            public const string CategorySerialNo = "@CategorySerialNo";
            public const string CategoryDesc = "@CategoryDesc";
            public const string @CategoryCalFreqId = "@CategoryCalFreqId";
            public const string CategoryCalibrationDate = "@CategoryCalibrationDate";
            public const string EntryDate = "@EntryDate";
            public const string ExpiryDate = "@ExpiryDate";
            public const string ManufacturerId = "@ManufacturerId";
            public const string Notes = "@Notes";
            public const string DeviceId = "@DeviceId";
            public const string MacAddress = "@MacAddress";
            public const string DeviceName = "@DeviceName";
            public const string DeviceIP = "@DeviceIP";
            public const string DeviceSrNo = "@DeviceSrNo";
            public const string TempLowerLimit = "@TempLowerLimit";
            public const string TempUpperLimit = "@TempUpperLimit";
            public const string WarnLowerLimit = "@WarnLowerLimit";
            public const string WarnUpperLimit = "@WarnUpperLimit";
            public const string EmergencyLowerLimit = "@EmergencyLowerLimit";
            public const string EmergencyUpperLimit = "@EmergencyUpperLimit";
            public const string MeasureUnitId = "@MeasureUnitId";
            public const string TempRecInterval = "@TempRecInterval";
            public const string TempDisplayOnCTC = "@TempDisplayOnCTC";
            public const string TempNotes = "@TempNotes";
            public const string TempOffset = "@TempOffset";
            public const string CalibrationDate = "@CalibrationDate";
            public const string NextServiceDateTime = "@NextServiceDateTime";
            public const string ResetPin = "@ResetPin";
            public const string ResetDate = "@ResetDate";
            public const string AlarmOnOff = "@AlarmOnOff";
            public const string AlarmTypeId = "@AlarmTypeId";
            public const string FirmwareVer = "@FirmwareVer";
            public const string FirmwareUpdateTime = "@FirmwareUpdateTime";
            public const string SysAccuracy = "@SysAccuracy";
            public const string DisplayResolution = "@DisplayResolution";
            public const string LiveIntervalForCTC = "@LiveIntervalForCTC";
            public const string EscalRecInterval = "@EscalRecInterval";
            public const string WSId = "@WSId";
            public const string CategoryIds = "@CategoryIds";
            public const string Symbol = "@Symbol";
            public const string EmailIdList = "@EmailIdList";
            public const string EmailGroupId = "@EmailGroupId";
            public const string ServerName = "@ServerName";
            public const string ServerIP = "@ServerIP";
            public const string EmailPort = "@EmailPort";
            public const string UserName = "@UserName";
            public const string UserPswd = "@UserPswd";
            public const string UseDefaultCredentials = "@UseDefaultCredentials";
            public const string EnableSSL = "@EnableSSL";
            public const string UpdateUserID = "@UpdateUserID";
            public const string Priority = "@Priority";
            public const string ServerId = "@ServerId";
            public const string RoleId = "@RoleId";
            public const string PassPhrase = "@PassPhrase";
            public const string TempGroupId = "@TempGroupId";
            public const string GroupName = "@GroupName";
            public const string GroupNotes = "@GroupNotes";
            public const string strDeviceIds = "@strDeviceIds";
            public const string RetQuestion = "@RetQuestion";
            public const string EscalationId = "@EscalationId";
            public const string SecurityQuestionId = "@SecurityQuestionId";
            public const string Answer = "@Answer";
            public const string OldPIN = "@OldPIN";
            public const string RetEmailTemplate = "@RetEmailTemplate";
            public const string RetEmailSubject = "@RetEmailSubject";
            public const string CallBackUrl = "@CallBackUrl";
            public const string EscalationLevel = "@EscalationLevel";
            public const string StrListItem1 = "@StrListItem1";
            public const string StrListItem2 = "@StrListItem2";
            public const string StrListItem3 = "@StrListItem3";
            public const string StrListItem4 = "@StrListItem4";
            public const string Level1Interval = "@Level1Interval";
            public const string Level1Repeat = "@Level1Repeat";
            public const string Level2Interval = "@Level2Interval";
            public const string Level2Repeat = "@Level2Repeat";
            public const string Level2IncludePrev = "@Level2IncludePrev";
            public const string Level3Interval = "@Level3Interval";
            public const string Level3Repeat = "@Level3Repeat";
            public const string Level3IncludePrev = "@Level3IncludePrev";
            public const string Level4Interval = "@Level4Interval";
            public const string Level4Repeat = "@Level4Repeat";
            public const string Level4IncludePrev = "@Level4IncludePrev";
            public const string EmailType = "@EmailType";
            public const string To = "@To";
            public const string Cc = "@Cc";
            public const string Bcc = "@Bcc";
            public const string Subject = "@Subject";
            public const string Body = "@Body";
            public const string SentStatus = "@SentStatus";
            public const string UnSentReason = "@UnSentReason";
            public const string CategoryManufacturerId = "@CategoryManufacturerId";
            public const string Question = "@Question";
            public const string ObjectId = "@ObjectId";
            public const string Caption = "@Caption";
            public const string URL = "@URL";
            public const string ParentObjectId = "@ParentObjectId";
            public const string AllowWrite = "@AllowWrite";
            public const string AllowDelete = "@AllowDelete";
            public const string ObjectLevel = "@ObjectLevel";
            public const string ModuleId = "@ModuleId";
            public const string CategoryName = "@CategoryName";
            public const string ObjectName = "@ObjectName";
            public const string Address1 = "@Address1";
            public const string Address2 = "@Address2";
            public const string Address3 = "@Address3";
            public const string RoleObjectId = "@RoleObjectId";
            public const string DOB = "@DOB";
            public const string CPage = "@CPage";
            public const string PSize = "@PSize";
            public const string GenderId = "@GenderId";
            public const string ObjectOrder = "@ObjectOrder";
            public const string ObjectImage = "@ObjectImage";
            public const string SoundFilePath = "@SoundFilePath";
            public const string SoundName = "@SoundName";
            public const string AlarmSoundId = "@AlarmSoundId";
            public const string AlarmName = "@AlarmName";
            public const string FixedAlarm = "@FixedAlarm";
            public const string FixedAlarmTime = "@FixedAlarmTime";
            public const string AlarmId = "@AlarmId";
            public const string CorrectiveActionListId = "@CorrectiveActionListId";
            public const string strCorrectiveActionTypeIds = "@strCorrectiveActionTypeIds";
            public const string TempMailListId = "@TempMailListId";
            public const string CcEmailGroupSelected = "@CcEmailGroupSelected";
            public const string BccEmailGroupSelected = "@BccEmailGroupSelected";
            public const string ToEmailGroupSelected = "@ToEmailGroupSelected";
            public const string EmailSearchTerm = "@EmailSearchTerm";
            public const string MailListName = "@MailListName";
            public const string MailTo = "@MailTo";
            public const string MailCC = "@MailCC";
            public const string MailBcc = "@MailBcc";
            public const string MailSubject = "@MailSubject";
            public const string MailBody = "@MailBody";
            public const string AlertTypeSelected = "@AlertTypeSelected";

            public const string CorrActionTakenId = "@CorrActionTakenId";
            public const string DashboardErrorId = "@DashboardErrorId";
            public const string CorrectiveActionDate = "@CorrectiveActionDate";
            public const string CorrectiveActionUserId = "@CorrectiveActionUserId";
            public const string CorrectiveActionNotes = "@CorrectiveActionNotes";
            public const string DashboardErrorName = "@DashboardErrorName";
            public const string SuppressInterval = "@SuppressInterval";
            public const string SuppressStatus = "@SuppressStatus";
            public const string SuppressReason = "@SuppressReason";
            public const string CorrectiveActionStatus = "@CorrectiveActionStatus";
            public const string LoginType = "@LoginType";
            public const string LogoutType = "@LogoutType";
            public const string ModuleType = "@ModuleType";
            public const string ActivityId = "@ActivityId";
            public const string SessionId = "@SessionId";
            public const string IPAddress = "@IPAddress";
            public const string ActivityForm = "@ActivityForm";
            public const string AlertTypeUnSelected = "@AlertTypeUnSelected";
            public const string strEscalationIds = "@strEscalationIds";
            public const string GroupIds = "@GroupIds";
            public const string strGroupIds = "@strGroupIds";
            public const string Rank = "@Rank";
            public const string Reason = "@Reason";
            public const string RecordId = "@RecordId";
            public const string IsDeletedMarked = "@IsDeletedMarked";
            public const string LoggedInUserId = "@LoggedInUserId";
            public const string AlertInterval = "@AlertInterval";

            public const string ServiceObjectId = "@ServiceObjectId";
            public const string Interval = "@Interval";
            public const string HeartbeatType = "@HeartbeatType";

            public const string TCPPort = "@TCPPort";
            public const string HeartbeatInterval = "@HeartbeatInterval";

            public const string CorrectiveActionId = "@CorrectiveActionId";
            public const string CorrectiveAction = "@CorrectiveAction";
      
            public const string Category = "@Category";
            public const string RootCauseId = "@RootCauseId";
            public const string RootCause = "@RootCause";
            public const string SymptomId = "@SymptomId";
            public const string Symptom = "@Symptom";
            public const string CorrectiveActionSettingId = "@CorrectiveActionSettingId";
            public const string StartDate = "@StartDate";
            public const string EndDate = "@EndDate";

            public const string WARoleId = "@WARoleId";
            public const string WAObjectId = "@WAObjectId";
            public const string AllowGet = "@AllowGet";
            public const string AllowPost = "@AllowPost";
            public const string AllowPut = "@AllowPut";
            public const string WAUserId = "@WAUserId";
            public const string ReadingSampleType = "@ReadingSampleType";           
            public const string SamplePoint = "@SamplePoint";
            public const string StartTime = "@StartTime";

        }

        public class Fields
        {
            
            public const string ShowActive = "ShowActive";
            public const string IsWithoutSearch = "IsWithoutSearch";
            public const string ColumnName = "ColumnName";
            public const string Address = "Address";
            public const string Location = "Location";
            public const string Duration = "Duration";
            public const string TempLowLimit = "TempLowLimit";
            public const string TempUpperLimit = "TempUpperLimit";
            public const string IsNormalReading = "IsNormalReading";
            public const string ReadingDateUTC = "ReadingDateUTC";
            public const string UserId = "UserId";
            public const string EmailIdList = "EmailIdList";
            public const string TempEmailGroupId = "TempEmailGroupId";
            public const string Description = "Description";
            public const string IsActive = "IsActive";
            public const string Name = "Name";
            public const string TempGroupId = "TempGroupId";
            public const string GroupName = "GroupName";
            public const string GroupNotes = "GroupNotes";
            public const string EscalationId = "EscalationId";
            public const string Level1Interval = "Level1Interval";
            public const string Level1Repeat = "Level1Repeat";
            public const string Level2Interval = "Level2Interval";
            public const string Level2Repeat = "Level2Repeat";
            public const string Level2IncludePrev = "Level2IncludePrev";
            public const string Level3Interval = "Level3Interval";
            public const string Level3Repeat = "Level3Repeat";
            public const string Level3IncludePrev = "Level3IncludePrev";
            public const string Level4Interval = "Level4Interval";
            public const string Level4Repeat = "Level4Repeat";
            public const string Level4IncludePrev = "Level4IncludePrev";
            public const string CategoryManufacturerId = "CategoryManufacturerId";
            public const string SecurityQuestionId = "SecurityQuestionId";
            public const string Question = "Question";
            public const string RoleId = "RoleId";
            public const string ModuleName = "ModuleName";
            public const string ObjectId = "ObjectId";
            public const string Caption = "Caption";
            public const string URL = "URL";
            public const string ParentObjectId = "ParentObjectId";
            public const string AllowWrite = "AllowWrite";
            public const string AllowDelete = "AllowDelete";
            public const string ObjectLevel = "ObjectLevel";
            public const string ModuleId = "ModuleId";
            public const string ObjectLevelId = "ObjectLevelId";
            public const string EscalationName = "EscalationName";
            public const string ParentName = "ParentName";
            public const string BusinessTypeId = "BusinessTypeId";
            public const string BusinessType = "BusinessType";
            public const string RoleObjectId = "RoleObjectId";
            public const string ShowWrite = "ShowWrite";
            public const string ShowDelete = "ShowDelete";
            public const string DOB = "DOB";
            public const string ObjectName = "ObjectName";
            public const string ObjectOrder = "ObjectOrder";
            public const string ObjectImage = "ObjectImage";
            public const string SoundFilePath = "SoundFilePath";
            public const string SoundName = "SoundName";
            public const string AlarmSoundId = "AlarmSoundId";
            public const string AlarmName = "AlarmName";
            public const string FixedAlarm = "FixedAlarm";
            public const string FixedAlarmTime = "FixedAlarmTime";
            public const string AlarmId = "AlarmId";
            public const string TRows = "TRows";
            public const string PSize = "PSize";
            public const string CPage = "CPage";
            public const string Text = "Text";
            public const string Value = "Value";
            public const string CorrectiveActionListId = "CorrectiveActionListId";
            public const string CorrectiveActionTypeId = "CorrectiveActionTypeId";
            public const string MailListName = "MailListName";
            public const string TempMailListId = "TempMailListId";
            public const string OrganizationName = "OrganizationName";
            public const string CorrActionSettingSelected = "CorrActionSettingSelected";

            public const string MailTo = "MailTo";
            public const string MailCC = "MailCC";
            public const string MailBcc = "MailBcc";
            public const string MailSubject = "MailSubject";
            public const string MailBody = "MailBody";
            public const string ToEmailGroupList = "ToEmailGroupList";
            public const string CcEmailGroupList = "CcEmailGroupList";
            public const string BccEmailGroupList = "BccEmailGroupList";
            public const string SystemId = "SystemId";
            public const string OrganizationId = "OrganizationId";
            public const string DeviceName = "DeviceName";
            public const string CurrentTemp = "CurrentTemp";
            public const string ErrorReason = "ErrorReason";
            public const string ErrorDate = "ErrorDate";
            public const string ErrorDateTZ = "ErrorDateTZ";
            public const string ReadingDate = "ReadingDate";
            public const string TempReading = "TempReading";
            public const string CorrActionTakenId = "CorrActionTakenId";
            public const string DashboardErrorId = "DashboardErrorId";

            public const string CorrectiveActionDate = "CorrectiveActionDate";
            public const string CorrectiveActionUserId = "CorrectiveActionUserId";
            public const string CorrectiveActionNotes = "CorrectiveActionNotes";
            public const string DashboardErrorName = "DashboardErrorName";

            public const string DashBoardErrorId = "DashBoardErrorId";
            public const string AlertTypeId = "AlertTypeId";
            public const string ErrorCodeId = "ErrorCodeId";
            public const string DeviceId = "DeviceId";
            public const string AlertStatusId = "AlertStatusId";
            public const string SuppressUserId = "SuppressUserId";
            public const string SuppressDate = "SuppressDate";
            public const string SuppressInterval = "SuppressInterval";
            public const string SuppressStatus = "SuppressStatus";
            public const string CorrectiveActionStatus = "CorrectiveActionStatus";
            public const string EscalationLevel = "EscalationLevel";
            public const string AlertSentDate = "AlertSentDate";
            public const string MinTemp = "MinTemp";
            public const string MaxTemp = "MaxTemp";
            public const string CategorySerialNo = "CategorySerialNo";
            public const string AlertReason = "AlertReason";
            public const string AddDate = "AddDate";
            public const string ErrorCode = "ErrorCode";
            public const string ErrorAlerts = "ErrorAlerts";
            public const string WorkingCabinets = "WorkingCabinets";
            public const string TotalCabinets = "TotalCabinets";
            public const string TotalAlerts = "TotalAlerts";
            public const string ThemeId = "ThemeId";
            public const string CategorySerailNo = "CategorySerailNo";
            public const string CategoryDesc = "CategoryDesc";
            public const string CategoryCalFreqId = "CategoryCalFreqId";
            public const string CategoryCalFreqName = "CategoryCalFreqName";
            public const string SuppressReason = "SuppressReason";

            public const string MeasureUnitId = "MeasureUnitId";
            public const string CorrActionInProgressNotes = "CorrActionInProgressNotes";
            public const string CorrActionInProgressDate = "CorrActionInProgressDate";
            public const string UpdateDate = "UpdateDate";
            public const string Rank = "Rank";
            public const string IsMepsGroup = "IsMepsGroup";

            public const string Notes = "Notes";
            public const string AlertInterval = "AlertInterval";
            public const string ServiceObjectId = "ServiceObjectId";
            public const string Interval = "Interval";
            public const string HeartbeatType = "HeartbeatType";
            public const string HeartbeatTypeText = "HeartbeatTypeText";

            public const string ServerId = "ServerId";
            public const string ServerName = "ServerName";
            public const string ServerIP = "ServerIP";
            public const string TCPPort = "TCPPort";
            public const string HeartbeatInterval = "HeartbeatInterval";

            public const string IsOther = "IsOther";
            public const string CorrectiveActionId = "CorrectiveActionId";
            public const string CorrectiveAction = "CorrectiveAction";
            public const string CategoryId = "CategoryId";
            public const string Category = "Category";
            public const string RootCauseId = "RootCauseId";
            public const string RootCause = "RootCause";
            public const string SymptomId = "SymptomId";
            public const string Symptom = "Symptom";
            public const string CorrectiveActionSettingId = "CorrectiveActionSettingId";

            public const string CCategory = "C.Category";
            public const string RCRootCause = "RC.RootCause";
            public const string SSymptom = "S.Symptom";
            public const string CACorrectiveAction = "CA.CorrectiveAction";

            public const string CorrectiveActionTakenDate = "CorrectiveActionTakenDate";

            public const string LastContactDate = "LastContactDate";
            public const string LastContactDateTZ = "LastContactDateTZ";
            public const string TimeZoneCode = "TimeZoneCode";
            public const string IsSystemGenerated = "IsSystemGenerated";

            public const string WARoleId = "WARoleId";
            public const string AllowPut = "AllowPut";
            public const string AllowGet = "AllowGet";
            public const string AllowPost = "AllowPost";
            public const string WAObjectId = "WAObjectId";

            public const string ShowGet = "ShowGet";
            public const string ShowPut = "ShowPut";
            public const string ShowPost = "ShowPost";
        }
    }
}
