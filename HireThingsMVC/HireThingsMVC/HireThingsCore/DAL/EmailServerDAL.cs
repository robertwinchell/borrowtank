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
    public class EmailServerDAL : AbstractBaseDAL<IEmailServerModel>
    {
        public DataTable GetSystemEmailServer(long userId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            return this.GetSPDataTable(DBObjects.StoredProcedures.pspEmailServersGetActiveServers.ToString(), ref param);

        }

        #region
        public override async Task<long> SaveAsync(IEmailServerModel model, CancellationToken cancellationToken)
        {

            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add(DBObjects.SPParameter.ServerName, GetParameter(DBObjects.SPParameter.ServerName, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, model.ServerName));
            param.Add(DBObjects.SPParameter.ServerIP, GetParameter(DBObjects.SPParameter.ServerIP, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, model.ServerIP));
            param.Add(DBObjects.SPParameter.EmailPort, GetParameter(DBObjects.SPParameter.EmailPort, ParameterDirection.Input, ((int)SqlDbType.Int), 4, model.EmailPort));
            param.Add(DBObjects.SPParameter.UserName, GetParameter(DBObjects.SPParameter.UserName, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, model.UserName));
            param.Add(DBObjects.SPParameter.UserPswd, GetParameter(DBObjects.SPParameter.UserPswd, ParameterDirection.Input, ((int)SqlDbType.VarChar), -1, model.UserPswd));
            param.Add(DBObjects.SPParameter.UseDefaultCredentials, GetParameter(DBObjects.SPParameter.UseDefaultCredentials, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.UseDefaultCredentials));
            param.Add(DBObjects.SPParameter.EnableSSL, GetParameter(DBObjects.SPParameter.EnableSSL, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.EnableSSL));
            param.Add(DBObjects.SPParameter.IsActive, GetParameter(DBObjects.SPParameter.IsActive, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.AddUserId, GetParameter(DBObjects.SPParameter.AddUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.UserId)));
            param.Add(DBObjects.SPParameter.Priority, GetParameter(DBObjects.SPParameter.Priority, ParameterDirection.Input, ((int)SqlDbType.TinyInt), 1, PublicFunctions.ConvertNulltoDBNull(model.Priority)));
            param.Add(DBObjects.SPParameter.ServerId, GetParameter(DBObjects.SPParameter.ServerId, ParameterDirection.Output, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.ServerId)));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));


           await this.ExecuteSPAsync(DBObjects.StoredProcedures.pspEmailServerSave.ToString(), cancellationToken, param);

            return (long)(((SqlParameter)param[DBObjects.SPParameter.ServerId]).Value);
        }

        public override async Task<bool> UpdateAsync(IEmailServerModel model, CancellationToken cancellationToken)
        {

            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add(DBObjects.SPParameter.ServerName, GetParameter(DBObjects.SPParameter.ServerName, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, model.ServerName));
            param.Add(DBObjects.SPParameter.ServerIP, GetParameter(DBObjects.SPParameter.ServerIP, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, model.ServerIP));
            param.Add(DBObjects.SPParameter.EmailPort, GetParameter(DBObjects.SPParameter.EmailPort, ParameterDirection.Input, ((int)SqlDbType.Int), 4, model.EmailPort));
            param.Add(DBObjects.SPParameter.UserName, GetParameter(DBObjects.SPParameter.UserName, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, model.UserName));
            param.Add(DBObjects.SPParameter.UserPswd, GetParameter(DBObjects.SPParameter.UserPswd, ParameterDirection.Input, ((int)SqlDbType.VarChar), -1, model.UserPswd));
            param.Add(DBObjects.SPParameter.UseDefaultCredentials, GetParameter(DBObjects.SPParameter.UseDefaultCredentials, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.UseDefaultCredentials));
            param.Add(DBObjects.SPParameter.EnableSSL, GetParameter(DBObjects.SPParameter.EnableSSL, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.EnableSSL));
            param.Add(DBObjects.SPParameter.IsActive, GetParameter(DBObjects.SPParameter.IsActive, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.UpdateUserID, GetParameter(DBObjects.SPParameter.UpdateUserID, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.UserId)));
            param.Add(DBObjects.SPParameter.Priority, GetParameter(DBObjects.SPParameter.Priority, ParameterDirection.Input, ((int)SqlDbType.TinyInt), 1, PublicFunctions.ConvertNulltoDBNull(model.Priority)));
            param.Add(DBObjects.SPParameter.ServerId, GetParameter(DBObjects.SPParameter.ServerId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.ServerId)));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

            await this.ExecuteSPAsync(DBObjects.StoredProcedures.pspEmailServerUpdate.ToString(), cancellationToken, param);

            return Convert.ToBoolean((Int32)((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value);
        }

        public override async Task<List<IEmailServerModel>> SelectAllAsync(CancellationToken cancellationToken)
        {
            DataTable dt = null;
            Dictionary<string, object> param = new Dictionary<string, object>();

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspEmailServerSelect.ToString(), cancellationToken,  param);

            List<IEmailServerModel> list = new List<IEmailServerModel>();

            return list;
            ///
        }

        public override async Task<IEmailServerModel> SelectByIDAsync(long id, long userId, CancellationToken cancellationToken)
        {
            DataTable dt = null;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.ServerId, GetParameter(DBObjects.SPParameter.ServerId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, id));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspEmailServerByID.ToString(), cancellationToken, param);

            IEmailServerModel model = new EmailServerModel();
            model.ServerId = Convert.ToInt64(dt.Rows[0]["ServerId"]);
            model.ServerName = dt.Rows[0]["ServerName"].ToString();
            model.ServerIP = dt.Rows[0]["ServerIP"].ToString();
            model.EmailPort = Convert.ToInt32(dt.Rows[0]["EmailPort"]);
            model.UserName = dt.Rows[0]["UserName"].ToString();
            model.UserPswd = dt.Rows[0]["UserPswd"].ToString();
            model.UseDefaultCredentials = Convert.ToBoolean(dt.Rows[0]["UseDefaultCredentials"]);
            model.EnableSSL = Convert.ToBoolean(dt.Rows[0]["EnableSSL"]);
            model.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
            model.UpdateDate = Convert.ToDateTime(PublicFunctions.ConvertDBNullToNull(dt.Rows[0]["UpdateDate"]));
            model.UpdateUserID = Convert.ToInt64(PublicFunctions.ConvertDBNullToNull(dt.Rows[0]["UpdateUserID"]));
            model.AddDate = Convert.ToDateTime(PublicFunctions.ConvertDBNullToNull(dt.Rows[0]["AddDate"]));
            model.AddUserID = Convert.ToInt64(PublicFunctions.ConvertDBNullToNull(dt.Rows[0]["AddUserID"]));

            model.Priority = Convert.ToByte(PublicFunctions.ConvertDBNullToNull(dt.Rows[0]["Priority"]));

            return model;
        }

        public override async Task<List<IEmailServerModel>> SelectAllByFilterAsync(IPredicate predicate, CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
            string retVal = predicate.Serialize();

            DataTable dt;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.Predicate, GetParameter(DBObjects.SPParameter.Predicate, ParameterDirection.Input, ((int)SqlDbType.NVarChar), retVal.Length, retVal));
            param.Add(DBObjects.SPParameter.CPage, GetParameter(DBObjects.SPParameter.CPage, ParameterDirection.Input, ((int)SqlDbType.Int), 8, pageInfo.CPage));
            param.Add(DBObjects.SPParameter.PSize, GetParameter(DBObjects.SPParameter.PSize, ParameterDirection.Input, ((int)SqlDbType.Int), 8, Convert.ToInt32((pageInfo.PSize))));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, pageInfo.UserId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspEmailServerSelect.ToString(), cancellationToken, param);

            List<IEmailServerModel> list = new List<IEmailServerModel>();
            list = (from DataRow dr in dt.Rows
                    select new EmailServerModel
                    {
                        ServerId = Convert.ToInt64(dr["ServerId"]),
                        ServerName = dr["ServerName"].ToString(),
                        ServerIP = dr["ServerIP"].ToString(),
                        EmailPort = Convert.ToInt32(dr["EmailPort"]),
                        UserName = dr["UserName"].ToString(),
                        UserPswd = dr["UserPswd"].ToString(),
                        UseDefaultCredentials = Convert.ToBoolean(dr["UseDefaultCredentials"]),
                        EnableSSL = Convert.ToBoolean(dr["EnableSSL"]),
                        IsActive = Convert.ToBoolean(dr["IsActive"]),
                        UpdateDate = Convert.ToDateTime(PublicFunctions.ConvertDBNullToNull(dr["UpdateDate"])),
                        UpdateUserID = Convert.ToInt64(PublicFunctions.ConvertDBNullToNull(dr["UpdateUserID"])),
                        AddDate = Convert.ToDateTime(PublicFunctions.ConvertDBNullToNull(dr["AddDate"])),
                        AddUserID = Convert.ToInt64(PublicFunctions.ConvertDBNullToNull(dr["AddUserID"])),
                        Priority = Convert.ToByte(PublicFunctions.ConvertDBNullToNull(dr["Priority"])),
                        TRows = Convert.ToInt32(dr[DBObjects.Fields.TRows]),
                        CPage = Convert.ToInt32(dr[DBObjects.Fields.CPage]),
                        PSize = dr[DBObjects.Fields.PSize].ToString()
                    }).ToList<IEmailServerModel>();

            return list;
        }

        public async Task<DataTable> GetSystemEmailServer(long userId, CancellationToken cancellationToken)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            return await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspEmailServersGetActiveServers.ToString(), cancellationToken, param);

        }

        #endregion
    }
}
