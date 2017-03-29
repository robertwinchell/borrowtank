using ASOL.HireThings.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using System.Data.SqlClient;

namespace ASOL.HireThings.Core
{
    public class WebApiObjectDAL : AbstractBaseDAL<IWebApiObjectModel>
    {
        public override async Task<long> SaveAsync(IWebApiObjectModel model, CancellationToken cancellationToken)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add(DBObjects.SPParameter.ModuleId, GetParameter(DBObjects.SPParameter.ModuleId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.ModuleId));
            param.Add(DBObjects.SPParameter.Name, GetParameter(DBObjects.SPParameter.Name, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, model.Name));
            param.Add(DBObjects.SPParameter.Description, GetParameter(DBObjects.SPParameter.Description, ParameterDirection.Input, ((int)SqlDbType.VarChar), 500, PublicFunctions.ConvertNulltoDBNull(model.Description)));
            param.Add(DBObjects.SPParameter.URL, GetParameter(DBObjects.SPParameter.URL, ParameterDirection.Input, ((int)SqlDbType.VarChar), 150, PublicFunctions.ConvertNulltoDBNull(model.URL)));
            param.Add(DBObjects.SPParameter.AllowGet, GetParameter(DBObjects.SPParameter.AllowGet, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.AllowGet));
            param.Add(DBObjects.SPParameter.AllowPost, GetParameter(DBObjects.SPParameter.AllowPost, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.AllowPost));
            param.Add(DBObjects.SPParameter.AllowPut, GetParameter(DBObjects.SPParameter.AllowPut, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.AllowPut));
            param.Add(DBObjects.SPParameter.AllowDelete, GetParameter(DBObjects.SPParameter.AllowDelete, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.AllowDelete));
            param.Add(DBObjects.SPParameter.IsActive, GetParameter(DBObjects.SPParameter.IsActive, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.AddUserId, GetParameter(DBObjects.SPParameter.AddUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.WAObjectId, GetParameter(DBObjects.SPParameter.WAObjectId, ParameterDirection.Output, ((int)SqlDbType.BigInt), 8, 0));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

            await this.ExecuteSPAsync(DBObjects.StoredProcedures.webApi_pspObjectSave.ToString(), cancellationToken, param);

            return (long)(((SqlParameter)param[DBObjects.SPParameter.WAObjectId]).Value);
        }

        public override Task<List<IWebApiObjectModel>> SelectAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<IWebApiObjectModel>> SelectAllByFilterAsync(IPredicate predicate, CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
            string retVal = predicate.Serialize();

            DataTable dt;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.Predicate, GetParameter(DBObjects.SPParameter.Predicate, ParameterDirection.Input, ((int)SqlDbType.NVarChar), retVal.Length, retVal));
            param.Add(DBObjects.SPParameter.CPage, GetParameter(DBObjects.SPParameter.CPage, ParameterDirection.Input, ((int)SqlDbType.Int), 8, pageInfo.CPage));
            param.Add(DBObjects.SPParameter.PSize, GetParameter(DBObjects.SPParameter.PSize, ParameterDirection.Input, ((int)SqlDbType.Int), 8, Convert.ToInt32((pageInfo.PSize))));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, pageInfo.UserId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.webApi_pspObjectSelect.ToString(), cancellationToken, param);

            List<IWebApiObjectModel> list = new List<IWebApiObjectModel>();
            list = (from DataRow dr in dt.Rows
                    select new WebApiObjectModel
                    {
                        WAObjectId = Convert.ToInt64(dr[DBObjects.Fields.WAObjectId]),
                        Name = dr[DBObjects.Fields.Name].ToString(),
                        ModuleName = dr[DBObjects.Fields.ModuleName].ToString(),
                        Description = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dr[DBObjects.Fields.Description])),
                        URL = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dr[DBObjects.Fields.URL])),
                        AllowGet=Convert.ToBoolean(dr[DBObjects.Fields.AllowGet]),
                        AllowPost = Convert.ToBoolean(dr[DBObjects.Fields.AllowPost]),
                        AllowDelete = Convert.ToBoolean(dr[DBObjects.Fields.AllowDelete]),
                        AllowPut = Convert.ToBoolean(dr[DBObjects.Fields.AllowPut]),
                        IsActive = Convert.ToBoolean(dr[DBObjects.Fields.IsActive]),
                        TRows = Convert.ToInt32(dr[DBObjects.Fields.TRows]),
                        CPage = Convert.ToInt32(dr[DBObjects.Fields.CPage]),
                        PSize = dr[DBObjects.Fields.PSize].ToString()
                    }).ToList<IWebApiObjectModel>();

            return list;
        }

        public override async Task<IWebApiObjectModel> SelectByIDAsync(long id, long userId, CancellationToken cancellationToken)
        {
            DataTable dt;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.WAObjectId, GetParameter(DBObjects.SPParameter.WAObjectId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, id));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.webApi_pspObjectSelectById.ToString(), cancellationToken, param);

            IWebApiObjectModel model = new WebApiObjectModel();
            model.WAObjectId = Convert.ToInt64(dt.Rows[0][DBObjects.Fields.WAObjectId]);
            model.ModuleId = Convert.ToInt64(dt.Rows[0][DBObjects.Fields.ModuleId]);
            model.Name = dt.Rows[0][DBObjects.Fields.Name].ToString();
            model.Description = PublicFunctions.ConvertNULL(dt.Rows[0][DBObjects.Fields.Description], "");
            model.URL = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dt.Rows[0][DBObjects.Fields.URL]));
            model.AllowGet = Convert.ToBoolean(dt.Rows[0][DBObjects.Fields.AllowGet]);
            model.AllowPost = Convert.ToBoolean(dt.Rows[0][DBObjects.Fields.AllowPost]);
            model.AllowPut = Convert.ToBoolean(dt.Rows[0][DBObjects.Fields.AllowPut]);
            model.AllowDelete = Convert.ToBoolean(dt.Rows[0][DBObjects.Fields.AllowDelete]);
            model.IsActive = Convert.ToBoolean(dt.Rows[0][DBObjects.Fields.IsActive]);
            return model;
        }

        public override async Task<bool> UpdateAsync(IWebApiObjectModel model, CancellationToken cancellationToken)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add(DBObjects.SPParameter.WAObjectId, GetParameter(DBObjects.SPParameter.WAObjectId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.WAObjectId));
            param.Add(DBObjects.SPParameter.ModuleId, GetParameter(DBObjects.SPParameter.ModuleId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.ModuleId));
            param.Add(DBObjects.SPParameter.Name, GetParameter(DBObjects.SPParameter.Name, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, model.Name));
            param.Add(DBObjects.SPParameter.Description, GetParameter(DBObjects.SPParameter.Description, ParameterDirection.Input, ((int)SqlDbType.VarChar), 500, PublicFunctions.ConvertNulltoDBNull(model.Description)));
            param.Add(DBObjects.SPParameter.URL, GetParameter(DBObjects.SPParameter.URL, ParameterDirection.Input, ((int)SqlDbType.VarChar), 1024, PublicFunctions.ConvertNulltoDBNull(model.URL)));
            param.Add(DBObjects.SPParameter.AllowGet, GetParameter(DBObjects.SPParameter.AllowGet, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.AllowGet));
            param.Add(DBObjects.SPParameter.AllowPost, GetParameter(DBObjects.SPParameter.AllowPost, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.AllowPost));
            param.Add(DBObjects.SPParameter.AllowPut, GetParameter(DBObjects.SPParameter.AllowPut, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.AllowPut));
            param.Add(DBObjects.SPParameter.AllowDelete, GetParameter(DBObjects.SPParameter.AllowDelete, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.AllowDelete));
            param.Add(DBObjects.SPParameter.IsActive, GetParameter(DBObjects.SPParameter.IsActive, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.UpdateUserId, GetParameter(DBObjects.SPParameter.UpdateUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

            await this.ExecuteSPAsync(DBObjects.StoredProcedures.webApi_pspObjectUpdate.ToString(), cancellationToken, param);

            int ret = (int)(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value);
            return Convert.ToBoolean(ret);
        }
    }
}
