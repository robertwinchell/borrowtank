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
    public class ThemeDAL : AbstractBaseDAL<IThemeModel>
    {
        #region
        public override async Task<long> SaveAsync(IThemeModel model, CancellationToken cancellationToken)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add(DBObjects.SPParameter.Name, GetParameter(DBObjects.SPParameter.Name, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, PublicFunctions.ConvertNulltoDBNull(model.Name)));
            param.Add(DBObjects.SPParameter.Description, GetParameter(DBObjects.SPParameter.Description, ParameterDirection.Input, ((int)SqlDbType.VarChar), 500, PublicFunctions.ConvertNulltoDBNull(model.Description)));
            param.Add(DBObjects.SPParameter.Code, GetParameter(DBObjects.SPParameter.Code, ParameterDirection.Input, ((int)SqlDbType.VarChar), 5, PublicFunctions.ConvertNulltoDBNull(model.Code)));
            param.Add(DBObjects.SPParameter.IsActive, GetParameter(DBObjects.SPParameter.IsActive, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.AddUserId, GetParameter(DBObjects.SPParameter.AddUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.ThemeId, GetParameter(DBObjects.SPParameter.ThemeId, ParameterDirection.Output, ((int)SqlDbType.BigInt), 8, 0));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

            await this.ExecuteSPAsync(DBObjects.StoredProcedures.pspThemeSave.ToString(), cancellationToken,  param);

            return (long)(((SqlParameter)param[DBObjects.SPParameter.ThemeId]).Value);
        }

        public override async Task<bool> UpdateAsync(IThemeModel model, CancellationToken cancellationToken)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add(DBObjects.SPParameter.ThemeId, GetParameter(DBObjects.SPParameter.ThemeId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.ThemeId));
            param.Add(DBObjects.SPParameter.Name, GetParameter(DBObjects.SPParameter.Name, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, model.Name));
            param.Add(DBObjects.SPParameter.Description, GetParameter(DBObjects.SPParameter.Description, ParameterDirection.Input, ((int)SqlDbType.VarChar), 500, PublicFunctions.ConvertNulltoDBNull(model.Description)));
            param.Add(DBObjects.SPParameter.Code, GetParameter(DBObjects.SPParameter.Code, ParameterDirection.Input, ((int)SqlDbType.VarChar), 5, model.Code));
            param.Add(DBObjects.SPParameter.IsActive, GetParameter(DBObjects.SPParameter.IsActive, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.UpdateUserId, GetParameter(DBObjects.SPParameter.UpdateUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));
           await this.ExecuteSPAsync(DBObjects.StoredProcedures.pspThemeUpdate.ToString(), cancellationToken, param);

            return Convert.ToBoolean(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value);
        }

        public override async Task<List<IThemeModel>> SelectAllAsync(CancellationToken cancellationToken)
        {
            DataTable dt = null;
            Dictionary<string, object> param = new Dictionary<string, object>();

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspThemeSelect.ToString(), cancellationToken, param);

            List<IThemeModel> list = new List<IThemeModel>();
            list = (from DataRow dr in dt.Rows
                    select new ThemeModel
                    {
                        ThemeId = Convert.ToInt64(dr["ThemeId"]),
                        Name = dr["Name"].ToString(),
                        Description = dr["Description"].ToString(),
                        Code = dr["Code"].ToString(),
                        IsActive = Convert.ToBoolean(dr["IsActive"])
                    }).ToList<IThemeModel>();

            return list;

        }
        public override async Task<IThemeModel> SelectByIDAsync(long id, long userId, CancellationToken cancellationToken)
        {
            DataTable dt = null;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.ThemeId, GetParameter(DBObjects.SPParameter.ThemeId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, id));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspThemeById.ToString(), cancellationToken,  param);

            IThemeModel model = new ThemeModel();
            model.ThemeId = Convert.ToInt64(dt.Rows[0]["ThemeId"]);
            model.Name = dt.Rows[0]["Name"].ToString();
            model.Description = dt.Rows[0]["Description"].ToString();
            model.Code = dt.Rows[0]["Code"].ToString();
            model.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);

            return model;
        }

        public override async Task<List<IThemeModel>> SelectAllByFilterAsync(IPredicate predicate, CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
            string retVal = predicate.Serialize();

            DataTable dt;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.Predicate, GetParameter(DBObjects.SPParameter.Predicate, ParameterDirection.Input, ((int)SqlDbType.NVarChar), retVal.Length, retVal));
            param.Add(DBObjects.SPParameter.CPage, GetParameter(DBObjects.SPParameter.CPage, ParameterDirection.Input, ((int)SqlDbType.Int), 8, pageInfo.CPage));
            param.Add(DBObjects.SPParameter.PSize, GetParameter(DBObjects.SPParameter.PSize, ParameterDirection.Input, ((int)SqlDbType.Int), 8, Convert.ToInt32((pageInfo.PSize))));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, pageInfo.UserId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspThemeSearch.ToString(), cancellationToken, param);

            List<IThemeModel> list = new List<IThemeModel>();
            list = (from DataRow dr in dt.Rows
                    select new ThemeModel
                    {
                        ThemeId = Convert.ToInt64(dr["ThemeId"]),
                        Name = dr["Name"].ToString(),
                        Description = dr["Description"].ToString(),
                        Code = dr["Code"].ToString(),
                        IsActive = Convert.ToBoolean(dr["IsActive"]),
                        TRows = Convert.ToInt32(dr[DBObjects.Fields.TRows]),
                        CPage = Convert.ToInt32(dr[DBObjects.Fields.CPage]),
                        PSize = dr[DBObjects.Fields.PSize].ToString()
                    }).ToList<IThemeModel>();

            return list;
        }
        #endregion
    }
}
