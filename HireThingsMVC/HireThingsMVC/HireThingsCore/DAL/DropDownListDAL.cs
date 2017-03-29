using ASOL.HireThings.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Core
{
    public class DropDownListDAL : AbstractBaseDAL<ISelectListItem>
    {
        public async override Task<long> SaveAsync(ISelectListItem model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async override Task<bool> UpdateAsync(ISelectListItem model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async override Task<ISelectListItem> SelectByIDAsync(long id, long userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async override Task<List<ISelectListItem>> SelectAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async override Task<List<ISelectListItem>> SelectAllByFilterAsync(IPredicate predicate, CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
            throw new NotImplementedException();
        }
        private void AddAllItem(List<ISelectListItem> list)
        {
            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            Item.Selected = true;
            list.Insert(0, Item);

        }

        public List<ISelectListItem> GetSystemList(Int64 userId, bool addAllItem = true)
        {
            DataTable dt = null;
            List<ISelectListItem> list = new List<ISelectListItem>();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLSystem.ToString(), ref param);

            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["SystemName"].ToString(),
                            Value = dr["SystemId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetCorrectiveActionList(Int64 userId, bool addAllItem = false)
        {
            DataTable dt = null;
            List<ISelectListItem> list = new List<ISelectListItem>();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLCorrectiveActionList.ToString(), ref param);

            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Name"].ToString(),
                            Value = dr["CorrectiveActionListId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetFixedAlarmList(Int64 userId, bool addAllItem = false)
        {
            DataTable dt = null;
            List<ISelectListItem> list = new List<ISelectListItem>();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLFixedAlarm.ToString(), ref param);

            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Text"].ToString(),
                            Value = dr["Value"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetEmailGroupList(Int64 userId, bool addAllItem = false)
        {
            DataTable dt = null;
            List<ISelectListItem> list = new List<ISelectListItem>();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLEmailGroup.ToString(), ref param);

            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Name"].ToString(),
                            Value = dr["EmailGroupId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }


        public List<ISelectListItem> GetSuppressIntervalList(Int64 userId, bool addAllItem = false)
        {
            DataTable dt = null;
            List<ISelectListItem> list = new List<ISelectListItem>();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLSuppressInterval.ToString(), ref param);

            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["SuppressInterval"].ToString(),
                            Value = dr["SuppressIntervalId"].ToString()

                        }).ToList<ISelectListItem>();
            }
            if (addAllItem)
                AddAllItem(list);


            return list;
        }
        public List<SelectListItem> GetAlertTypeList(Int64 userId, Int64? escalationId, bool addAllItem = false)
        {
            DataTable dt = null;
            if (escalationId == null)
                escalationId = 0;
            List<SelectListItem> list = new List<SelectListItem>();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            param.Add(DBObjects.SPParameter.EscalationId, GetParameter(DBObjects.SPParameter.EscalationId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, escalationId));
            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLAlertType.ToString(), ref param);

            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Name"].ToString(),
                            Value = dr["AlertTypeId"].ToString(),
                            Selected = Convert.ToBoolean(dr["Selected"])
                        }).ToList<SelectListItem>();
            }



            return list;
        }

        public List<ISelectListItem> GetOrganizationList(Int64 userId, Int64 systemId, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            param.Add(DBObjects.SPParameter.SystemId, GetParameter(DBObjects.SPParameter.SystemId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, systemId));

            if (systemId > 0 || systemId == -2)
                dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLOrganization.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["OrganizationName"].ToString(),
                            Value = dr["OrganizationId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetWorkstationList(Int64 userId, Int64 orgId, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            param.Add(DBObjects.SPParameter.OrganizationID, GetParameter(DBObjects.SPParameter.OrganizationID, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, orgId));
            if (orgId > 0)
                dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLWorkstation.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["WSName"].ToString(),
                            Value = dr["WSId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetThemeList(Int64 userId, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLTheme.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Name"].ToString(),
                            Value = dr["ThemeId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetThermostatTypeList(Int64 userId, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLThermostatType.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Name"].ToString(),
                            Value = dr["ThermostatTypeId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetManufacturerList(Int64 userId, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLManufacturer.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Name"].ToString(),
                            Value = dr["ProbeManufacturerId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetTimeZoneList(Int64 userId, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspTimeZoneSelect.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            list = (from DataRow dr in dt.Rows
                    select new SelectListItem
                    {
                        Text = dr["DisplayName"].ToString(),
                        Value = dr["TimeZoneId"].ToString(),
                    }).ToList<ISelectListItem>();

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

     
        public List<ISelectListItem> GetGroupList(long userId, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLGroup.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["GroupName"].ToString(),
                            Value = dr["TempGroupId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetModuleList(long userId, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLModule.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr[DBObjects.Fields.Name].ToString(),
                            Value = dr[DBObjects.Fields.ModuleId].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetObjectLevelList(long userId, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLObjectLevel.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr[DBObjects.Fields.ObjectLevel].ToString(),
                            Value = dr[DBObjects.Fields.ObjectLevelId].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetParentObjectList(long userId, long objectLvl, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.ObjectLevel, GetParameter(DBObjects.SPParameter.ObjectLevel, ParameterDirection.Input, ((int)SqlDbType.Int), 4, objectLvl));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            if (objectLvl > 0)
                dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLParentObjectList.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr[DBObjects.Fields.Name].ToString(),
                            Value = dr[DBObjects.Fields.ObjectId].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetBusinessTypesList(long userId, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLBusinessTypeList.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr[DBObjects.Fields.BusinessType].ToString(),
                            Value = dr[DBObjects.Fields.BusinessTypeId].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetAlarmSoundList(long userId, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLAlarmList.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr[DBObjects.Fields.SoundName].ToString(),
                            Value = dr[DBObjects.Fields.AlarmSoundId].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetOrganizationLevelList(long userId, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLOrganizationLevelList.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr[DBObjects.Fields.Text].ToString(),
                            Value = dr[DBObjects.Fields.Value].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> PopulateParentObjectLevelDropDownList(long userId, long? orgLvl, long systemId, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.OrganizationLevelID, GetParameter(DBObjects.SPParameter.OrganizationLevelID, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, orgLvl));
            param.Add(DBObjects.SPParameter.SystemId, GetParameter(DBObjects.SPParameter.SystemId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, systemId));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLParentOrganizationList.ToString(), ref param);


            List<ISelectListItem> list = new List<ISelectListItem>();

            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr[DBObjects.Fields.OrganizationName].ToString(),
                            Value = dr[DBObjects.Fields.OrganizationId].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetSearchList(Int64 userId, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Organization Control No";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Organization Name";
            Item.Value = "2";
            list.Add(Item);
            return list;
        }
        public List<ISelectListItem> GetPageSizeList()
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLPageSize.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr[DBObjects.Fields.Text].ToString(),
                            Value = dr[DBObjects.Fields.Value].ToString(),

                        }).ToList<ISelectListItem>();
            }


            return list;
        }

        public List<ISelectListItem> GetMeasureUnitList(long userId, bool addAllItem = false)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLMeasureUnit.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();

            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr[DBObjects.Fields.Name].ToString(),
                            Value = dr[DBObjects.Fields.MeasureUnitId].ToString(),

                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetProbeLifeList(Int64 userId, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            if (addAllItem)
            {
                Item.Text = "--Select--";
                Item.Value = "-1";
                list.Add(Item);
            }
            Item = new SelectListItem();
            Item.Text = "Life 1";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Life 2";
            Item.Value = "2";
            list.Add(Item);
            return list;
        }

        public List<ISelectListItem> GetOrganizationSearchList(Int64 userId, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Facility Name";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Customer No";
            Item.Value = "2";
            list.Add(Item);
            return list;
        }


        public List<ISelectListItem> GetSystemSearchList(Int64 userId, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Organization Control No";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Organization Name";
            Item.Value = "2";
            list.Add(Item);
            return list;
        }
        public List<ISelectListItem> GetWorkstationSearchList(Int64 userId, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "WS Name";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "WS Serial No";
            Item.Value = "2";
            list.Add(Item);
            return list;
        }

        public List<ISelectListItem> GetThemeSearchList(Int64 userId, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Name";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Code";
            Item.Value = "2";
            list.Add(Item);




            return list;
        }

        public List<ISelectListItem> GetProbeSearchList(Int64 userId, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();

            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Serial No";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Probe Name";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Probe Type";
            Item.Value = "3";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Manufacture";
            Item.Value = "4";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Thermostat Type";
            Item.Value = "5";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Entry Date";
            Item.Value = "6";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Expiry Date";
            Item.Value = "7";
            list.Add(Item);

            return list;
        }

        public List<ISelectListItem> GetDashboardErrorSearchList(Int64 userId, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();

            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Last Reading";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Reason For Alert";
            Item.Value = "2";
            list.Add(Item);




            return list;
        }
        public List<ISelectListItem> GetDashboardErrorDeviceStatusSearchList(Int64 userId, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();

            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Last Reading";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Equipment Name";
            Item.Value = "2";
            list.Add(Item);




            return list;
        }


        public List<ISelectListItem> GetTempDeivceSearchList(Int64 userId, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();

            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Device Name";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Mac Address";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Device IP";
            Item.Value = "3";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "System";
            Item.Value = "8";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Organization";
            Item.Value = "4";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Workstation";
            Item.Value = "5";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Lower Limit";
            Item.Value = "6";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Upper Limit";
            Item.Value = "7";
            list.Add(Item);

            return list;
        }

       

        public List<ISelectListItem> GetThermoTypeSearchList(long userId)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Name";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Code";
            Item.Value = "2";
            list.Add(Item);





            return list;
        }

        public List<ISelectListItem> GetMeasureUnitList(long userId)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Name";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Symbol";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Code";
            Item.Value = "3";
            list.Add(Item);

            return list;
        }

        public List<ISelectListItem> GetEmailGroupSearchList(long userId, bool addAllItem = false)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            if (addAllItem)
                AddAllItem(list);

            ISelectListItem Item = new SelectListItem();

            Item = new SelectListItem();
            Item.Text = "Name ";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Mail To";
            Item.Value = "2";
            list.Add(Item);



            return list;
        }

        public List<ISelectListItem> GetEmailServerSearchList(long userId, bool addAllItem = false)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            if (addAllItem)
                AddAllItem(list);

            ISelectListItem Item = new SelectListItem();

            Item.Text = "Name";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "IP";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Email Port";
            Item.Value = "3";
            list.Add(Item);

            return list;
        }

        public List<ISelectListItem> GetObjectList(long userId, bool addAllItem = false)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            if (addAllItem)
                AddAllItem(list);

            ISelectListItem Item = new SelectListItem();

            Item = new SelectListItem();
            Item.Text = "Module";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Name";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Caption";
            Item.Value = "3";
            list.Add(Item);

            return list;
        }

        public List<ISelectListItem> GetAlarmSearchList(long userId, bool addAllItem = false)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            if (addAllItem)
                AddAllItem(list);

            ISelectListItem Item = new SelectListItem();

            Item = new SelectListItem();
            Item.Text = "Alarm Sound";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Alarm Name";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Fixed Alarm Time";
            Item.Value = "3";
            list.Add(Item);

            return list;
        }

        public List<ISelectListItem> GetMailListSearchList(Int64 userId, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Facility Name";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Mail List Name";
            Item.Value = "1";
            list.Add(Item);


            return list;
        }

        public List<ISelectListItem> GetCorrectiveActionListSearch(long userId, bool addAllItem = false)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            if (addAllItem)
                AddAllItem(list);

            ISelectListItem Item = new SelectListItem();

            Item = new SelectListItem();

            Item = new SelectListItem();
            Item.Text = "Name";
            Item.Value = "1";
            list.Add(Item);

            return list;
        }
        public List<ISelectListItem> GetUnassignedProbeList(IDeviceModel model, bool addAllItem = false)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.DeviceId, GetParameter(DBObjects.SPParameter.DeviceId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.DeviceId));

           // dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspUnassignedProbe.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["ProbeSerailNo"].ToString(),
                            Value = dr["ProbeId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetAssignedProbeList(IDeviceModel model, bool addAllItem = false)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.DeviceId, GetParameter(DBObjects.SPParameter.DeviceId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.DeviceId));

          //  dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspAssignedProbe.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["ProbeSerailNo"].ToString(),
                            Value = dr["ProbeId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetUnassignedGroupList(IDeviceModel model, bool addAllItem = false)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.DeviceId, GetParameter(DBObjects.SPParameter.DeviceId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.DeviceId));

            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspUnassignedGroup.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["GroupName"].ToString(),
                            Value = dr["TempGroupId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetAssignedGroupList(IDeviceModel model, bool addAllItem = false)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.DeviceId, GetParameter(DBObjects.SPParameter.DeviceId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.DeviceId));

            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspAssignedGroup.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["GroupName"].ToString(),
                            Value = dr["TempGroupId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }




        public List<ISelectListItem> GetUserSearchList(Int64 userId, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();

            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Name";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Email";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Organization";
            Item.Value = "3";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Phone";
            Item.Value = "4";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "State";
            Item.Value = "5";
            list.Add(Item);

            return list;
        }

        public List<ISelectListItem> GetEscalationList(long userId, bool addAllItem = true)
        {

            DataTable dt = null;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLAvailableEscalation.ToString(), ref param);
            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Name"].ToString(),
                            Value = dr["EscalationId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);


            return list;
        }


        public List<ISelectListItem> GetUnassignedCorrectiveActionTypeList(long userId, long? correctiveActionListId, bool addAllItem = false)
        {
            DataTable dt = null;
            if (correctiveActionListId == null)
                correctiveActionListId = 0;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            param.Add(DBObjects.SPParameter.CorrectiveActionListId, GetParameter(DBObjects.SPParameter.CorrectiveActionListId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, correctiveActionListId));

            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspGetCorrectiveActionTypeUnassigned.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Name"].ToString(),
                            Value = dr["CorrectiveActionTypeId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetAssignedCorrectiveActionTypeList(long userId, long? correctiveActionListId, bool addAllItem = false)
        {
            DataTable dt = null;
            if (correctiveActionListId == null)
                correctiveActionListId = 0;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            param.Add(DBObjects.SPParameter.CorrectiveActionListId, GetParameter(DBObjects.SPParameter.CorrectiveActionListId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, correctiveActionListId));

            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspGetCorrectiveActionTypeAssignedToList.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Name"].ToString(),
                            Value = dr["CorrectiveActionTypeId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }






        public List<ISelectListItem> GetSecurityQuestionList(long userId, ref long SecurityQuestionId, bool addAllItem = false)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            param.Add(DBObjects.SPParameter.SecurityQuestionId, GetParameter(DBObjects.SPParameter.SecurityQuestionId, ParameterDirection.Output, ((int)SqlDbType.BigInt), 8, 0));

            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLSecurityQuestion.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Question"].ToString(),
                            Value = dr["SecurityQuestionId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            if (userId != 0)
                SecurityQuestionId = Convert.ToInt64(((SqlParameter)param[DBObjects.SPParameter.SecurityQuestionId]).Value);

            return list;
        }

        public List<ISelectListItem> GeRoleList(long userId, bool addAllItem = false)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLRole.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Name"].ToString(),
                            Value = dr["RoleId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetGenderList(Int64 userId, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLGender.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Name"].ToString(),
                            Value = dr["GenderId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetAssignedCorrectiveActionTypeListAsync(long userId, CancellationToken cancellationToken, long? correctiveActionListId, bool addAllItem = false)
        {
            DataTable dt = null;
            if (correctiveActionListId == null)
                correctiveActionListId = 0;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            param.Add(DBObjects.SPParameter.CorrectiveActionListId, GetParameter(DBObjects.SPParameter.CorrectiveActionListId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, correctiveActionListId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspGetCorrectiveActionTypeAssignedToList.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Name"].ToString(),
                            Value = dr["CorrectiveActionTypeId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetAlarmTypeList(Int64 userId, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLAlarmType.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Text"].ToString(),
                            Value = dr["Value"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetDisplayResoulationList(long userId, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDisplayResoulation.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Text"].ToString(),
                            Value = dr["Value"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetAlarmOnOffList(long userId, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLAlarmOnOff.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();

            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Text"].ToString(),
                            Value = dr["Value"].ToString(),

                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetDisplayOnCTCList(long userId, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLDisplayONCTC.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Text"].ToString(),
                            Value = dr["Value"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public List<ISelectListItem> GetRankList(long userId, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspDDLRank.ToString(), ref param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Text"].ToString(),
                            Value = dr["Value"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        #region Async Implementation
        public async Task<List<ISelectListItem>> GetSuppressIntervalListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = false)
        {
            DataTable dt = null;
            List<ISelectListItem> list = new List<ISelectListItem>();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLSuppressInterval.ToString(), cancellationToken, param);

            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["SuppressInterval"].ToString(),
                            Value = dr["SuppressIntervalId"].ToString()

                        }).ToList<ISelectListItem>();
            }
            if (addAllItem)
                AddAllItem(list);


            return list;
        }
        public async Task<List<ISelectListItem>> GetDashboardErrorDeviceStatusSearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();

            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Temp Reading";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Equipment Name";
            Item.Value = "2";
            list.Add(Item);




            return await Task.FromResult(list);
        }

        public async Task<List<ISelectListItem>> GetDashboardErrorSearchListAsync(Int64 userId, CancellationToken cancellationToken, bool flag = false, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();

            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Facility";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Equipment";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Last Reading";
            Item.Value = "3";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Reason For Alert";
            Item.Value = "4";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Alert Time";
            Item.Value = "5";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Last Contact";
            Item.Value = "6";
            list.Add(Item);

            return await Task.FromResult(list);
        }

        public async Task<List<ISelectListItem>> GetOrganizationListAsync(Int64 userId, Int64 systemId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            param.Add(DBObjects.SPParameter.SystemId, GetParameter(DBObjects.SPParameter.SystemId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, systemId));

            if (systemId > 0 || systemId == -2)
                dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLOrganization.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["OrganizationName"].ToString(),
                            Value = dr["OrganizationId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }
        public async Task<List<ISelectListItem>> GetWSModelsListAsync(Int64 userId, Int64 systemId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            param.Add(DBObjects.SPParameter.SystemId, GetParameter(DBObjects.SPParameter.SystemId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, systemId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLWSModel.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["ModelName"].ToString(),
                            Value = dr["WSModelId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }
        public async Task<List<ISelectListItem>> GetSystemListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;
            List<ISelectListItem> list = new List<ISelectListItem>();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLSystem.ToString(), cancellationToken, param);

            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["SystemName"].ToString(),
                            Value = dr["SystemId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }


        public async Task<List<ISelectListItem>> GetSystemListForReadingSampleAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;
            List<ISelectListItem> list = new List<ISelectListItem>();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLSystemForReadingSample.ToString(), cancellationToken, param);

            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["SystemName"].ToString(),
                            Value = dr["SystemId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }


        public async Task<List<ISelectListItem>> GetCorrectiveActionListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = false)
        {
            DataTable dt = null;
            List<ISelectListItem> list = new List<ISelectListItem>();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLCorrectiveActionList.ToString(), cancellationToken, param);

            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Name"].ToString(),
                            Value = dr["CorrectiveActionListId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

  
        public async Task<List<ISelectListItem>> GetThemeListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLTheme.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Name"].ToString(),
                            Value = dr["ThemeId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

   
      
        public async Task<List<ISelectListItem>> GetTimeZoneListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspTimeZoneSelect.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            list = (from DataRow dr in dt.Rows
                    select new SelectListItem
                    {
                        Text = dr["DisplayName"].ToString(),
                        Value = dr["TimeZoneId"].ToString(),
                    }).ToList<ISelectListItem>();

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

   
   
        public async Task<List<ISelectListItem>> GetModuleListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLModule.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr[DBObjects.Fields.Name].ToString(),
                            Value = dr[DBObjects.Fields.ModuleId].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetObjectLevelListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLObjectLevel.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr[DBObjects.Fields.ObjectLevel].ToString(),
                            Value = dr[DBObjects.Fields.ObjectLevelId].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetParentObjectListAsync(long userId, CancellationToken cancellationToken, long objectLvl, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.ObjectLevel, GetParameter(DBObjects.SPParameter.ObjectLevel, ParameterDirection.Input, ((int)SqlDbType.Int), 4, objectLvl));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            if (objectLvl > 0)
                dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLParentObjectList.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr[DBObjects.Fields.Name].ToString(),
                            Value = dr[DBObjects.Fields.ObjectId].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetBusinessTypesListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLBusinessTypeList.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr[DBObjects.Fields.BusinessType].ToString(),
                            Value = dr[DBObjects.Fields.BusinessTypeId].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetAlarmSoundListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLAlarmList.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr[DBObjects.Fields.SoundName].ToString(),
                            Value = dr[DBObjects.Fields.AlarmSoundId].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetOrganizationLevelListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLOrganizationLevelList.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr[DBObjects.Fields.Text].ToString(),
                            Value = dr[DBObjects.Fields.Value].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> PopulateParentObjectLevelDropDownListAsync(long userId, CancellationToken cancellationToken, long? orgLvl, long systemId, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.OrganizationLevelID, GetParameter(DBObjects.SPParameter.OrganizationLevelID, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, orgLvl));
            param.Add(DBObjects.SPParameter.SystemId, GetParameter(DBObjects.SPParameter.SystemId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, systemId));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLParentOrganizationList.ToString(), cancellationToken, param);


            List<ISelectListItem> list = new List<ISelectListItem>();

            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr[DBObjects.Fields.OrganizationName].ToString(),
                            Value = dr[DBObjects.Fields.OrganizationId].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetSearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Organization Control No";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Organization Name";
            Item.Value = "2";
            list.Add(Item);
            return list;
        }
        public async Task<List<ISelectListItem>> GetPageSizeList(CancellationToken cancellationToken)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLPageSize.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr[DBObjects.Fields.Text].ToString(),
                            Value = dr[DBObjects.Fields.Value].ToString(),

                        }).ToList<ISelectListItem>();
            }


            return list;
        }

        public async Task<List<ISelectListItem>> GetMeasureUnitListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = false)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLMeasureUnit.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();

            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr[DBObjects.Fields.Name].ToString(),
                            Value = dr[DBObjects.Fields.MeasureUnitId].ToString(),

                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetProbeCalibrationFrequencyListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

           // dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLProbeCalibrationFrequency.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["ProbeCalFreqName"].ToString(),
                            Value = dr["ProbeCalFreqId"].ToString(),
                            IsOther = Convert.ToBoolean(dr["IsOther"])
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetOrganizationSearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Facility Name";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Customer No";
            Item.Value = "2";
            list.Add(Item);
            return list;
        }


        public async Task<List<ISelectListItem>> GetSystemSearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Organization Control No";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Organization Name";
            Item.Value = "2";
            list.Add(Item);
            return list;
        }
        public async Task<List<ISelectListItem>> GetWorkstationSearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "WS Name";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "WS Serial No";
            Item.Value = "2";
            list.Add(Item);
            return list;
        }

        public async Task<List<ISelectListItem>> GetThemeSearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Name";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Code";
            Item.Value = "2";
            list.Add(Item);




            return list;
        }

        public async Task<List<ISelectListItem>> GetProbeSearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();

            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Serial No";
            Item.Value = "1";
            list.Add(Item);

            //Item = new SelectListItem();
            //Item.Text = "Probe Name";
            //Item.Value = "2";
            //list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Probe Type";
            Item.Value = "3";
            list.Add(Item);

            //Item = new SelectListItem();
            //Item.Text = "Manufacture";
            //Item.Value = "4";
            //list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Thermostat Type";
            Item.Value = "5";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Calibration Frequency";
            Item.Value = "6";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Expiry Date";
            Item.Value = "7";
            list.Add(Item);

            return list;
        }

        public async Task<List<ISelectListItem>> GetDashboardErrorSearchList(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();

            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Last Reading";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Reason For Alert";
            Item.Value = "2";
            list.Add(Item);




            return list;
        }

        public async Task<List<ISelectListItem>> GetTempDeivceSearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();

            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Address";
            Item.Value = "8";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Device Name";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Mac Address";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Device IP";
            Item.Value = "3";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "System";
            Item.Value = "8";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Organization";
            Item.Value = "4";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Workstation";
            Item.Value = "5";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Lower Limit";
            Item.Value = "6";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Upper Limit";
            Item.Value = "7";
            list.Add(Item);

            return list;
        }

        public async Task<List<ISelectListItem>> GetTempDeivceResetSearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();

            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Organization";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Facility";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Device Name";
            Item.Value = "3";
            list.Add(Item);

            return list;
        }

        public async Task<List<ISelectListItem>> GetThermoTypeSearchListAsync(long userId, CancellationToken cancellationToken)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Name";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Code";
            Item.Value = "2";
            list.Add(Item);





            return list;
        }

        public async Task<List<ISelectListItem>> GetMeasureUnitListAsync(long userId, CancellationToken cancellationToken)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Name";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Symbol";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Code";
            Item.Value = "3";
            list.Add(Item);

            return list;
        }

        public async Task<List<ISelectListItem>> GetEmailGroupSearchListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = false)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            if (addAllItem)
                AddAllItem(list);

            ISelectListItem Item = new SelectListItem();

            Item = new SelectListItem();
            Item.Text = "Group Name ";
            Item.Value = "1";
            list.Add(Item);





            return list;
        }

        public async Task<List<ISelectListItem>> GetEmailServerSearchListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = false)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            if (addAllItem)
                AddAllItem(list);

            ISelectListItem Item = new SelectListItem();

            Item.Text = "Name";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "IP";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Email Port";
            Item.Value = "3";
            list.Add(Item);

            return list;
        }

        public async Task<List<ISelectListItem>> GetObjectListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = false)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            if (addAllItem)
                AddAllItem(list);

            ISelectListItem Item = new SelectListItem();

            Item = new SelectListItem();
            Item.Text = "Module";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Name";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Caption";
            Item.Value = "3";
            list.Add(Item);

            return list;
        }

        public async Task<List<ISelectListItem>> GetWebApiObjectListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = false)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            if (addAllItem)
                AddAllItem(list);

            ISelectListItem Item = new SelectListItem();

            Item = new SelectListItem();
            Item.Text = "Module";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Name";
            Item.Value = "2";
            list.Add(Item);

            return list;
        }

        public async Task<List<ISelectListItem>> GetAlarmSearchListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = false)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            if (addAllItem)
                AddAllItem(list);

            ISelectListItem Item = new SelectListItem();

            Item = new SelectListItem();
            Item.Text = "Alarm Sound";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Alarm Name";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Fixed Alarm Time";
            Item.Value = "3";
            list.Add(Item);

            return list;
        }

        public async Task<List<ISelectListItem>> GetMailListSearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Facility Name";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Mail List Name";
            Item.Value = "1";
            list.Add(Item);


            return list;
        }

        public async Task<List<ISelectListItem>> GetCorrectiveActionListSearchAsync(long userId, CancellationToken cancellationToken, bool addAllItem = false)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            if (addAllItem)
                AddAllItem(list);

            ISelectListItem Item = new SelectListItem();

            Item = new SelectListItem();

            Item = new SelectListItem();
            Item.Text = "Name";
            Item.Value = "1";
            list.Add(Item);

            return list;
        }
        public async Task<List<ISelectListItem>> GetUnassignedProbeListAsync(IDeviceModel model, CancellationToken cancellationToken, bool addAllItem = false)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.DeviceId, GetParameter(DBObjects.SPParameter.DeviceId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.DeviceId));

           // dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspUnassignedProbe.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            //if (dt != null)
            //{
            //    list = (from DataRow dr in dt.Rows
            //            select new SelectListItem
            //            {
            //                Text = dr["ProbeSerailNo"].ToString(),
            //                Value = dr["ProbeId"].ToString(),
            //            }).ToList<ISelectListItem>();
            //}

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetAssignedProbeListAsync(IDeviceModel model, CancellationToken cancellationToken, bool addAllItem = false)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.DeviceId, GetParameter(DBObjects.SPParameter.DeviceId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.DeviceId));

        //    dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspAssignedProbe.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["ProbeSerailNo"].ToString(),
                            Value = dr["ProbeId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetUnassignedGroupList(IDeviceModel model, CancellationToken cancellationToken, bool addAllItem = false)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.DeviceId, GetParameter(DBObjects.SPParameter.DeviceId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.DeviceId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspUnassignedGroup.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["GroupName"].ToString(),
                            Value = dr["TempGroupId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetAssignedGroupListAsync(IDeviceModel model, CancellationToken cancellationToken, bool addAllItem = false)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.DeviceId, GetParameter(DBObjects.SPParameter.DeviceId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.DeviceId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspAssignedGroup.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["GroupName"].ToString(),
                            Value = dr["TempGroupId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }




        public async Task<List<ISelectListItem>> GetUserSearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();

            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Name";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Email";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Organization";
            Item.Value = "3";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Phone";
            Item.Value = "4";
            list.Add(Item);

            //Item = new SelectListItem();
            //Item.Text = "State";
            //Item.Value = "5";
            //list.Add(Item);

            return list;
        }

        public async Task<List<ISelectListItem>> GetWebApiUserSearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();

            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Name";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Email";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Phone";
            Item.Value = "3";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "State";
            Item.Value = "4";
            list.Add(Item);

            return list;
        }

        public async Task<List<ISelectListItem>> GetEscalationListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = true)
        {

            DataTable dt = null;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLAvailableEscalation.ToString(), cancellationToken, param);
            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Name"].ToString(),
                            Value = dr["EscalationId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);


            return list;
        }


        public async Task<List<ISelectListItem>> GetUnassignedCorrectiveActionTypeListAsync(long userId, CancellationToken cancellationToken, long? correctiveActionListId, bool addAllItem = false)
        {
            DataTable dt = null;
            if (correctiveActionListId == null)
                correctiveActionListId = 0;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            param.Add(DBObjects.SPParameter.CorrectiveActionListId, GetParameter(DBObjects.SPParameter.CorrectiveActionListId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, correctiveActionListId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspGetCorrectiveActionTypeUnassigned.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Name"].ToString(),
                            Value = dr["CorrectiveActionTypeId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetAssignedCorrectiveActionTypeList(long userId, CancellationToken cancellationToken, long? correctiveActionListId, bool addAllItem = false)
        {
            DataTable dt = null;
            if (correctiveActionListId == null)
                correctiveActionListId = 0;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            param.Add(DBObjects.SPParameter.CorrectiveActionListId, GetParameter(DBObjects.SPParameter.CorrectiveActionListId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, correctiveActionListId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspGetCorrectiveActionTypeAssignedToList.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Name"].ToString(),
                            Value = dr["CorrectiveActionTypeId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetUnassignedDeviceListAsync(long userId, CancellationToken cancellationToken, long? groupId, bool addAllItem = false)
        {
            DataTable dt = null;
            if (groupId == null)
                groupId = 0;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            param.Add(DBObjects.SPParameter.TempGroupId, GetParameter(DBObjects.SPParameter.TempGroupId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, groupId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDeviceUnassignedToGroup.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["DeviceName"].ToString(),
                            Value = dr["DeviceId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetAssignedDeviceListAsync(long userId, CancellationToken cancellationToken, long? groupId, bool addAllItem = false)
        {
            DataTable dt = null;
            if (groupId == null)
                groupId = 0;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            param.Add(DBObjects.SPParameter.TempGroupId, GetParameter(DBObjects.SPParameter.TempGroupId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, groupId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDeviceAssignedToGroup.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["DeviceName"].ToString(),
                            Value = dr["DeviceId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }



     
        public async Task<List<ISelectListItem>> GetSecurityQuestionListAsync(long userId, CancellationToken cancellationToken, long SecurityQuestionId, bool addAllItem = false)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            param.Add(DBObjects.SPParameter.SecurityQuestionId, GetParameter(DBObjects.SPParameter.SecurityQuestionId, ParameterDirection.Output, ((int)SqlDbType.BigInt), 8, 0));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLSecurityQuestion.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Question"].ToString(),
                            Value = dr["SecurityQuestionId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            if (userId != 0)
                SecurityQuestionId = Convert.ToInt64(((SqlParameter)param[DBObjects.SPParameter.SecurityQuestionId]).Value);

            return list;
        }

        public async Task<List<ISelectListItem>> GetRoleListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = false)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLRole.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Name"].ToString(),
                            Value = dr["RoleId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetWebAPIRoleListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = false)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLWebAPIRole.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Name"].ToString(),
                            Value = dr["RoleId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetGenderListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLGender.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Name"].ToString(),
                            Value = dr["GenderId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }



        public async Task<List<ISelectListItem>> GetAlarmTypeListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLAlarmType.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Text"].ToString(),
                            Value = dr["Value"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetDisplayResoulationListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDisplayResoulation.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Text"].ToString(),
                            Value = dr["Value"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetAlarmOnOffListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLAlarmOnOff.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();

            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Text"].ToString(),
                            Value = dr["Value"].ToString(),

                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetDisplayOnCTCListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLDisplayONCTC.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Text"].ToString(),
                            Value = dr["Value"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetRankListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLRank.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Text"].ToString(),
                            Value = dr["Value"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetServiceObjectSearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Name";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Notes";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Interval";
            Item.Value = "3";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Alert Interval";
            Item.Value = "4";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Heartbeat Type";
            Item.Value = "5";
            list.Add(Item);

            return list;
        }

        public async Task<List<ISelectListItem>> GetServiceObjectHeartbeatListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();

            Item = new SelectListItem();
            Item.Text = "Normal";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Debug";
            Item.Value = "2";
            list.Add(Item);

            return list;
        }

        public async Task<List<ISelectListItem>> GetTCPServerSearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Server Name";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Server IP";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "TCP Port";
            Item.Value = "3";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Alert Interval";
            Item.Value = "4";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Heartbeat Interval";
            Item.Value = "5";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Heartbeat Type";
            Item.Value = "6";
            list.Add(Item);

            return list;
        }


        public async Task<List<ISelectListItem>> GetCorrectiveActionSearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Corrective Action";
            Item.Value = "1";
            list.Add(Item);

            return list;
        }

        public async Task<List<ISelectListItem>> GetCategorySearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Category";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Theme";
            Item.Value = "2";
            list.Add(Item);
            return list;
        }

        public async Task<List<ISelectListItem>> GetRootCauseSearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Root Cause";
            Item.Value = "1";
            list.Add(Item);

            return list;
        }

        public async Task<List<ISelectListItem>> GetSymptomSearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Symptom";
            Item.Value = "1";
            list.Add(Item);

            return list;
        }

        public async Task<List<ISelectListItem>> GetCorrectiveActionSettingSearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Category";
            Item.Value = "1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Root Cause";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Symptom";
            Item.Value = "3";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Corrective Action";
            Item.Value = "4";
            list.Add(Item);

            return list;
        }

        public async Task<List<ISelectListItem>> GetCACategoryListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLCACategory.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Category"].ToString(),
                            Value = dr["CategoryId"].ToString(),
                            IsOther = Convert.ToBoolean(dr["IsOther"])
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetCARootCauseListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLCARootCause.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["RootCause"].ToString(),
                            Value = dr["RootCauseId"].ToString(),
                            IsOther = Convert.ToBoolean(dr["IsOther"])
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetCASymptomListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLCASymptom.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["Symptom"].ToString(),
                            Value = dr["SymptomId"].ToString(),
                            IsOther = Convert.ToBoolean(dr["IsOther"])
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetCACorrectiveActionListAsync(long userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspDDLCACorrectiveAction.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["CorrectiveAction"].ToString(),
                            Value = dr["CorrectiveActionId"].ToString(),
                            IsOther = Convert.ToBoolean(dr["IsOther"])
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }
    
        public async Task<string> SelectCurrentDateTimeLocalizedAsync(long userId, CancellationToken cancellationToken)
        {

            DataTable dt;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspGetExportGridMetaInfo.ToString(), cancellationToken, param);
            return dt.Rows[0]["RetVal"].ToString(); ;
        }
        
        public async Task<List<ISelectListItem>> GetReadingSampleTypeListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            foreach (int value in Enum.GetValues(typeof(ReadingSampleType)))
            {
                Item = new SelectListItem();
                Item.Text = Enum.GetName(typeof(ReadingSampleType), value);
                Item.Value = value.ToString();
                list.Add(Item);
            }

            return list;
        }




        public async Task<List<ISelectListItem>> GetReadingSampleSearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);
            Item = new SelectListItem();
            Item.Text = "Organization";
            Item.Value = "1";
            list.Add(Item);
            Item = new SelectListItem();
            Item.Text = "Facility ";
            Item.Value = "2";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Device";
            Item.Value = "3";
            list.Add(Item);


            return list;
        }
        #endregion



        public async Task<List<ISelectListItem>> GetCountryListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;
            List<ISelectListItem> list = new List<ISelectListItem>();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.hspDDLCountry.ToString(), cancellationToken, param);

            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["CountryName"].ToString(),
                            Value = dr["CountryId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

        public async Task<List<ISelectListItem>> GetCountrySearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Country Name";
            Item.Value = "1";
            list.Add(Item);

           
            return list;
        }

        public async Task<List<ISelectListItem>> GetLocationListAsync(Int64 userId, Int64 CountryId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            param.Add(DBObjects.SPParameter.CountryId, GetParameter(DBObjects.SPParameter.CountryId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, CountryId));

            if (CountryId > 0 || CountryId == -2)
                dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.hspDDLLocation.ToString(), cancellationToken, param);

            List<ISelectListItem> list = new List<ISelectListItem>();
            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["LocationName"].ToString(),
                            Value = dr["LocationId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }
        public async Task<List<ISelectListItem>> GetLocationSearchListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            List<ISelectListItem> list = new List<ISelectListItem>();

            ISelectListItem Item = new SelectListItem();
            Item.Text = "--Select--";
            Item.Value = "-1";
            list.Add(Item);

            Item = new SelectListItem();
            Item.Text = "Location Name";
            Item.Value = "1";
            list.Add(Item);

           
            return list;
        }


        public async Task<List<ISelectListItem>> GetCategoryListAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;
            List<ISelectListItem> list = new List<ISelectListItem>();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.hspDDLCategory.ToString(), cancellationToken, param);

            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["CategoryName"].ToString(),
                            Value = dr["CategoryId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }
        public async Task<List<ISelectListItem>> GetCategoryListForHomePageAsync(Int64 userId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;
            List<ISelectListItem> list = new List<ISelectListItem>();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.hspDDLCategoryForHomePage.ToString(), cancellationToken, param);

            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["CategoryName"].ToString(),
                            Value = dr["CategoryId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }


        public async Task<List<ISelectListItem>> GetCategoryListByParentAsync(Int64 userId, long parentId, CancellationToken cancellationToken, bool addAllItem = true)
        {
            DataTable dt = null;
            List<ISelectListItem> list = new List<ISelectListItem>();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            param.Add(DBObjects.SPParameter.ParentId, GetParameter(DBObjects.SPParameter.ParentId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull( parentId)));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.hspDDLCategoryByParent.ToString(), cancellationToken, param);

            if (dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new SelectListItem
                        {
                            Text = dr["CategoryName"].ToString(),
                            Value = dr["CategoryId"].ToString(),
                        }).ToList<ISelectListItem>();
            }

            if (addAllItem)
                AddAllItem(list);

            return list;
        }

    }
}
