using ASOL.HireThings.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using ASOL.HireThings.Core;

namespace ASOL.HireThings.Service
{
    public class WebApiRoleSearchService : BaseService<IWebApiRoleSearchViewModel>, IWebApiRoleSearchService
    {
        WebApiRoleDAL _dal = null;
        DropDownListDAL _ddlDal = null;

        public WebApiRoleSearchService()
        {
            _dal = new WebApiRoleDAL();
            _ddlDal = new DropDownListDAL();
        }

        #region
        public async override Task<IWebApiRoleSearchViewModel> IndexAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            IWebApiRoleSearchViewModel model = await PoulateDropDownListAsync(null, cancellationToken);
            ISearchModel searchModel = new SearchModel()
            {
                SearchText = string.Empty
            };
            model = await SearchDataAsync(searchModel, model, cancellationToken);
            return model;
        }

        public async Task<IWebApiRoleSearchViewModel> SearchAsync(System.Web.HttpContext httpContext, ISearchModel model, CancellationToken cancellationToken)
        {
            IWebApiRoleSearchViewModel tdmodel = await SearchDataAsync(model, null, cancellationToken);
            return tdmodel;
        }

        private async Task<IWebApiRoleSearchViewModel> SearchDataAsync(ISearchModel searchModel, IWebApiRoleSearchViewModel model, CancellationToken cancellationToken)
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

        private async Task<IWebApiRoleSearchViewModel> PoulateDropDownListAsync(IWebApiRoleSearchViewModel model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                model = new WebApiRoleSearchViewModel();
            }

            return model;
        }
        #endregion
    }
}
