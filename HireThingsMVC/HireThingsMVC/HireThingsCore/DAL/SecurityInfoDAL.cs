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
    public class SecurityInfoDAL : AbstractBaseDAL<ISecurityInfoViewModel>
    {
       #region
        public override async Task<long> SaveAsync(ISecurityInfoViewModel model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> UpdateAsync(ISecurityInfoViewModel model, CancellationToken cancellationToken)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.SecurityQuestionId, GetParameter(DBObjects.SPParameter.SecurityQuestionId, ParameterDirection.Input, ((int)SqlDbType.VarChar), 20, model.SecurityQuestionId));
            param.Add(DBObjects.SPParameter.Answer, GetParameter(DBObjects.SPParameter.Answer, ParameterDirection.Input, ((int)SqlDbType.VarChar), 20, model.Answer));
            param.Add(DBObjects.SPParameter.UpdateUserId, GetParameter(DBObjects.SPParameter.UpdateUserId, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, model.UserId));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));
            //param.Add(DBObjects.SPParameter.PIN, GetParameter(DBObjects.SPParameter.PIN, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, model.PIN));

            await this.ExecuteSPAsync(DBObjects.StoredProcedures.pspChangeSecurityInfo.ToString(), cancellationToken, param);

            // For RetVal = 0,2 return false http://redmine.vteamslabs.com/issues/37885 
            if ((Int32)(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value) == 1)
                return true;
            return false;
        }

        public override async Task<List<ISecurityInfoViewModel>> SelectAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

        }

        public override async Task<ISecurityInfoViewModel> SelectByIDAsync(long id, long userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

        }

        public override async Task<List<ISecurityInfoViewModel>> SelectAllByFilterAsync(IPredicate predicate, CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
