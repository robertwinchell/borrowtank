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
    public class CountryDAL : AbstractBaseDAL<ICountryModel>
    {   
        #region async implementation
        public override async Task<long> SaveAsync(ICountryModel model, CancellationToken cancellationToken)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

           
            param.Add(DBObjects.SPParameter.CountryName, GetParameter(DBObjects.SPParameter.CountryName, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, PublicFunctions.ConvertNulltoDBNull(model.CountryName)));
            param.Add(DBObjects.SPParameter.IsActive, GetParameter(DBObjects.SPParameter.IsActive, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.AddUserId, GetParameter(DBObjects.SPParameter.AddUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.CountryId, GetParameter(DBObjects.SPParameter.CountryId, ParameterDirection.Output, ((int)SqlDbType.BigInt), 8, 0));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

            await this.ExecuteSPAsync(DBObjects.StoredProcedures.hspCountrySave.ToString(), cancellationToken, param);

            return (long)(((SqlParameter)param[DBObjects.SPParameter.CountryId]).Value);
        }

        public override async Task<bool> UpdateAsync(ICountryModel model, CancellationToken cancellationToken)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add(DBObjects.SPParameter.CountryId, GetParameter(DBObjects.SPParameter.CountryId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.CountryId));
            param.Add(DBObjects.SPParameter.CountryName, GetParameter(DBObjects.SPParameter.CountryName, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, model.CountryName));
            param.Add(DBObjects.SPParameter.IsActive, GetParameter(DBObjects.SPParameter.IsActive, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.UpdateUserId, GetParameter(DBObjects.SPParameter.UpdateUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

            await this.ExecuteSPAsync(DBObjects.StoredProcedures.hspCountryUpdate.ToString(), cancellationToken, param);

            return Convert.ToBoolean(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value);

        }

        public override async Task<ICountryModel> SelectByIDAsync(Int64 id, Int64 userId, CancellationToken cancellationToken)
        {
            DataTable dt = null;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.CountryId, GetParameter(DBObjects.SPParameter.CountryId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, id));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.hspCountry.ToString(), cancellationToken, param);

            ICountryModel model = new CountryModel();
            model.CountryId = Convert.ToInt64(dt.Rows[0]["CountryId"]);
            model.CountryName = dt.Rows[0]["CountryName"].ToString();
             model.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);

            return model;
        }

        public override async Task<List<ICountryModel>> SelectAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<ICountryModel>> SelectAllByFilterAsync(IPredicate predicate, CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
            string retVal = predicate.Serialize();

            DataTable dt;
            Dictionary<string, object> param = new Dictionary<string, object>();
            pageInfo.ColumnName = (pageInfo.ColumnName == String.Empty) ?  "1" :  pageInfo.ColumnName;
            string sortOrder = (pageInfo.SortType == 0) ? " asc " : (pageInfo.SortType == 1) ? " desc" : "";

            param.Add(DBObjects.SPParameter.Predicate, GetParameter(DBObjects.SPParameter.Predicate, ParameterDirection.Input, ((int)SqlDbType.NVarChar), retVal.Length, retVal));
            param.Add(DBObjects.SPParameter.CPage, GetParameter(DBObjects.SPParameter.CPage, ParameterDirection.Input, ((int)SqlDbType.Int), 8, pageInfo.CPage));
            param.Add(DBObjects.SPParameter.PSize, GetParameter(DBObjects.SPParameter.PSize, ParameterDirection.Input, ((int)SqlDbType.Int), 8, Convert.ToInt32((pageInfo.PSize))));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, pageInfo.UserId));
            param.Add(DBObjects.SPParameter.ColumnName, GetParameter(DBObjects.SPParameter.ColumnName, ParameterDirection.Input, ((int)SqlDbType.NVarChar),pageInfo.ColumnName.Length+5, pageInfo.ColumnName+sortOrder));



            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.hspCountrySearch.ToString(), cancellationToken, param);

            List<ICountryModel> list = new List<ICountryModel>();
            list = (from DataRow dr in dt.Rows
                    select new CountryModel
                    {
                        CountryId = Convert.ToInt64(dr["CountryId"]), 
                        CountryName = dr["CountryName"].ToString(),
                        IsActive = Convert.ToBoolean(dr["IsActive"]),
                        TRows = Convert.ToInt32(dr[DBObjects.Fields.TRows]),
                        CPage = Convert.ToInt32(dr[DBObjects.Fields.CPage]),
                        PSize = dr[DBObjects.Fields.PSize].ToString(),
                        ColumnName = pageInfo.ColumnName.ToString(),
                        SortType = pageInfo.SortType
                    }).ToList<ICountryModel>();

            return list;
        }

        #endregion

        }
}
