using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;


namespace ASOL.HireThings.Service
{
    public class UserSearchService : BaseService<IUserSearchViewModel>, IUserSearchService
    {
        TempUserDAL _dal = null;
        DropDownListDAL _ddlDal = null;
        long userId = System.Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public UserSearchService()
        {
            _dal = new TempUserDAL();
            _ddlDal = new DropDownListDAL();
        }

       #region
        public async override Task<IUserSearchViewModel> IndexAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            IUserSearchViewModel model = await PoulateDropDownListAsync(null, cancellationToken);

            SearchModel searchModel = new SearchModel()
            {
                CountryId = System.Convert.ToInt64(model.DDLCountry[0].Value),
                LocationId = System.Convert.ToInt64(model.DDLLocation[0].Value),
                SearchField = System.Convert.ToInt32(model.DDLSearch[0].Value),
                SearchText = string.Empty,
                UserId = userId
            };

            model = await SearchDataAsync(searchModel, model, cancellationToken);

            return model;
        }

        public async Task<IUserSearchViewModel> SearchAsync(System.Web.HttpContext httpContext, ISearchModel model, CancellationToken cancellationToken)
        {
            IUserSearchViewModel tdModel =  await SearchDataAsync(model, null, cancellationToken);
            return tdModel;
        }

        private async Task<IUserSearchViewModel> SearchDataAsync(ISearchModel searchModel, IUserSearchViewModel model, CancellationToken cancellationToken)
        {
            IPredicate pred = null;
            IPredicate finalPred = null;
            List<IPredicate> list = new List<IPredicate>();

            if (searchModel.CountryId > 0)
            {
                pred = Predicate<long>.GetPredicate("C.CountryId", searchModel.CountryId, Operator.Equal);
                list.Add(pred);
            }

            if (searchModel.LocationId > 0)
            {
                pred = Predicate<long>.GetPredicate("L.LocationId", searchModel.LocationId, Operator.Equal);
                list.Add(pred);
            }
            
            if (!string.IsNullOrEmpty(searchModel.SearchText))
            {
                searchModel.SearchText = searchModel.SearchText.Trim();
                if (searchModel.SearchField == -1)
                {
                    pred = CompoundPredicate.GetCompoundPredicate(
                          LogicalOperator.OR,
                          Predicate<string>.GetPredicate("U.FirstName", searchModel.SearchText, Operator.Like),
                          Predicate<string>.GetPredicate("U.EmailId", searchModel.SearchText, Operator.Like),
                          Predicate<string>.GetPredicate("L.LocationName", searchModel.SearchText, Operator.Like),
                          Predicate<string>.GetPredicate("U.PhoneNo", searchModel.SearchText, Operator.Like),
                          Predicate<string>.GetPredicate("U.Address2", searchModel.SearchText, Operator.Like));

                    list.Add(pred);
                }
                else if (searchModel.SearchField == 1)
                {
                    pred = Predicate<string>.GetPredicate("U.FirstName", searchModel.SearchText, Operator.Like);
                    list.Add(pred);
                }
                else if (searchModel.SearchField == 2)
                {
                    pred = Predicate<string>.GetPredicate("U.EmailId", searchModel.SearchText, Operator.Like);
                    list.Add(pred);
                }
                else if (searchModel.SearchField == 3)
                {
                    pred = Predicate<string>.GetPredicate("L.LocationName", searchModel.SearchText, Operator.Like);
                    list.Add(pred);
                }
                else if (searchModel.SearchField == 4)
                {
                    pred = Predicate<string>.GetPredicate("U.PhoneNo", searchModel.SearchText, Operator.Like);
                    list.Add(pred);
                }
                else if (searchModel.SearchField == 5)
                {
                    pred = Predicate<string>.GetPredicate("U.Address2", searchModel.SearchText, Operator.Like);
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

            model.UserList = await _dal.SelectAllByFilterAsync(finalPred, cancellationToken, model.GetPageInfo(searchModel.CPage, searchModel.TPage, searchModel.TRows, searchModel.PSize, searchModel.UserId));
            return model;
        }

        private async Task<IUserSearchViewModel> PoulateDropDownListAsync(IUserSearchViewModel model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                model = new UserSearchViewModel();
            }

            DropDownListDAL _ddlDal = new DropDownListDAL();
            model.DDLLocation = await _ddlDal.GetLocationListAsync(userId, -1, cancellationToken);
            model.DDLCountry = await _ddlDal.GetCountryListAsync(userId, cancellationToken);
            model.DDLSearch = await _ddlDal.GetUserSearchListAsync(userId, cancellationToken);

            model.DDLCountry[0].Identifier = "ddlCountry";
            model.DDLLocation[0].Identifier = "ddlLocation";
            return model;
        }

        #endregion

    }
}
