using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public class ThemeSearchService : BaseService<IThemeSearchViewModel>, IThemeSearchService
    {
        ThemeDAL _dal;
        DropDownListDAL _ddlDal;
        long userId = System.Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public ThemeSearchService()
        {
            _dal = new ThemeDAL();
            _ddlDal = new DropDownListDAL();
        }
        
        #region
        public async override Task<IThemeSearchViewModel> IndexAsync(HttpContext context, CancellationToken cancellationToken)
        {
            IThemeSearchViewModel model = await PoulateDropDownListAsync(null, cancellationToken);
            ISearchModel searchModel = new SearchModel()
            {

                SearchField = System.Convert.ToInt32(model.DDLSearch[0].Value),
                SearchText = string.Empty
            };

            model = await SearchDataAsync(searchModel, model, cancellationToken);

            return model;
        }
        public async Task<IThemeSearchViewModel> SearchAsync(HttpContext context, ISearchModel searchModel, CancellationToken cancellationToken)
        {
            IThemeSearchViewModel tdModel = await SearchDataAsync(searchModel, null, cancellationToken);
            return tdModel;
        }
        public async Task<IThemeSearchViewModel> SearchDataAsync(ISearchModel searchModel, IThemeSearchViewModel model, CancellationToken cancellationToken)
        {
            IPredicate pred = null;
            IPredicate finalPred = null;
            List<IPredicate> list = new List<IPredicate>();
            
            if (!string.IsNullOrEmpty(searchModel.SearchText))
            {
                searchModel.SearchText = searchModel.SearchText.Trim();
                if (searchModel.SearchField == -1)
                {
                    pred = CompoundPredicate.GetCompoundPredicate(
                            Predicate<string>.GetPredicate("Name", searchModel.SearchText, Operator.Like),
                            Predicate<string>.GetPredicate("Code", searchModel.SearchText, Operator.Like), LogicalOperator.OR);

                    list.Add(pred);
                }
                else if (searchModel.SearchField == 1)
                {
                    pred = Predicate<string>.GetPredicate("Name", searchModel.SearchText, Operator.Like);
                    list.Add(pred);
                }
                else if (searchModel.SearchField == 2)
                {
                    pred = Predicate<string>.GetPredicate("Code", searchModel.SearchText, Operator.Like);
                    list.Add(pred);
                }
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

            model.ThemeList = await _dal.SelectAllByFilterAsync(finalPred, cancellationToken, model.GetPageInfo(searchModel.CPage, searchModel.TPage, searchModel.TRows, searchModel.PSize, searchModel.UserId));
            return model;
        }

        private async Task<IThemeSearchViewModel> PoulateDropDownListAsync(IThemeSearchViewModel model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                model = new ThemeSearchViewModel();
            }


            model.DDLSearch = await _ddlDal.GetThemeSearchListAsync(userId, cancellationToken);



            return model;
        }

        #endregion
    }
}
