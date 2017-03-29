using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASOL.HireThings.Model;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace ASOL.HireThings.Core
{
    public class ItemDeleteDAL : AbstractBaseDAL<IItemDeleteModel>
    {
    

        public async override Task<bool> UpdateAsync(IItemDeleteModel model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async override Task<IItemDeleteModel> SelectByIDAsync(Int64 id, long userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async override Task<List<IItemDeleteModel>> SelectAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async override Task<List<IItemDeleteModel>> SelectAllByFilterAsync(IPredicate filterList, CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
            throw new NotImplementedException();
        }

        #region
        public override async Task<Int64> SaveAsync(IItemDeleteModel model, CancellationToken cancellationToken)
        {
            //       return 1;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.ObjectName, GetParameter(DBObjects.SPParameter.ObjectName, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, PublicFunctions.ConvertNulltoDBNull(model.ObjectName)));
            param.Add(DBObjects.SPParameter.Reason, GetParameter(DBObjects.SPParameter.Reason, ParameterDirection.Input, ((int)SqlDbType.VarChar), 500, PublicFunctions.ConvertNulltoDBNull(model.Reason)));
            param.Add(DBObjects.SPParameter.RecordId, GetParameter(DBObjects.SPParameter.RecordId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.RecordId));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

            await this.ExecuteSPAsync(DBObjects.StoredProcedures.pspDeleteData.ToString(), cancellationToken, param);

            return (Int32)(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value);

        }

        #endregion
    }
}
