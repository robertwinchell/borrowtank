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
    public class WebAPIRoleObjectDAL :  AbstractBaseDAL<IWebAPIRoleObjectViewModel>
    {
        public async override Task<bool> UpdateAsync(IWebAPIRoleObjectViewModel model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async override Task<IWebAPIRoleObjectViewModel> SelectByIDAsync(long id, long userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async override Task<List<IWebAPIRoleObjectViewModel>> SelectAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async override Task<List<IWebAPIRoleObjectViewModel>> SelectAllByFilterAsync(IPredicate predicate, CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
            throw new NotImplementedException();
        }

        #region 
        public override async Task<long> SaveAsync(IWebAPIRoleObjectViewModel model, CancellationToken cancellationToken)
        {
            List<int> retList = new List<int>();
            foreach (var rObject in model.WebAPIRoleObjectList.Where(m => m.IsChange == true).ToList())
            {
                Dictionary<string, object> param = new Dictionary<string, object>();

                param.Add(DBObjects.SPParameter.RoleObjectId, GetParameter(DBObjects.SPParameter.RoleObjectId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, rObject.RoleObjectId));
                param.Add(DBObjects.SPParameter.RoleId, GetParameter(DBObjects.SPParameter.RoleId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.RoleId));
                param.Add(DBObjects.SPParameter.ObjectId, GetParameter(DBObjects.SPParameter.ObjectId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, rObject.ObjectID));
                param.Add(DBObjects.SPParameter.AllowGet, GetParameter(DBObjects.SPParameter.AllowGet, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, rObject.AllowGet));
                param.Add(DBObjects.SPParameter.AllowPost, GetParameter(DBObjects.SPParameter.AllowPost, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, rObject.AllowPost));
                param.Add(DBObjects.SPParameter.AllowPut, GetParameter(DBObjects.SPParameter.AllowPut, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, rObject.AllowPut));
                param.Add(DBObjects.SPParameter.AllowDelete, GetParameter(DBObjects.SPParameter.AllowDelete, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, rObject.AllowDelete));
                param.Add(DBObjects.SPParameter.IsActive, GetParameter(DBObjects.SPParameter.IsActive, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, rObject.IsActive));
                param.Add(DBObjects.SPParameter.AddUserId, GetParameter(DBObjects.SPParameter.AddUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
                param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

                await this.ExecuteSPAsync(DBObjects.StoredProcedures.pspWebAPI_RoleObjectSave.ToString(), cancellationToken, param);

                retList.Add((int)((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value);
            }

            if (retList.Contains(0))
                return 0;
            else
                return 1;

        }

        public async Task<List<WebAPIRoleObjectModel>> SelectAllObjectAsync(long roleId, CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
            DataTable dt;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.RoleId, GetParameter(DBObjects.SPParameter.RoleId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, roleId));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, pageInfo.UserId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspWebAPI_RoleObjectSelect.ToString(), cancellationToken, param);



            List<WebAPIRoleObjectModel> list = new List<WebAPIRoleObjectModel>();
            list = (from DataRow dr in dt.Rows
                    select new WebAPIRoleObjectModel
                    {
                        RoleId = Convert.ToInt64(dr[DBObjects.Fields.RoleId]),
                        RoleObjectId = Convert.ToInt64(dr[DBObjects.Fields.RoleObjectId]),
                        ObjectID = Convert.ToInt64(dr[DBObjects.Fields.ObjectId]),
                        ObjectName = dr[DBObjects.Fields.Name].ToString(),
                        AllowGet = Convert.ToBoolean(dr[DBObjects.Fields.AllowGet]),
                        AllowPost = Convert.ToBoolean(dr[DBObjects.Fields.AllowPost]),
                        AllowPut = Convert.ToBoolean(dr[DBObjects.Fields.AllowPut]),
                        AllowDelete = Convert.ToBoolean(dr[DBObjects.Fields.AllowDelete]),
                        IsActive = Convert.ToBoolean(dr[DBObjects.Fields.IsActive]),
                        ShowGet = Convert.ToBoolean(dr[DBObjects.Fields.ShowGet]),
                        ShowPost = Convert.ToBoolean(dr[DBObjects.Fields.ShowPost]),
                        ShowPut = Convert.ToBoolean(dr[DBObjects.Fields.ShowPut]),
                        ShowDelete = Convert.ToBoolean(dr[DBObjects.Fields.ShowDelete]),
                    }).ToList<WebAPIRoleObjectModel>();

            return list;
        }

        #endregion
    }
}
