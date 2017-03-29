using ASOL.HireThings.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Core
{
    public class MenuDAL : AbstractBaseDAL<IMenuModel>
    {

        public async override Task<long> SaveAsync(IMenuModel model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async override Task<bool> UpdateAsync(IMenuModel model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async override Task<IMenuModel> SelectByIDAsync(long id, long userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async override Task<List<IMenuModel>> SelectAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async override Task<List<IMenuModel>> SelectAllByFilterAsync(IPredicate predicate, CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
            throw new NotImplementedException();
        }

        #region
        public async Task<IMenuModel> SelectMenuAsync(long userId, CancellationToken cancellationToken)
        {
            DataTable dt = null;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspGetMenuList.ToString(), cancellationToken, param);

            IMenuModel mModel = new MenuModel();
            IMenuItemModel itemModel = null;

            mModel.ParentMenuList = dt.AsEnumerable()
                       .Select(s => new MenuItemModel()
                       {
                           ObjectId = s.Field<Int64>(DBObjects.Fields.ObjectId),
                           ObjectName = s.Field<string>(DBObjects.Fields.ObjectName),
                           ParentObjectId = s.Field<Int64>(DBObjects.Fields.ParentObjectId),
                           ObjectImage = s.Field<string>(DBObjects.Fields.ObjectImage),
                           ObjectLevel = s.Field<int>(DBObjects.Fields.ObjectLevel),
                           Caption = s.Field<string>(DBObjects.Fields.Caption),
                           URL = s.Field<string>(DBObjects.Fields.URL),
                           ObjectOrder = s.Field<byte>(DBObjects.Fields.ObjectOrder),
                       }).Where(a => a.ObjectLevel == 1).OrderBy(a => a.ObjectOrder).ToList<IMenuItemModel>();

            // Level 2 Menu
            mModel.MenuItemList = dt.AsEnumerable()
                   .Select(s => new MenuItemModel()
                   {
                       ObjectId = s.Field<Int64>(DBObjects.Fields.ObjectId),
                       ObjectName = s.Field<string>(DBObjects.Fields.ObjectName),
                       ParentObjectId = s.Field<Int64>(DBObjects.Fields.ParentObjectId),
                       ObjectImage = s.Field<string>(DBObjects.Fields.ObjectImage),
                       ObjectLevel = s.Field<int>(DBObjects.Fields.ObjectLevel),
                       Caption = s.Field<string>(DBObjects.Fields.Caption),
                       URL = s.Field<string>(DBObjects.Fields.URL),
                       ObjectOrder = s.Field<byte>(DBObjects.Fields.ObjectOrder),
                   }).Where(a => a.ObjectLevel != 1).OrderBy(a => a.ObjectOrder).ToList<IMenuItemModel>();

            return mModel;

        }


        public IMenuModel SelectMenu(long userId)
        {
            DataTable dt = null;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));

            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspGetMenuList.ToString(), ref param);

            IMenuModel mModel = new MenuModel();

            mModel.ParentMenuList = dt.AsEnumerable()
                       .Select(s => new MenuItemModel()
                       {
                           ObjectId = s.Field<Int64>(DBObjects.Fields.ObjectId),
                           ObjectName = s.Field<string>(DBObjects.Fields.ObjectName),
                           ParentObjectId = s.Field<Int64>(DBObjects.Fields.ParentObjectId),
                           ObjectImage = s.Field<string>(DBObjects.Fields.ObjectImage),
                           ObjectLevel = s.Field<int>(DBObjects.Fields.ObjectLevel),
                           Caption = s.Field<string>(DBObjects.Fields.Caption),
                           URL = s.Field<string>(DBObjects.Fields.URL),
                           ObjectOrder = s.Field<byte>(DBObjects.Fields.ObjectOrder),
                       }).Where(a => a.ObjectLevel == 1).OrderBy(a => a.ObjectOrder).ToList<IMenuItemModel>();


            // Level 2 Menu
            mModel.MenuItemList = dt.AsEnumerable()
                   .Select(s => new MenuItemModel()
                   {
                       ObjectId = s.Field<Int64>(DBObjects.Fields.ObjectId),
                       ObjectName = s.Field<string>(DBObjects.Fields.ObjectName),
                       ObjectLevel = s.Field<int>(DBObjects.Fields.ObjectLevel),
                       ObjectOrder = s.Field<byte>(DBObjects.Fields.ObjectOrder),
                       ObjectImage = s.Field<string>(DBObjects.Fields.ObjectImage),
                       URL = s.Field<string>(DBObjects.Fields.URL),
                       Caption = s.Field<string>(DBObjects.Fields.Caption),
                       ParentObjectId = s.Field<Int64>(DBObjects.Fields.ParentObjectId)
                   }).Where(a => a.ObjectLevel != 1).OrderBy(a => a.ObjectOrder).ToList<IMenuItemModel>();

            return mModel;

        }
        #endregion
    }
}
