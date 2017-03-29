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
    public class HirePlanaDAL : AbstractBaseDAL<IHirePlanaModel>
    {
        #region Async 
        public override async Task<long> SaveAsync(IHirePlanaModel model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();


            //Dictionary<string, object> param = new Dictionary<string, object>();
            //param.Add(DBObjects.SPParameter.CountryId, GetParameter(DBObjects.SPParameter.CountryId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.CountryId));
            //param.Add(DBObjects.SPParameter.LocationName, GetParameter(DBObjects.SPParameter.LocationName, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, model.LocationName));
            //param.Add(DBObjects.SPParameter.ZipCode, GetParameter(DBObjects.SPParameter.ZipCode, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, model.ZipCode));
            //param.Add(DBObjects.SPParameter.IsActive, GetParameter(DBObjects.SPParameter.IsActive, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            //param.Add(DBObjects.SPParameter.AddUserId, GetParameter(DBObjects.SPParameter.AddUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            //param.Add(DBObjects.SPParameter.LocationID, GetParameter(DBObjects.SPParameter.LocationID, ParameterDirection.Output, ((int)SqlDbType.BigInt), 8, 0));
            //param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

            //await this.ExecuteSPAsync(DBObjects.StoredProcedures.hspLocationSave.ToString(), cancellationToken, param);
            //return (long)(((SqlParameter)param[DBObjects.SPParameter.LocationID]).Value);
        }

        public override async Task<bool> UpdateAsync(IHirePlanaModel model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();



            //Dictionary<string, object> param = new Dictionary<string, object>();
            //param.Add(DBObjects.SPParameter.LocationID, GetParameter(DBObjects.SPParameter.LocationID, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.LocationId));
            //param.Add(DBObjects.SPParameter.CountryId, GetParameter(DBObjects.SPParameter.CountryId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.CountryId));
            //param.Add(DBObjects.SPParameter.LocationName, GetParameter(DBObjects.SPParameter.LocationName, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, model.LocationName));
            //param.Add(DBObjects.SPParameter.ZipCode, GetParameter(DBObjects.SPParameter.ZipCode, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, model.ZipCode));
            //param.Add(DBObjects.SPParameter.IsActive, GetParameter(DBObjects.SPParameter.IsActive, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            //param.Add(DBObjects.SPParameter.UpdateUserId, GetParameter(DBObjects.SPParameter.UpdateUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            //param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));
            //await this.ExecuteSPAsync(DBObjects.StoredProcedures.hspLocationUpdate.ToString(), cancellationToken, param);
            //return Convert.ToBoolean(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value);
        }

        public override async Task<IHirePlanaModel> SelectByIDAsync(long id, long userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

            //DataTable dt = null;
            //Dictionary<string, object> param = new Dictionary<string, object>();
            //param.Add(DBObjects.SPParameter.LocationID, GetParameter(DBObjects.SPParameter.LocationID, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, id));
            //param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            //dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.hspSelectLocationByID.ToString(), cancellationToken, param);

            //IHirePlanaModel model = new HirePlanaModel();
            //model.LocationId = Convert.ToInt64(dt.Rows[0]["LocationId"]);
            //model.CountryId = Convert.ToInt64(dt.Rows[0]["CountryId"]);
            //model.LocationName = dt.Rows[0]["LocationName"].ToString();
            //model.ZipCode = dt.Rows[0]["ZipCode"].ToString();
            //model.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
            //return model;
        }

        public override async Task<List<IHirePlanaModel>> SelectAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

            //DataTable dt = null;
            //Dictionary<string, object> param = new Dictionary<string, object>();
            //dt = this.GetSPDataTable(DBObjects.StoredProcedures.hspLocationSelect.ToString(), ref param);

            //List<IHirePlanaModel> list = new List<IHirePlanaModel>();
            //list = (from DataRow dr in dt.Rows
            //        select new HirePlanaModel
            //        {
            //            LocationId = Convert.ToInt64(dr["LocationId"]),
            //            LocationName = dr["LocationName"].ToString(),
            //            CountryId = Convert.ToInt64(dr["CountryId"]),
            //            IsActive = Convert.ToBoolean(dr["IsActive"]),

            //        }).ToList<IHirePlanaModel>();

            //return list;

        }

        public override async Task<List<IHirePlanaModel>> SelectAllByFilterAsync(IPredicate predicate, CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
            string retVal = predicate.Serialize();

            DataTable dt;
            throw new NotImplementedException();
            //Dictionary<string, object> param = new Dictionary<string, object>();
            //param.Add(DBObjects.SPParameter.Predicate, GetParameter(DBObjects.SPParameter.Predicate, ParameterDirection.Input, ((int)SqlDbType.NVarChar), retVal.Length, retVal));
            //param.Add(DBObjects.SPParameter.CPage, GetParameter(DBObjects.SPParameter.CPage, ParameterDirection.Input, ((int)SqlDbType.Int), 8, pageInfo.CPage));
            //param.Add(DBObjects.SPParameter.PSize, GetParameter(DBObjects.SPParameter.PSize, ParameterDirection.Input, ((int)SqlDbType.Int), 8, Convert.ToInt32((pageInfo.PSize))));
            //param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, pageInfo.UserId));
            //dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.hspLocationSearch.ToString(), cancellationToken, param);

            //List<ILocationModel> list = new List<ILocationModel>();
            //list = (from DataRow dr in dt.Rows
            //        select new LocationModel
            //        {
            //            LocationId = Convert.ToInt64(dr["LocationId"]),
            //            LocationName = dr["LocationName"].ToString(),
            //            CountryName = dr["CountryName"].ToString(),
            //            IsActive = Convert.ToBoolean(dr["IsActive"]),
            //            TRows = Convert.ToInt32(dr[DBObjects.Fields.TRows]),
            //            CPage = Convert.ToInt32(dr[DBObjects.Fields.CPage]),
            //            PSize = dr[DBObjects.Fields.PSize].ToString()
            //        }).ToList<ILocationModel>();
            //if (list.Count > 0)
            //{
            //    var sdsd = list[0].TRows;

            //    LocationModel dsd = (LocationModel)list[0];
            //}
            //return list;
        }
        #endregion
    }
}
