using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public class LocationSearchService : BaseService<ILocationSearchViewModel>, ILocationSearchService, IBaseAsyncService<ILocationSearchViewModel>
    {
        LocationDAL _dal = null;
        DropDownListDAL _ddlDal = null;
        long userId = System.Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public LocationSearchService()
        {
            _dal = new LocationDAL();
            _ddlDal = new DropDownListDAL();
        }

        public override async Task<ILocationSearchViewModel> IndexAsync(HttpContext context, CancellationToken cancellationToken)
        {
            ILocationSearchViewModel model = await PoulateDropDownListAsync(null, cancellationToken);

            ISearchModel searchModel = new SearchModel()
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

        public async Task<ILocationSearchViewModel> SearchAsync(HttpContext context, ISearchModel searchModel, CancellationToken cancellationToken)
        {
            ILocationSearchViewModel tdModel = await SearchDataAsync(searchModel, null, cancellationToken);
            return tdModel;
        }

        private async Task<ILocationSearchViewModel> SearchDataAsync(ISearchModel searchModel, ILocationSearchViewModel model, CancellationToken cancellationToken)
        {
            IPredicate pred = null;
            IPredicate finalPred = null;
            List<IPredicate> list = new List<IPredicate>();

            var sdsd = searchModel.CPage;


            if (searchModel.CountryId > 0)
            {
                pred = Predicate<long>.GetPredicate("T.CountryId", searchModel.CountryId, Operator.Equal);
                list.Add(pred);
            }

            if (searchModel.LocationId > 0)
            {
                pred = Predicate<long>.GetPredicate("O.LocationId", searchModel.LocationId, Operator.Equal);
                list.Add(pred);
            }

            if (!string.IsNullOrEmpty(searchModel.SearchText))
            {
                if (searchModel.SearchField == -1)
                {
                    //pred = CompoundPredicate.GetCompoundPredicate(
                    //        Predicate<string>.GetPredicate("O.LocationName", searchModel.SearchText, Operator.Like),
                    //        Predicate<string>.GetPredicate("O.LocationRefNo", searchModel.SearchText, Operator.Like), LogicalOperator.OR);

                    //list.Add(pred);

                    pred = Predicate<string>.GetPredicate("L.LocationName", searchModel.SearchText, Operator.Like);
                    list.Add(pred);
                }
                else if (searchModel.SearchField == 1)
                {
                    pred = Predicate<string>.GetPredicate("L.LocationName", searchModel.SearchText, Operator.Like);
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
                model =  await PoulateDropDownListAsync(model, cancellationToken);
            }

            model.LocationList = await _dal.SelectAllByFilterAsync(finalPred, cancellationToken, model.GetPageInfo(searchModel.CPage, searchModel.TPage, searchModel.TRows, searchModel.PSize, searchModel.UserId));
            return model;
        }

        private async  Task<ILocationSearchViewModel> PoulateDropDownListAsync(ILocationSearchViewModel model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                model = new LocationSearchViewModel();
            }

            model.DDLCountry = await _ddlDal.GetCountryListAsync(userId, cancellationToken);
            model.DDLLocation = await _ddlDal.GetLocationListAsync(userId, 0,  cancellationToken, true);
            model.DDLSearch = await _ddlDal.GetLocationSearchListAsync(userId, cancellationToken);

            model.DDLCountry[0].Identifier = "DDLCountry";
            model.DDLLocation[0].Identifier = "ddlLocation";

            return model;
        }
    }
}
