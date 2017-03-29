using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Service
{
    public class WebApiUserSearchService : BaseService<IWebApiUserSearchViewModel>, IWebApiUserSearchService
    {
        WebApiUserDAL _dal = null;
        DropDownListDAL _ddlDal = null;
        long userId = System.Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public WebApiUserSearchService()
        {
            _dal = new WebApiUserDAL();
            _ddlDal = new DropDownListDAL();
        }

       #region
        public async override Task<IWebApiUserSearchViewModel> IndexAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            IWebApiUserSearchViewModel model = await PoulateDropDownListAsync(null, cancellationToken);

            SearchModel searchModel = new SearchModel()
            {
                SystemId = System.Convert.ToInt64(model.DDLSystem[0].Value),
                OrganizationId = System.Convert.ToInt64(model.DDLOrganization[0].Value),
                SearchField = System.Convert.ToInt32(model.DDLSearch[0].Value),
                SearchText = string.Empty,
                UserId = userId
            };

            model = await SearchDataAsync(searchModel, model, cancellationToken);

            return model;
        }

        public async Task<IWebApiUserSearchViewModel> SearchAsync(System.Web.HttpContext httpContext, ISearchModel model, CancellationToken cancellationToken)
        {
            IWebApiUserSearchViewModel tdModel =  await SearchDataAsync(model, null, cancellationToken);
            return tdModel;
        }

        private async Task<IWebApiUserSearchViewModel> SearchDataAsync(ISearchModel searchModel, IWebApiUserSearchViewModel model, CancellationToken cancellationToken)
        {
            IPredicate pred = null;
            IPredicate finalPred = null;
            List<IPredicate> list = new List<IPredicate>();

            if (searchModel.SystemId > 0)
            {
                pred = Predicate<long>.GetPredicate("U.SystemId", searchModel.SystemId, Operator.Equal);
                list.Add(pred);
            }

            //if (searchModel.OrganizationId > 0)
            //{
            //    pred = Predicate<long>.GetPredicate("U.OrganizationId", searchModel.OrganizationId, Operator.Equal);
            //    list.Add(pred);
            //}
            
            if (!string.IsNullOrEmpty(searchModel.SearchText))
            {
                searchModel.SearchText = searchModel.SearchText.Trim();
                if (searchModel.SearchField == -1)
                {
                    pred = CompoundPredicate.GetCompoundPredicate(
                          LogicalOperator.OR,
                          Predicate<string>.GetPredicate("U.FirstName", searchModel.SearchText, Operator.Like),
                          Predicate<string>.GetPredicate("U.EmailId", searchModel.SearchText, Operator.Like),
                          //Predicate<string>.GetPredicate("O.OrganizationName", searchModel.SearchText, Operator.Like),
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
                    pred = Predicate<string>.GetPredicate("U.PhoneNo", searchModel.SearchText, Operator.Like);
                    list.Add(pred);
                }
                else if (searchModel.SearchField == 4)
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

        private async Task<IWebApiUserSearchViewModel> PoulateDropDownListAsync(IWebApiUserSearchViewModel model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                model = new WebApiUserSearchViewModel();
            }

            DropDownListDAL _ddlDal = new DropDownListDAL();
            model.DDLOrganization = await _ddlDal.GetOrganizationListAsync(userId, -1, cancellationToken);
            model.DDLSystem = await _ddlDal.GetSystemListAsync(userId, cancellationToken);
            model.DDLSearch = await _ddlDal.GetWebApiUserSearchListAsync(userId, cancellationToken);

            model.DDLSystem[0].Identifier = "ddlSystem";
            model.DDLOrganization[0].Identifier = "ddlOrganization";
            return model;
        }        

        #endregion

    }
}
