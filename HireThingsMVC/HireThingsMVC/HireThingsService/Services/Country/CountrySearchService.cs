using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public class CountrySearchService : BaseService<ICountrySearchViewModel>, ICountrySearchService 
    {
        CountryDAL _dal;
        DropDownListDAL _ddlDal;
        long userId = System.Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public CountrySearchService()
        {
            _dal = new CountryDAL();
            _ddlDal = new DropDownListDAL();
        }

        #region async service

        public override async Task<ICountrySearchViewModel> IndexAsync(HttpContext context, CancellationToken cancellationToken)
        {
            ICountrySearchViewModel model = await PoulateDropDownListAsync(null, cancellationToken);

            ISearchModel searchModel = new SearchModel()
            {
                CountryId = System.Convert.ToInt64(model.DDLCountry[0].Value),
                 SearchField = System.Convert.ToInt32(model.DDLSearch[0].Value),
                SearchText = string.Empty,
                UserId = userId,
                SortType = -1,
                ColumnName = string.Empty
            };

            model = await SearchDataAsync(searchModel, model, cancellationToken);

            return model;
        }

        public async Task<ICountrySearchViewModel> SearchAsync(HttpContext context, CancellationToken cancellationToken, ISearchModel searchModel)
        {
            ICountrySearchViewModel tdModel = await SearchDataAsync(searchModel, null, cancellationToken);
            return tdModel;
        }

        public async Task<ICountrySearchViewModel> SearchDataAsync(ISearchModel searchModel, ICountrySearchViewModel model, CancellationToken cancellationToken)
        {
            IPredicate pred = null;
            IPredicate finalPred = null;
            List<IPredicate> list = new List<IPredicate>();

            if (searchModel.CountryId > 0)
            {
                pred = Predicate<long>.GetPredicate("T.CountryId", searchModel.CountryId, Operator.Equal);
                list.Add(pred);
            }

            

            if (!string.IsNullOrEmpty(searchModel.SearchText))
            {
                if (searchModel.SearchField == -1)
                {
                    pred = Predicate<string>.GetPredicate("T.CountryName", searchModel.SearchText, Operator.Like);
                    list.Add(pred);
                }
             
                else if (searchModel.SearchField ==1)
                {
                    pred = Predicate<string>.GetPredicate("T.CountryName", searchModel.SearchText, Operator.Like);
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

            model.CountryList = await _dal.SelectAllByFilterAsync(finalPred, cancellationToken, model.GetPageInfo(searchModel.CPage, searchModel.TPage, searchModel.TRows, searchModel.PSize, searchModel.UserId, searchModel.ColumnName, searchModel.SortType)); ;
            return model;
        }

        private async Task<ICountrySearchViewModel> PoulateDropDownListAsync(ICountrySearchViewModel model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                model = new CountrySearchViewModel();
            }

            model.DDLCountry =  await _ddlDal.GetCountryListAsync(userId, cancellationToken);
            model.DDLSearch = await _ddlDal.GetCountrySearchListAsync(userId, cancellationToken);

            model.DDLCountry[0].Identifier = "ddlCountry";
           
            return model;
        }
        #endregion

    }
}
