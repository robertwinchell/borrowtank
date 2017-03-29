using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public class CategorySearchService : BaseService<ICategorySearchViewModel>, ICategorySearchService
    {
        CategoryDAL _dal;
        DropDownListDAL _ddlDal;
        long userId = System.Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public CategorySearchService()
        {
            _dal = new CategoryDAL();
            _ddlDal = new DropDownListDAL();
        }

        #region
        public async override Task<ICategorySearchViewModel> IndexAsync(HttpContext context, CancellationToken cancellationToken)
        {
            ICategorySearchViewModel model = await PoulateDropDownListAsync(null, cancellationToken);

            ISearchModel searchModel = new SearchModel()
            {
                SearchField = System.Convert.ToInt32(model.DDLSearch[0].Value),
                SearchText = string.Empty,
                UserId = userId
            };

            model = await SearchDataAsync(searchModel, model, cancellationToken);
            return model;
        }

        public async Task<ICategorySearchViewModel> SearchAsync(HttpContext context, ISearchModel searchModel, CancellationToken cancellationToken)
        {
            ICategorySearchViewModel tdModel = await SearchDataAsync(searchModel, null, cancellationToken);
            return tdModel;
        }

        public async Task<ICategorySearchViewModel> SearchDataAsync(ISearchModel searchModel, ICategorySearchViewModel model, CancellationToken cancellationToken)
        {
            IPredicate pred = null;
            IPredicate finalPred = null;
            List<IPredicate> list = new List<IPredicate>();

            if (!string.IsNullOrEmpty(searchModel.SearchText))
            {
                if (searchModel.SearchField == -1)
                {
                    pred = CompoundPredicate.GetCompoundPredicate(
                            LogicalOperator.OR,
                            Predicate<string>.GetPredicate("C.CategoryName", searchModel.SearchText, Operator.Like),
                            Predicate<string>.GetPredicate("T.Name", searchModel.SearchText, Operator.Like)
                            );

                   


                    list.Add(pred);
                }
                else if (searchModel.SearchField == 1)
                {
                    pred = Predicate<string>.GetPredicate("C.CategoryName", searchModel.SearchText, Operator.Like);
                    list.Add(pred);
                }
                
                else if (searchModel.SearchField == 2)
                {
                    pred = Predicate<string>.GetPredicate("T.Name", searchModel.SearchText, Operator.Like);
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

            model.CategoryList = await _dal.SelectAllByFilterAsync(finalPred, cancellationToken, model.GetPageInfo(searchModel.CPage, searchModel.TPage, searchModel.TRows, searchModel.PSize, searchModel.UserId));

            return model;
        }

        private async Task<ICategorySearchViewModel> PoulateDropDownListAsync(ICategorySearchViewModel model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                model = new CategorySearchViewModel();
            }

            model.DDLSearch = await _ddlDal.GetCategorySearchListAsync(userId, cancellationToken);

            return model;
        }

        #endregion
    }
}
