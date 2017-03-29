using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Service
{
    public class RoleSearchService : BaseService<IRoleSearchViewModel>, IRoleSearchService
    {
        RoleDAL _dal = null;
        DropDownListDAL _ddlDal = null;

        public RoleSearchService()
        {
            _dal = new RoleDAL();
            _ddlDal = new DropDownListDAL();
        }

        #region
        public async override Task<IRoleSearchViewModel> IndexAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            IRoleSearchViewModel model = await PoulateDropDownListAsync(null, cancellationToken);
            ISearchModel searchModel = new SearchModel()
            {
                SearchText = string.Empty
            };
            model = await SearchDataAsync(searchModel, model, cancellationToken);
            return model;
        }

        public async Task<IRoleSearchViewModel> SearchAsync(System.Web.HttpContext httpContext, ISearchModel model, CancellationToken cancellationToken)
        {
            IRoleSearchViewModel tdmodel = await SearchDataAsync(model, null, cancellationToken);
            return tdmodel;
        }

        private async Task<IRoleSearchViewModel> SearchDataAsync(ISearchModel searchModel, IRoleSearchViewModel model, CancellationToken cancellationToken)
        {
            IPredicate pred = null;
            IPredicate finalPred = null;
            List<IPredicate> list = new List<IPredicate>();
            
            if (!string.IsNullOrEmpty(searchModel.SearchText))
            {
                searchModel.SearchText = searchModel.SearchText.Trim();
                pred = Predicate<string>.GetPredicate("Name", searchModel.SearchText, Operator.Like);
                list.Add(pred);
            }

            if (list.Count == 0)
            {
                list.Add(Predicate<string>.GetPredicate("1", "1", Operator.Equal));
            }

            finalPred = CompoundPredicate.GetCompoundPredicate(LogicalOperator.AND, list.ToArray());

            if (model == null)
            {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
                model = await PoulateDropDownListAsync(model, cancellationToken);
            }

            model.RoleList = await _dal.SelectAllByFilterAsync(finalPred, cancellationToken, model.GetPageInfo(searchModel.CPage, searchModel.TPage, searchModel.TRows, searchModel.PSize, searchModel.UserId));
            return model;
        }

        private async Task<IRoleSearchViewModel> PoulateDropDownListAsync(IRoleSearchViewModel model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                model = new RoleSearchViewModel();
            }

            return model;
        }
        #endregion
    }
}
