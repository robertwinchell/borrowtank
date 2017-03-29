using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public class AdvertisementSearchService : BaseService<IAdvertisementSearchViewModel>, IAdvertisementSearchService
    {
        AdvertisementDAL _dal;
        DropDownListDAL _ddlDal;
        long userId = System.Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public AdvertisementSearchService()
        {
            _dal = new AdvertisementDAL();
            _ddlDal = new DropDownListDAL();
        }

        #region
        public async override Task<IAdvertisementSearchViewModel> IndexAsync(HttpContext context, CancellationToken cancellationToken)
        {
            IAdvertisementSearchViewModel model = await PoulateDropDownListAsync(null, cancellationToken);

            SearchModel searchModel = new SearchModel()
            {
                //SearchField = System.Convert.ToInt32(model.DDLSearch[0].Value),
                SearchText = string.Empty,
                SearchField = 0
            };

            model = await SearchDataAsync(searchModel, model, cancellationToken);

            return model;
        }

        public async Task<IAdvertisementSearchViewModel> IndexSearchAsync(string searchText, string themeid, string categoryid, HttpContext context, CancellationToken cancellationToken)
        {
            IAdvertisementSearchViewModel model = await PoulateDropDownListAsync(null, cancellationToken);

            SearchModel searchModel;
            if (System.Convert.ToInt64(themeid) > 0)
                searchModel = new SearchModel()
                {
                    SearchText = themeid,
                    SearchField = -2
                };
            else if (System.Convert.ToInt64(categoryid) > 0)
                searchModel = new SearchModel()
                {
                    SearchText = categoryid,
                    SearchField = -3
                };
            else
            searchModel = new SearchModel()
            {
                SearchText = searchText,
                SearchField = -1
            };
            model = await SearchDataAsync(searchModel, model, cancellationToken);

            return model;
        }

        public async Task<IAdvertisementSearchViewModel> SearchAsync(System.Web.HttpContext httpContext, ISearchModel model, CancellationToken cancellationToken)
        {
            IAdvertisementSearchViewModel tdModel = await SearchDataAsync(model, null, cancellationToken);
            return tdModel;
        }

        private async Task<IAdvertisementSearchViewModel> SearchDataAsync(ISearchModel searchModel, IAdvertisementSearchViewModel model, CancellationToken cancellationToken)
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
                          LogicalOperator.OR,
                          Predicate<string>.GetPredicate("A.Name", searchModel.SearchText, Operator.Like),
                          Predicate<string>.GetPredicate("A.Description", searchModel.SearchText, Operator.Like));



                    list.Add(pred);
                }
                else if (searchModel.SearchField == -2)
                {

                    pred = Predicate<long>.GetPredicate("T.ThemeId", System.Convert.ToInt64(searchModel.SearchText), Operator.Equal);


                    list.Add(pred);
                }
                else if (searchModel.SearchField == -3)
                {
                    pred = Predicate<long>.GetPredicate("C.CategoryId", System.Convert.ToInt64(searchModel.SearchText), Operator.Equal);

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

            model.AdvertisementList = await _dal.SelectAllByFilterAsync(finalPred, cancellationToken, model.GetPageInfo(searchModel.CPage, searchModel.TPage, searchModel.TRows, searchModel.PSize, searchModel.UserId));
            return model;
        }

        private async Task<IAdvertisementSearchViewModel> PoulateDropDownListAsync(IAdvertisementSearchViewModel model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                model = new AdvertisementSearchViewModel();
            }

            DropDownListDAL _ddlDal = new DropDownListDAL();
            //  model.DDLSearch = await _ddlDal.GetAdvertisementSearchListAsync(userId, cancellationToken, true);

            return model;
        }

        #endregion


    }
}
