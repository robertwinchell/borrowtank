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
    public class CategoryDAL : AbstractBaseDAL<ICategoryModel>
    {
        #region
        public override async Task<long> SaveAsync(ICategoryModel model, CancellationToken cancellationToken)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add(DBObjects.SPParameter.ThemeId, GetParameter(DBObjects.SPParameter.ThemeId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.ThemeId)));
            param.Add(DBObjects.SPParameter.CategoryName, GetParameter(DBObjects.SPParameter.CategoryName, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, model.CategoryName));
            param.Add(DBObjects.SPParameter.CategorySerialNo, GetParameter(DBObjects.SPParameter.CategorySerialNo, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, PublicFunctions.ConvertNulltoDBNull(model.CategorySerialNo)));
            param.Add(DBObjects.SPParameter.CategoryDesc, GetParameter(DBObjects.SPParameter.CategoryDesc, ParameterDirection.Input, ((int)SqlDbType.VarChar), 500, PublicFunctions.ConvertNulltoDBNull(model.CategoryDesc)));
            param.Add(DBObjects.SPParameter.AddUserId, GetParameter(DBObjects.SPParameter.AddUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.ShowOnHomepage, GetParameter(DBObjects.SPParameter.ShowOnHomepage, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.ShowOnHomepage));

            param.Add(DBObjects.SPParameter.IsActive, GetParameter(DBObjects.SPParameter.IsActive, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.CategoryId, GetParameter(DBObjects.SPParameter.CategoryId, ParameterDirection.Output, ((int)SqlDbType.BigInt), 8, 0));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

            await this.ExecuteSPAsync(DBObjects.StoredProcedures.pspCategorySave.ToString(), cancellationToken,param);

            return (long)(((SqlParameter)param[DBObjects.SPParameter.CategoryId]).Value);
        }

        public override async Task<bool> UpdateAsync(ICategoryModel model, CancellationToken cancellationToken)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add(DBObjects.SPParameter.CategoryId, GetParameter(DBObjects.SPParameter.CategoryId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.CategoryId));
            param.Add(DBObjects.SPParameter.ThemeId, GetParameter(DBObjects.SPParameter.ThemeId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.ThemeId)));
            param.Add(DBObjects.SPParameter.CategoryName, GetParameter(DBObjects.SPParameter.CategoryName, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, model.CategoryName));
            param.Add(DBObjects.SPParameter.CategorySerialNo, GetParameter(DBObjects.SPParameter.CategorySerialNo, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, PublicFunctions.ConvertNulltoDBNull(model.CategorySerialNo)));
            param.Add(DBObjects.SPParameter.CategoryDesc, GetParameter(DBObjects.SPParameter.CategoryDesc, ParameterDirection.Input, ((int)SqlDbType.VarChar), 500, PublicFunctions.ConvertNulltoDBNull(model.CategoryDesc)));
            param.Add(DBObjects.SPParameter.UpdateUserId, GetParameter(DBObjects.SPParameter.UpdateUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.ShowOnHomepage, GetParameter(DBObjects.SPParameter.ShowOnHomepage, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.ShowOnHomepage));

            param.Add(DBObjects.SPParameter.IsActive, GetParameter(DBObjects.SPParameter.IsActive, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

            await this.ExecuteSPAsync(DBObjects.StoredProcedures.pspCategoryUpdate.ToString(), cancellationToken, param);
            return Convert.ToBoolean(Convert.ToInt32(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value));


        }

        public override async Task<List<ICategoryModel>> SelectAllAsync(CancellationToken cancellationToken)
        {
            DataTable dt = null;
            Dictionary<string, object> param = new Dictionary<string, object>();

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspCategorySelect.ToString(), cancellationToken, param);

            List<ICategoryModel> list = new List<ICategoryModel>();
            list = (from DataRow dr in dt.Rows
                    select new CategoryModel
                    {
                        CategoryId = Convert.ToInt64(dr["CategoryId"]),
                        CategorySerialNo = dr["CategorySerailNo"].ToString(),
                        Theme = dr["ThemeName"].ToString(),
                        CategoryName = dr["CategoryName"].ToString(),
                        IsActive = Convert.ToBoolean(dr["IsActive"]),

                    }).ToList<ICategoryModel>();

            return list;
        }

        public override async Task<ICategoryModel> SelectByIDAsync(long id, long userId, CancellationToken cancellationToken)
        {
            DataTable dt = null;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.CategoryId, GetParameter(DBObjects.SPParameter.CategoryId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, id));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspCategoryById.ToString(), cancellationToken, param);

            ICategoryModel model = new CategoryModel();

            model.CategoryId = Convert.ToInt64(dt.Rows[0]["CategoryId"]);
            model.CategorySerialNo = Convert.ToString(dt.Rows[0]["CategorySerialNo"]);
            model.ThemeId = Convert.ToInt64(dt.Rows[0]["ThemeId"]);
            model.CategoryName = Convert.ToString(dt.Rows[0]["CategoryName"]);

            model.ShowOnHomepage = Convert.ToBoolean(dt.Rows[0]["ShowOnHomepage"]);
            model.CategoryDesc = Convert.ToString(dt.Rows[0]["CategoryDesc"]);

            model.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
            return model;
        }

        public override async Task<List<ICategoryModel>> SelectAllByFilterAsync(IPredicate predicate, CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
            string retVal = predicate.Serialize();

            DataTable dt;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.Predicate, GetParameter(DBObjects.SPParameter.Predicate, ParameterDirection.Input, ((int)SqlDbType.NVarChar), retVal.Length, retVal));
            param.Add(DBObjects.SPParameter.CPage, GetParameter(DBObjects.SPParameter.CPage, ParameterDirection.Input, ((int)SqlDbType.Int), 8, pageInfo.CPage));
            param.Add(DBObjects.SPParameter.PSize, GetParameter(DBObjects.SPParameter.PSize, ParameterDirection.Input, ((int)SqlDbType.Int), 8, Convert.ToInt32((pageInfo.PSize))));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, pageInfo.UserId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspCategorySearch.ToString(), cancellationToken, param);

            List<ICategoryModel> list = new List<ICategoryModel>();
            list = (from DataRow dr in dt.Rows
                    select new CategoryModel
                    {
                        CategoryId = Convert.ToInt64(dr["CategoryId"]),
                        CategoryName = dr["CategoryName"].ToString(),
                        CategorySerialNo = dr["CategorySerialNo"].ToString(),
                        Theme = dr["ThemeName"].ToString(),
                         IsActive = Convert.ToBoolean(dr["IsActive"]),
                         PSize = dr[DBObjects.Fields.PSize].ToString(),
                        TRows = Convert.ToInt32(dr[DBObjects.Fields.TRows]),
                        CPage = Convert.ToInt32(dr[DBObjects.Fields.CPage])
                    }).ToList<ICategoryModel>();

            return list;
        }
        #endregion
    }
}
