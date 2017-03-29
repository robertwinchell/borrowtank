using ASOL.HireThings.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Core
{
    public class ObjectDAL : AbstractBaseDAL<IObjectModel>
    {
        #region
        public override async Task<long> SaveAsync(IObjectModel model, CancellationToken cancellationToken)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add(DBObjects.SPParameter.ModuleId, GetParameter(DBObjects.SPParameter.ModuleId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.ModuleId));
            param.Add(DBObjects.SPParameter.Name, GetParameter(DBObjects.SPParameter.Name, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, model.Name));
            param.Add(DBObjects.SPParameter.Description, GetParameter(DBObjects.SPParameter.Description, ParameterDirection.Input, ((int)SqlDbType.VarChar), 500, PublicFunctions.ConvertNulltoDBNull(model.Description)));
            param.Add(DBObjects.SPParameter.Caption, GetParameter(DBObjects.SPParameter.Caption, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, model.Caption));
            param.Add(DBObjects.SPParameter.URL, GetParameter(DBObjects.SPParameter.URL, ParameterDirection.Input, ((int)SqlDbType.VarChar), 150, PublicFunctions.ConvertNulltoDBNull(model.URL)));
            param.Add(DBObjects.SPParameter.ParentObjectId, GetParameter(DBObjects.SPParameter.ParentObjectId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.ParentObjectId));
            param.Add(DBObjects.SPParameter.AllowWrite, GetParameter(DBObjects.SPParameter.AllowWrite, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.AllowWrite));
            param.Add(DBObjects.SPParameter.AllowDelete, GetParameter(DBObjects.SPParameter.AllowDelete, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.AllowDelete));
            param.Add(DBObjects.SPParameter.ObjectLevel, GetParameter(DBObjects.SPParameter.ObjectLevel, ParameterDirection.Input, ((int)SqlDbType.Int), 4, model.ObjectLevel));
            param.Add(DBObjects.SPParameter.IsActive, GetParameter(DBObjects.SPParameter.IsActive, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.AddUserId, GetParameter(DBObjects.SPParameter.AddUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.ObjectOrder, GetParameter(DBObjects.SPParameter.ObjectOrder, ParameterDirection.Input, ((int)SqlDbType.TinyInt), 3, PublicFunctions.ConvertNulltoDBNull(model.ObjectOrder)));
            param.Add(DBObjects.SPParameter.ObjectImage, GetParameter(DBObjects.SPParameter.ObjectImage, ParameterDirection.Input, ((int)SqlDbType.VarChar), 150, PublicFunctions.ConvertNulltoDBNull(model.ObjectImage)));
            param.Add(DBObjects.SPParameter.ObjectId, GetParameter(DBObjects.SPParameter.ObjectId, ParameterDirection.Output, ((int)SqlDbType.BigInt), 8, 0));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

            await this.ExecuteSPAsync(DBObjects.StoredProcedures.pspObjectSave.ToString(), cancellationToken, param);

            return (long)(((SqlParameter)param[DBObjects.SPParameter.ObjectId]).Value);
        }

        public override async Task<bool> UpdateAsync(IObjectModel model, CancellationToken cancellationToken)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add(DBObjects.SPParameter.ObjectId, GetParameter(DBObjects.SPParameter.ObjectId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.ObjectId));
            param.Add(DBObjects.SPParameter.ModuleId, GetParameter(DBObjects.SPParameter.ModuleId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.ModuleId));
            param.Add(DBObjects.SPParameter.Name, GetParameter(DBObjects.SPParameter.Name, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, model.Name));
            param.Add(DBObjects.SPParameter.Description, GetParameter(DBObjects.SPParameter.Description, ParameterDirection.Input, ((int)SqlDbType.VarChar), 500, PublicFunctions.ConvertNulltoDBNull(model.Description)));
            param.Add(DBObjects.SPParameter.Caption, GetParameter(DBObjects.SPParameter.Caption, ParameterDirection.Input, ((int)SqlDbType.VarChar), 500, model.Caption));
            param.Add(DBObjects.SPParameter.URL, GetParameter(DBObjects.SPParameter.URL, ParameterDirection.Input, ((int)SqlDbType.VarChar), 1024, PublicFunctions.ConvertNulltoDBNull(model.URL)));
            param.Add(DBObjects.SPParameter.ParentObjectId, GetParameter(DBObjects.SPParameter.ParentObjectId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.ParentObjectId));
            param.Add(DBObjects.SPParameter.AllowWrite, GetParameter(DBObjects.SPParameter.AllowWrite, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.AllowWrite));
            param.Add(DBObjects.SPParameter.AllowDelete, GetParameter(DBObjects.SPParameter.AllowDelete, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.AllowDelete));
            param.Add(DBObjects.SPParameter.ObjectLevel, GetParameter(DBObjects.SPParameter.ObjectLevel, ParameterDirection.Input, ((int)SqlDbType.Int), 4, model.ObjectLevel));
            param.Add(DBObjects.SPParameter.IsActive, GetParameter(DBObjects.SPParameter.IsActive, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.UpdateUserId, GetParameter(DBObjects.SPParameter.UpdateUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.ObjectOrder, GetParameter(DBObjects.SPParameter.ObjectOrder, ParameterDirection.Input, ((int)SqlDbType.TinyInt), 3, PublicFunctions.ConvertNulltoDBNull(model.ObjectOrder)));
            param.Add(DBObjects.SPParameter.ObjectImage, GetParameter(DBObjects.SPParameter.ObjectImage, ParameterDirection.Input, ((int)SqlDbType.VarChar), 150, PublicFunctions.ConvertNulltoDBNull(model.ObjectImage)));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

            await this.ExecuteSPAsync(DBObjects.StoredProcedures.pspObjectUpdate.ToString(), cancellationToken, param);

            int ret = (int)(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value);
            return Convert.ToBoolean(ret);
        }

        public override async Task<IObjectModel> SelectByIDAsync(long id, long userId, CancellationToken cancellationToken)
        {
            DataTable dt;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.ObjectId, GetParameter(DBObjects.SPParameter.ObjectId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, id));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspObjectSelectById.ToString(), cancellationToken, param);

            IObjectModel model = new ObjectModel();
            model.ObjectId = Convert.ToInt64(dt.Rows[0][DBObjects.Fields.ObjectId]);
            model.ModuleId = Convert.ToInt64(dt.Rows[0][DBObjects.Fields.ModuleId]);
            model.Name = dt.Rows[0][DBObjects.Fields.Name].ToString();
            model.Description = PublicFunctions.ConvertNULL(dt.Rows[0][DBObjects.Fields.Description], "");
            model.Caption = dt.Rows[0][DBObjects.Fields.Caption].ToString();
            model.URL = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dt.Rows[0][DBObjects.Fields.URL]));
            model.ParentObjectId = Convert.ToInt64(dt.Rows[0][DBObjects.Fields.ParentObjectId]);
            model.ObjectLevel = Convert.ToInt32(dt.Rows[0][DBObjects.Fields.ObjectLevel]);
            model.AllowWrite = Convert.ToBoolean(dt.Rows[0][DBObjects.Fields.AllowWrite]);
            model.AllowDelete = Convert.ToBoolean(dt.Rows[0][DBObjects.Fields.AllowDelete]);
            model.IsActive = Convert.ToBoolean(dt.Rows[0][DBObjects.Fields.IsActive]);
            model.ObjectOrder = Convert.ToByte(PublicFunctions.ConvertDBNullToNull(dt.Rows[0][DBObjects.Fields.ObjectOrder]));
            model.ObjectImage = PublicFunctions.ConvertNULL(dt.Rows[0][DBObjects.Fields.ObjectImage], "");
            return model;
        }

        public override async Task<List<IObjectModel>> SelectAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<IObjectModel>> SelectAllByFilterAsync(IPredicate predicate, CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
            string retVal = predicate.Serialize();

            DataTable dt;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.Predicate, GetParameter(DBObjects.SPParameter.Predicate, ParameterDirection.Input, ((int)SqlDbType.NVarChar), retVal.Length, retVal));
            param.Add(DBObjects.SPParameter.CPage, GetParameter(DBObjects.SPParameter.CPage, ParameterDirection.Input, ((int)SqlDbType.Int), 8, pageInfo.CPage));
            param.Add(DBObjects.SPParameter.PSize, GetParameter(DBObjects.SPParameter.PSize, ParameterDirection.Input, ((int)SqlDbType.Int), 8, Convert.ToInt32((pageInfo.PSize))));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, pageInfo.UserId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspObjectSelect.ToString(), cancellationToken, param);

            List<IObjectModel> list = new List<IObjectModel>();
            list = (from DataRow dr in dt.Rows
                    select new ObjectModel
                    {
                        ObjectId = Convert.ToInt64(dr[DBObjects.Fields.ObjectId]),
                        ParentObjectId = Convert.ToInt64(dr[DBObjects.Fields.ParentObjectId]),
                        Name = dr[DBObjects.Fields.Name].ToString(),
                        ModuleName = dr[DBObjects.Fields.ModuleName].ToString(),
                        ParentName = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dr[DBObjects.Fields.ParentName])),
                        Description = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dr[DBObjects.Fields.Description])),
                        Caption = dr[DBObjects.Fields.Caption].ToString(),
                        URL = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dr[DBObjects.Fields.URL])),
                        ObjectOrder = Convert.ToByte(dr[DBObjects.Fields.ObjectOrder]),
                        AllowDelete = Convert.ToBoolean(dr[DBObjects.Fields.AllowDelete]),
                        AllowWrite = Convert.ToBoolean(dr[DBObjects.Fields.AllowWrite]),
                        IsActive = Convert.ToBoolean(dr[DBObjects.Fields.IsActive]),
                        TRows = Convert.ToInt32(dr[DBObjects.Fields.TRows]),
                        CPage = Convert.ToInt32(dr[DBObjects.Fields.CPage]),
                        PSize = dr[DBObjects.Fields.PSize].ToString(),
                        IsWithoutSearch = Convert.ToBoolean(dr[DBObjects.Fields.IsWithoutSearch]),
                    }).ToList<IObjectModel>();

            return list;
        }
        #endregion
    }
}
