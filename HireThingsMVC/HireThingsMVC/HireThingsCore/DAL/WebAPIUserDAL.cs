using ASOL.HireThings.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ASOL.HireThings.Core
{
    public class WebApiUserDAL : AbstractBaseDAL<IWebApiUser>
    {
        public override async Task<long> SaveAsync(IWebApiUser model, CancellationToken cancellationToken)
        {

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.SystemId, GetParameter(DBObjects.SPParameter.SystemId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.SystemId)));
            //param.Add(DBObjects.SPParameter.OrganizationID, GetParameter(DBObjects.SPParameter.OrganizationID, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.OrganizationId)));
            param.Add(DBObjects.SPParameter.WARoleId, GetParameter(DBObjects.SPParameter.WARoleId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.WARoleId)));
            param.Add(DBObjects.SPParameter.FIRSTNAME, GetParameter(DBObjects.SPParameter.FIRSTNAME, ParameterDirection.Input, ((int)SqlDbType.VarChar), 30, PublicFunctions.ConvertNulltoDBNull(model.FirstName)));
            param.Add(DBObjects.SPParameter.MIDDLENAME, GetParameter(DBObjects.SPParameter.MIDDLENAME, ParameterDirection.Input, ((int)SqlDbType.VarChar), 1, PublicFunctions.ConvertNulltoDBNull(model.MiddleName)));
            param.Add(DBObjects.SPParameter.LASTNAME, GetParameter(DBObjects.SPParameter.LASTNAME, ParameterDirection.Input, ((int)SqlDbType.VarChar), 30, PublicFunctions.ConvertNulltoDBNull(model.LastName)));
            param.Add(DBObjects.SPParameter.EMAIL, GetParameter(DBObjects.SPParameter.EMAIL, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, PublicFunctions.ConvertNulltoDBNull(model.EmailId)));
            param.Add(DBObjects.SPParameter.PHONENUMBER, GetParameter(DBObjects.SPParameter.PHONENUMBER, ParameterDirection.Input, ((int)SqlDbType.VarChar), 30, PublicFunctions.ConvertNulltoDBNull(model.PhoneNo)));
            param.Add(DBObjects.SPParameter.Address1, GetParameter(DBObjects.SPParameter.Address1, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, PublicFunctions.ConvertNulltoDBNull(model.Address1)));
            param.Add(DBObjects.SPParameter.Address2, GetParameter(DBObjects.SPParameter.Address2, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, PublicFunctions.ConvertNulltoDBNull(model.Address2)));
            param.Add(DBObjects.SPParameter.Address3, GetParameter(DBObjects.SPParameter.Address3, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, PublicFunctions.ConvertNulltoDBNull(model.Address3)));
            param.Add(DBObjects.SPParameter.PIN, GetParameter(DBObjects.SPParameter.PIN, ParameterDirection.Input, ((int)SqlDbType.VarChar), 20, model.Pin));
            param.Add(DBObjects.SPParameter.Active, GetParameter(DBObjects.SPParameter.Active, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.RFIDPIN, GetParameter(DBObjects.SPParameter.RFIDPIN, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, "0x01000000428EC9DAF430F49BD0DBD9CF4994647BA7EABC48E7AF9D7E"));
            param.Add(DBObjects.SPParameter.WAUserId, GetParameter(DBObjects.SPParameter.WAUserId, ParameterDirection.Output, ((int)SqlDbType.BigInt), 8, 0));
            param.Add(DBObjects.SPParameter.TimeZoneID, GetParameter(DBObjects.SPParameter.TimeZoneID, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.TimeZoneId));
            //param.Add(DBObjects.SPParameter.PINHash, GetParameter(DBObjects.SPParameter.PINHash, ParameterDirection.Input, ((int)SqlDbType.VarChar), 255, model.PINHash));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 1));            
            param.Add(DBObjects.SPParameter.AddUserId, GetParameter(DBObjects.SPParameter.AddUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));                       

            await this.ExecuteSPAsync(DBObjects.StoredProcedures.webApi_pspUserSave.ToString(), cancellationToken, param);
            return (long)(((SqlParameter)param[DBObjects.SPParameter.WAUserId]).Value);
        }

        public override Task<List<IWebApiUser>> SelectAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<IWebApiUser>> SelectAllByFilterAsync(IPredicate predicate, CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
            string retVal = predicate.Serialize();

            DataTable dt;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.Predicate, GetParameter(DBObjects.SPParameter.Predicate, ParameterDirection.Input, ((int)SqlDbType.NVarChar), retVal.Length, retVal));
            param.Add(DBObjects.SPParameter.CPage, GetParameter(DBObjects.SPParameter.CPage, ParameterDirection.Input, ((int)SqlDbType.Int), 8, pageInfo.CPage));
            param.Add(DBObjects.SPParameter.PSize, GetParameter(DBObjects.SPParameter.PSize, ParameterDirection.Input, ((int)SqlDbType.Int), 8, Convert.ToInt32((pageInfo.PSize))));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, pageInfo.UserId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.webApi_pspUserSearch.ToString(), cancellationToken, param);
            List<IWebApiUser> list = new List<IWebApiUser>();
            list = (from DataRow dr in dt.Rows
                    select new WebApiUser
                    {
                        WAUserId = Convert.ToString(dr["WAUserId"]),
                        FirstName = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dr["FirstName"])),
                        MiddleName = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dr["MiddleName"])),
                        LastName = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dr["LastName"])),
                        EmailId = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dr["EmailId"])),
                        PhoneNo = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dr["PhoneNo"])),
                        Address2 = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dr["Address2"])),
                        IsActive = Convert.ToBoolean(PublicFunctions.ConvertDBNullToNull(dr["IsActive"])),
                        IsSystemGenerated = Convert.ToBoolean(PublicFunctions.ConvertDBNullToNull(dr[DBObjects.Fields.IsSystemGenerated])),
                        TRows = Convert.ToInt32(dr[DBObjects.Fields.TRows]),
                        CPage = Convert.ToInt32(dr[DBObjects.Fields.CPage]),
                        PSize = dr[DBObjects.Fields.PSize].ToString()
                    }).ToList<IWebApiUser>();

            return list;
        }

        public override async Task<IWebApiUser> SelectByIDAsync(long id, long userId, CancellationToken cancellationToken)
        {
            DataTable dt = null;
            Dictionary<string, object> param = new Dictionary<string, object>();
            IWebApiUser user = null;

            param.Add(DBObjects.SPParameter.WAUserId, GetParameter(DBObjects.SPParameter.WAUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, id));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.webApi_pspSelectUserById.ToString(), cancellationToken, param);

            if (dt != null && dt.Rows.Count > 0)
            {
                user = new WebApiUser();

                user.SystemId = Convert.ToInt64(PublicFunctions.ConvertDBNullToNull(dt.Rows[0]["SystemId"]));
                user.WARoleId = Convert.ToInt64(PublicFunctions.ConvertDBNullToNull(dt.Rows[0]["WARoleId"]));
                user.EmailId = Convert.ToString(dt.Rows[0]["EmailId"]);
                user.ConfirmEmailId = Convert.ToString(dt.Rows[0]["EmailId"]);
                user.UserName = Convert.ToString(dt.Rows[0]["UserName"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.MiddleName = Convert.ToString(dt.Rows[0]["MiddleName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.SystemName = Convert.ToString(dt.Rows[0]["SystemName"]);
                user.RoleName = Convert.ToString(dt.Rows[0]["RoleName"]);
                user.PhoneNo = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dt.Rows[0]["PhoneNo"]));
                user.Address1 = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dt.Rows[0]["Address1"]));
                user.Address2 = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dt.Rows[0]["Address2"]));
                user.Address3 = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dt.Rows[0]["Address3"]));
                user.WAUserId = dt.Rows[0]["WAUserId"].ToString();
                user.TimeZoneId = Convert.ToInt64(dt.Rows[0]["TimeZoneID"]);
                user.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
            }

            return user;
        }

        public override async Task<bool> UpdateAsync(IWebApiUser model, CancellationToken cancellationToken)
        {            

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.SystemId, GetParameter(DBObjects.SPParameter.SystemId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.SystemId)));
            param.Add(DBObjects.SPParameter.OrganizationID, GetParameter(DBObjects.SPParameter.OrganizationID, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.OrganizationId)));
            param.Add(DBObjects.SPParameter.WARoleId, GetParameter(DBObjects.SPParameter.WARoleId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.WARoleId));
            param.Add(DBObjects.SPParameter.FIRSTNAME, GetParameter(DBObjects.SPParameter.FIRSTNAME, ParameterDirection.Input, ((int)SqlDbType.VarChar), 30, model.FirstName));
            param.Add(DBObjects.SPParameter.MIDDLENAME, GetParameter(DBObjects.SPParameter.MIDDLENAME, ParameterDirection.Input, ((int)SqlDbType.VarChar), 1, PublicFunctions.ConvertNulltoDBNull(model.MiddleName)));
            param.Add(DBObjects.SPParameter.LASTNAME, GetParameter(DBObjects.SPParameter.LASTNAME, ParameterDirection.Input, ((int)SqlDbType.VarChar), 30, model.LastName));
            param.Add(DBObjects.SPParameter.EMAIL, GetParameter(DBObjects.SPParameter.EMAIL, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, model.EmailId));
            param.Add(DBObjects.SPParameter.PHONENUMBER, GetParameter(DBObjects.SPParameter.PHONENUMBER, ParameterDirection.Input, ((int)SqlDbType.VarChar), 30, PublicFunctions.ConvertNulltoDBNull(model.PhoneNo)));
            param.Add(DBObjects.SPParameter.Address1, GetParameter(DBObjects.SPParameter.Address1, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, PublicFunctions.ConvertNulltoDBNull(model.Address1)));
            param.Add(DBObjects.SPParameter.Address2, GetParameter(DBObjects.SPParameter.Address2, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, PublicFunctions.ConvertNulltoDBNull(model.Address2)));
            param.Add(DBObjects.SPParameter.Address3, GetParameter(DBObjects.SPParameter.Address3, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, PublicFunctions.ConvertNulltoDBNull(model.Address3)));
            param.Add(DBObjects.SPParameter.Active, GetParameter(DBObjects.SPParameter.Active, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.WAUserId, GetParameter(DBObjects.SPParameter.WAUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.WAUserId));
            param.Add(DBObjects.SPParameter.TimeZoneID, GetParameter(DBObjects.SPParameter.TimeZoneID, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.TimeZoneId));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 1));
            param.Add(DBObjects.SPParameter.UpdateUserId, GetParameter(DBObjects.SPParameter.UpdateUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            await this.ExecuteSPAsync(DBObjects.StoredProcedures.webApi_pspUserUpdate.ToString(), cancellationToken, param);

            return Convert.ToBoolean((Int32)(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value));
        }
    }
}
