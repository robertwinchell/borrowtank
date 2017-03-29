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
    public class RoleObjectDAL :  AbstractBaseDAL<IRoleObjectViewModel>
    {
        public async override Task<bool> UpdateAsync(IRoleObjectViewModel model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async override Task<IRoleObjectViewModel> SelectByIDAsync(long id, long userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async override Task<List<IRoleObjectViewModel>> SelectAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async override Task<List<IRoleObjectViewModel>> SelectAllByFilterAsync(IPredicate predicate, CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
            throw new NotImplementedException();
        }

        #region 
        public override async Task<long> SaveAsync(IRoleObjectViewModel model, CancellationToken cancellationToken)
        {
            List<int> retList = new List<int>();
            foreach (var rObject in model.RoleObjectList)
            {
                Dictionary<string, object> param = new Dictionary<string, object>();

                param.Add(DBObjects.SPParameter.RoleObjectId, GetParameter(DBObjects.SPParameter.RoleObjectId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, rObject.RoleObjectId));
                param.Add(DBObjects.SPParameter.ObjectId, GetParameter(DBObjects.SPParameter.ObjectId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, rObject.ObjectID));
                param.Add(DBObjects.SPParameter.RoleId, GetParameter(DBObjects.SPParameter.RoleId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.RoleId));
                param.Add(DBObjects.SPParameter.AllowWrite, GetParameter(DBObjects.SPParameter.AllowWrite, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, rObject.AllowWrite));
                param.Add(DBObjects.SPParameter.AllowDelete, GetParameter(DBObjects.SPParameter.AllowDelete, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, rObject.AllowDelete));
                param.Add(DBObjects.SPParameter.IsActive, GetParameter(DBObjects.SPParameter.IsActive, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, rObject.IsActive));
                param.Add(DBObjects.SPParameter.AddUserId, GetParameter(DBObjects.SPParameter.AddUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
                param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

                await this.ExecuteSPAsync(DBObjects.StoredProcedures.pspRoleObjectSave.ToString(), cancellationToken, param);

                retList.Add((int)((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value);
            }

            if (retList.Contains(0))
                return 0;
            else
                return 1;

        }

        public async Task<List<RoleObjectModel>> SelectAllObjectAsync(long roleId, CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
            DataTable dt;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.RoleId, GetParameter(DBObjects.SPParameter.RoleId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, roleId));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, pageInfo.UserId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspRoleObjectSelect.ToString(), cancellationToken, param);



            List<RoleObjectModel> list = new List<RoleObjectModel>();
            list = (from DataRow dr in dt.Rows
                    select new RoleObjectModel
                    {
                        RoleId = Convert.ToInt64(dr[DBObjects.Fields.RoleId]),
                        RoleObjectId = Convert.ToInt64(dr[DBObjects.Fields.RoleObjectId]),
                        ObjectID = Convert.ToInt64(dr[DBObjects.Fields.ObjectId]),
                        ObjectName = dr[DBObjects.Fields.Name].ToString(),
                        AllowWrite = Convert.ToBoolean(dr[DBObjects.Fields.AllowWrite]),
                        AllowDelete = Convert.ToBoolean(dr[DBObjects.Fields.AllowDelete]),
                        IsActive = Convert.ToBoolean(dr[DBObjects.Fields.IsActive]),
                        ShowActive = Convert.ToBoolean(dr[DBObjects.Fields.ShowActive]),
                        ShowWrite = Convert.ToBoolean(dr[DBObjects.Fields.ShowWrite]),
                        ShowDelete = Convert.ToBoolean(dr[DBObjects.Fields.ShowDelete]),
                        ParentObjectId = Convert.ToInt64(dr[DBObjects.Fields.ParentObjectId]),
                        ObjectOrder = Convert.ToByte(dr[DBObjects.Fields.ObjectOrder]),
                        ObjectLevel = Convert.ToInt32(dr[DBObjects.Fields.ObjectLevel]),
                    }).ToList<RoleObjectModel>();

            return list;
        }

        #endregion
    }
}
