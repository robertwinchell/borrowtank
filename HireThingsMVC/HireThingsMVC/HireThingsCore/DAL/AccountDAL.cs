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
    public class AccountDAL : AbstractBaseDAL<IAccountViewModel>
    {
        public async override Task<long> SaveAsync(IAccountViewModel model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async override  Task<bool> UpdateAsync(IAccountViewModel model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async override Task<IAccountViewModel> SelectByIDAsync(long id, long userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async override Task<List<IAccountViewModel>> SelectAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async override Task<List<IAccountViewModel>> SelectAllByFilterAsync(IPredicate predicate, CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
            throw new NotImplementedException();
        }

        public int IsUserValidPoulateQuestion(string userEmail, ref string question, ref Int64 userId)
        {
           // int retVal = 0;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.EMAIL, GetParameter(DBObjects.SPParameter.EMAIL, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, userEmail));
            param.Add(DBObjects.SPParameter.RetQuestion, GetParameter(DBObjects.SPParameter.RetQuestion, ParameterDirection.Output, ((int)SqlDbType.VarChar), 1024, null));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Output, ((int)SqlDbType.BigInt), 8, null));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, null));

            this.ExecuteSP(DBObjects.StoredProcedures.pspSecurityEmailVerify.ToString(), ref param);
            return (int)(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value);

        }

        public void LogUserActvity(IUserLogActvityModel model)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add(DBObjects.SPParameter.SessionId, GetParameter(DBObjects.SPParameter.SessionId, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, model.SessionId));
            param.Add(DBObjects.SPParameter.IPAddress, GetParameter(DBObjects.SPParameter.IPAddress, ParameterDirection.Input, ((int)SqlDbType.VarChar), 20, model.IPAddress));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.LoginType, GetParameter(DBObjects.SPParameter.LoginType, ParameterDirection.Input, ((int)SqlDbType.TinyInt), 1, model.LoginType));
            param.Add(DBObjects.SPParameter.LogoutType, GetParameter(DBObjects.SPParameter.LogoutType, ParameterDirection.Input, ((int)SqlDbType.TinyInt), 1, model.LogoutType));
            param.Add(DBObjects.SPParameter.ModuleId, GetParameter(DBObjects.SPParameter.ModuleId, ParameterDirection.Input, ((int)SqlDbType.VarChar), 5, model.ModuleId));
            param.Add(DBObjects.SPParameter.ActivityForm, GetParameter(DBObjects.SPParameter.ActivityForm, ParameterDirection.Input, ((int)SqlDbType.VarChar), 200, model.ActivityForm));
            this.ExecuteSP(DBObjects.StoredProcedures.spActivityLogInsertUpdate.ToString(), ref param);
        }
    }
}
