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
    public class RoleDAL : AbstractBaseDAL<IRoleModel>
    {
        #region
        public override async Task<long> SaveAsync(IRoleModel model, CancellationToken cancellationToken)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add(DBObjects.SPParameter.Name, GetParameter(DBObjects.SPParameter.Name, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, model.Name));
            param.Add(DBObjects.SPParameter.Description, GetParameter(DBObjects.SPParameter.Description, ParameterDirection.Input, ((int)SqlDbType.VarChar), 500, PublicFunctions.ConvertNulltoDBNull(model.Description)));
            param.Add(DBObjects.SPParameter.IsActive, GetParameter(DBObjects.SPParameter.IsActive, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.AddUserId, GetParameter(DBObjects.SPParameter.AddUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.Rank, GetParameter(DBObjects.SPParameter.Rank, ParameterDirection.Input, ((int)SqlDbType.Int), 4, model.Rank));
            param.Add(DBObjects.SPParameter.RoleId, GetParameter(DBObjects.SPParameter.RoleId, ParameterDirection.Output, ((int)SqlDbType.BigInt), 8, 0));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

            await this.ExecuteSPAsync(DBObjects.StoredProcedures.pspRoleSave.ToString(), cancellationToken, param);

            return (long)(((SqlParameter)param[DBObjects.SPParameter.RoleId]).Value);
        }

        public override async Task<bool> UpdateAsync(IRoleModel model, CancellationToken cancellationToken)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add(DBObjects.SPParameter.RoleId, GetParameter(DBObjects.SPParameter.RoleId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.RoleId));
            param.Add(DBObjects.SPParameter.Name, GetParameter(DBObjects.SPParameter.Name, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, model.Name));
            param.Add(DBObjects.SPParameter.Description, GetParameter(DBObjects.SPParameter.Description, ParameterDirection.Input, ((int)SqlDbType.VarChar), 500, PublicFunctions.ConvertNulltoDBNull(model.Description)));
            param.Add(DBObjects.SPParameter.IsActive, GetParameter(DBObjects.SPParameter.IsActive, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.UpdateUserId, GetParameter(DBObjects.SPParameter.UpdateUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.Rank, GetParameter(DBObjects.SPParameter.Rank, ParameterDirection.Input, ((int)SqlDbType.Int), 4, model.Rank));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

           await this.ExecuteSPAsync(DBObjects.StoredProcedures.pspRoleUpdate.ToString(), cancellationToken,  param);

            int ret = (int)(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value);
            return Convert.ToBoolean(ret);
        }

        public override async Task<IRoleModel> SelectByIDAsync(long id, long userId, CancellationToken cancellationToken)
        {
            DataTable dt;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.RoleId, GetParameter(DBObjects.SPParameter.RoleId, ParameterDirection.Input, ((int)SqlDbType.NVarChar), 8, id));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspRoleSelectById.ToString(), cancellationToken,  param);

            IRoleModel model = new RoleModel();
            model.RoleId = Convert.ToInt64(dt.Rows[0][DBObjects.Fields.RoleId]);
            model.Name = dt.Rows[0][DBObjects.Fields.Name].ToString();
            model.Description = PublicFunctions.ConvertDBNullToNull(dt.Rows[0][DBObjects.Fields.Description]).ToString();
            model.Rank = Convert.ToInt32(dt.Rows[0][DBObjects.Fields.Rank]);
            model.IsActive = Convert.ToBoolean(dt.Rows[0][DBObjects.Fields.IsActive]);

            return model;

        }

        public override async Task<List<IRoleModel>> SelectAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<IRoleModel>> SelectAllByFilterAsync(IPredicate predicate, CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
            string retVal = predicate.Serialize();

            DataTable dt;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.Predicate, GetParameter(DBObjects.SPParameter.Predicate, ParameterDirection.Input, ((int)SqlDbType.NVarChar), retVal.Length, retVal));
            param.Add(DBObjects.SPParameter.CPage, GetParameter(DBObjects.SPParameter.CPage, ParameterDirection.Input, ((int)SqlDbType.Int), 8, pageInfo.CPage));
            param.Add(DBObjects.SPParameter.PSize, GetParameter(DBObjects.SPParameter.PSize, ParameterDirection.Input, ((int)SqlDbType.Int), 8, Convert.ToInt32((pageInfo.PSize))));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, pageInfo.UserId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspRoleSelect.ToString(), cancellationToken, param);

            List<IRoleModel> list = new List<IRoleModel>();
            list = (from DataRow dr in dt.Rows
                    select new RoleModel
                    {
                        RoleId = Convert.ToInt64(dr[DBObjects.Fields.RoleId]),
                        Name = dr[DBObjects.Fields.Name].ToString(),
                        Description = PublicFunctions.ConvertDBNullToNull(dr[DBObjects.Fields.Description]).ToString(),
                        IsActive = Convert.ToBoolean(dr[DBObjects.Fields.IsActive]),
                        Rank = Convert.ToInt32(dr[DBObjects.Fields.Rank]),
                        IsSystemGenerated = Convert.ToBoolean(PublicFunctions.ConvertDBNullToNull(dr[DBObjects.Fields.IsSystemGenerated])),
                        TRows = Convert.ToInt32(dr[DBObjects.Fields.TRows]),
                        CPage = Convert.ToInt32(dr[DBObjects.Fields.CPage]),
                        PSize = dr[DBObjects.Fields.PSize].ToString()
                    }).ToList<IRoleModel>();

            return list;
        }
        #endregion
    }
}
