using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Service
{
    public class SecurityQuestionSearchService : BaseService<ISecurityQuestionSearchViewModel>, ISecurityQuestionSearchService
    {
        SecurityQuestionDAL _dal = null;
        DropDownListDAL _ddlDal = null;

        public SecurityQuestionSearchService()
        {
            _dal = new SecurityQuestionDAL();
            _ddlDal = new DropDownListDAL();
        }

        #region
        public async override Task<ISecurityQuestionSearchViewModel> IndexAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            ISecurityQuestionSearchViewModel model = await PoulateDropDownListAsync(null, cancellationToken);
            SearchModel searchModel = new SearchModel()
            {
                SearchText = string.Empty
            };
            model = await SearchDataAsync(searchModel, null, cancellationToken);

            return model;
        }

        public async Task<ISecurityQuestionSearchViewModel> SearchAsync(System.Web.HttpContext httpContext, ISearchModel model, CancellationToken cancellationToken)
        {
            ISecurityQuestionSearchViewModel tdmodel = await SearchDataAsync(model, null, cancellationToken);
            return tdmodel;
        }

        

        private async Task<ISecurityQuestionSearchViewModel> SearchDataAsync(ISearchModel searchModel, ISecurityQuestionSearchViewModel model, CancellationToken cancellationToken)
        {
            IPredicate pred = null;
            IPredicate finalPred = null;
            List<IPredicate> list = new List<IPredicate>();
            
            if (!string.IsNullOrEmpty(searchModel.SearchText))
            {
                searchModel.SearchText = searchModel.SearchText.Trim();
                pred = Predicate<string>.GetPredicate("Question", searchModel.SearchText, Operator.Like);
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

            model.SecurityQuestionList = await _dal.SelectAllByFilterAsync(finalPred, cancellationToken, model.GetPageInfo(searchModel.CPage, searchModel.TPage, searchModel.TRows, searchModel.PSize, searchModel.UserId));
            return model;
        }

        private async Task<ISecurityQuestionSearchViewModel> PoulateDropDownListAsync(ISecurityQuestionSearchViewModel model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                model = new SecurityQuestionSearchViewModel();
            }

            return model;
        }

        #endregion
    }
}
