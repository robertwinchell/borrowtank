using ASOL.HireThings.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Core
{
    public class GlobalDAL : AbstractBaseDAL<IBaseModel>
    {
        public DataTable spGetUserSecurityPORTAL(long userId, string ModType)
        {
            DataTable dt = new DataTable();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, 10011));
            param.Add(DBObjects.SPParameter.ModType, GetParameter(DBObjects.SPParameter.ModType, ParameterDirection.Input, ((int)SqlDbType.Char), 1, "W"));

            dt = this.GetSPDataTable(DBObjects.StoredProcedures.spGetUserSecurityPORTAL.ToString(), ref param);

            return dt;
        }

        public async override Task<Int64> SaveAsync(IBaseModel model, CancellationToken cancellationToken)
        {
            return 10011;
        }
        public async override Task<bool> UpdateAsync(IBaseModel model, CancellationToken cancellationToken)
        {
            return true;
        }
        public async override Task<IBaseModel> SelectByIDAsync(Int64 id, long userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async override Task<List<IBaseModel>> SelectAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async override Task<List<IBaseModel>> SelectAllByFilterAsync(IPredicate filterList, CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Custom rights implementation on Objects 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="objName"></param>
        /// <returns>AuthModel which defines Edit/Delete rights</returns>
        public AuthModel AuthorizeObject(long userId, string objName, string action)
        {
            DataTable dt;
            AuthModel model = null;
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            param.Add(DBObjects.SPParameter.ObjectName, GetParameter(DBObjects.SPParameter.ObjectName, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, objName));

            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspAuthorization.ToString(), ref param);

            if (dt.Rows.Count > 0)
            {
                model = new AuthModel();
                model.AllowWrite = PublicFunctions.ConvertNULL(dt.Rows[0]["AllowWrite"], false);
                model.AllowDelete = PublicFunctions.ConvertNULL(dt.Rows[0]["AllowDelete"], false);
                model.ObjectName = Convert.ToString(dt.Rows[0]["ObjectName"]);
                model.URL = Convert.ToString(dt.Rows[0]["URL"]);
                model.IsWithoutSearch = PublicFunctions.ConvertNULL(dt.Rows[0]["IsWithoutSearch"], false);
                model.IsPublicUser = Convert.ToBoolean(dt.Rows[0]["IsPublicUser"]);


            }

            return model;
        }

    }
}
