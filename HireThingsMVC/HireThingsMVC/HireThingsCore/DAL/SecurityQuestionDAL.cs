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
    public class SecurityQuestionDAL : AbstractBaseDAL<ISecurityQuestionModel>
    {
        #region 
        public override async Task<long> SaveAsync(ISecurityQuestionModel model, CancellationToken cancellationToken)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add(DBObjects.SPParameter.Question, GetParameter(DBObjects.SPParameter.Question, ParameterDirection.Input, ((int)SqlDbType.NVarChar), 1024, model.Question));
            param.Add(DBObjects.SPParameter.IsActive, GetParameter(DBObjects.SPParameter.IsActive, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.AddUserId, GetParameter(DBObjects.SPParameter.AddUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.SecurityQuestionId, GetParameter(DBObjects.SPParameter.SecurityQuestionId, ParameterDirection.Output, ((int)SqlDbType.BigInt), 8, 0));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

           await this.ExecuteSPAsync(DBObjects.StoredProcedures.pspSecurityQuestionSave.ToString(), cancellationToken, param);

            return (long)(((SqlParameter)param[DBObjects.SPParameter.SecurityQuestionId]).Value);
        }

        public override async Task<bool> UpdateAsync(ISecurityQuestionModel model, CancellationToken cancellationToken)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add(DBObjects.SPParameter.SecurityQuestionId, GetParameter(DBObjects.SPParameter.SecurityQuestionId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.SecurityQuestionId));
            param.Add(DBObjects.SPParameter.Question, GetParameter(DBObjects.SPParameter.Question, ParameterDirection.Input, ((int)SqlDbType.NVarChar), 1024, model.Question));
            param.Add(DBObjects.SPParameter.IsActive, GetParameter(DBObjects.SPParameter.IsActive, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.UpdateUserId, GetParameter(DBObjects.SPParameter.UpdateUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

            await this.ExecuteSPAsync(DBObjects.StoredProcedures.pspSecurityQuestionUpdate.ToString(), cancellationToken, param);

            int ret = (int)(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value);
            return Convert.ToBoolean(ret);
        }

        public override async Task<ISecurityQuestionModel> SelectByIDAsync(long id, long userId, CancellationToken cancellationToken)
        {
            DataTable dt;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.SecurityQuestionId, GetParameter(DBObjects.SPParameter.SecurityQuestionId, ParameterDirection.Input, ((int)SqlDbType.NVarChar), 8, id));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspSecurityQuestionSelectById.ToString(), cancellationToken, param);

            ISecurityQuestionModel model = new SecurityQuestionModel();
            model.SecurityQuestionId = Convert.ToInt64(dt.Rows[0][DBObjects.Fields.SecurityQuestionId]);
            model.Question = dt.Rows[0][DBObjects.Fields.Question].ToString();
            model.IsActive = Convert.ToBoolean(dt.Rows[0][DBObjects.Fields.IsActive]);
            return model;

        }

        public override async Task<List<ISecurityQuestionModel>> SelectAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<ISecurityQuestionModel>> SelectAllByFilterAsync(IPredicate predicate, CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
            string retVal = predicate.Serialize();

            DataTable dt;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.Predicate, GetParameter(DBObjects.SPParameter.Predicate, ParameterDirection.Input, ((int)SqlDbType.NVarChar), retVal.Length, retVal));
            param.Add(DBObjects.SPParameter.CPage, GetParameter(DBObjects.SPParameter.CPage, ParameterDirection.Input, ((int)SqlDbType.Int), 8, pageInfo.CPage));
            param.Add(DBObjects.SPParameter.PSize, GetParameter(DBObjects.SPParameter.PSize, ParameterDirection.Input, ((int)SqlDbType.Int), 8, Convert.ToInt32((pageInfo.PSize))));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, pageInfo.UserId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspSecurityQuestionSelect.ToString(), cancellationToken, param);

            List<ISecurityQuestionModel> list = new List<ISecurityQuestionModel>();
            list = (from DataRow dr in dt.Rows
                    select new SecurityQuestionModel
                    {
                        SecurityQuestionId = Convert.ToInt64(dr[DBObjects.Fields.SecurityQuestionId]),
                        Question = dr[DBObjects.Fields.Question].ToString(),
                        IsActive = Convert.ToBoolean(dr[DBObjects.Fields.IsActive]),
                        TRows = Convert.ToInt32(dr[DBObjects.Fields.TRows]),
                        CPage = Convert.ToInt32(dr[DBObjects.Fields.CPage]),
                        PSize = dr[DBObjects.Fields.PSize].ToString()
                    }).ToList<ISecurityQuestionModel>();

            return list;
        }
        #endregion

    }
}
