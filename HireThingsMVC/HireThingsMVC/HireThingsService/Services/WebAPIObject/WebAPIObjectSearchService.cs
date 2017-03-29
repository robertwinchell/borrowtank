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
    public class WebApiObjectSearchService : BaseService<IWebApiObjectSearchViewModel>, IWebApiObjectSearchService
    {
        WebApiObjectDAL _dal = null;
        DropDownListDAL _ddlDal = null;
        long userId = System.Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public WebApiObjectSearchService()
        {
            _dal = new WebApiObjectDAL();
            _ddlDal = new DropDownListDAL();
        }

        #region

        public  async override Task<IWebApiObjectSearchViewModel> IndexAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            IWebApiObjectSearchViewModel model = await PoulateDropDownListAsync(null, cancellationToken);
            ISearchModel searchModel = new SearchModel()
            {
                SearchText = string.Empty
            };
            model = await SearchDataAsync(searchModel, model, cancellationToken);
            return model;
        }

        public async Task<IWebApiObjectSearchViewModel> SearchAsync(System.Web.HttpContext httpContext, ISearchModel model, CancellationToken cancellationToken)
        {
            IWebApiObjectSearchViewModel tdmodel = await SearchDataAsync(model, null, cancellationToken);
            return tdmodel;
        }

        private async Task<IWebApiObjectSearchViewModel> SearchDataAsync(ISearchModel searchModel, IWebApiObjectSearchViewModel model, CancellationToken cancellationToken)
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
                          Predicate<string>.GetPredicate("M.Name", searchModel.SearchText, Operator.Like),
                            Predicate<string>.GetPredicate("CO.Name", searchModel.SearchText, Operator.Like),
                          Predicate<string>.GetPredicate("CO.Caption", searchModel.SearchText, Operator.Like));

                    list.Add(pred);
                }
                else if (searchModel.SearchField == 1)
                {
                    pred = Predicate<string>.GetPredicate("M.Name", searchModel.SearchText, Operator.Like);
                    list.Add(pred);
                }
                else if (searchModel.SearchField == 2)
                {
                    pred = Predicate<string>.GetPredicate("CO.Name", searchModel.SearchText, Operator.Like);
                    list.Add(pred);
                }
                else if (searchModel.SearchField == 3)
                {
                    pred = Predicate<string>.GetPredicate("CO.Caption", searchModel.SearchText, Operator.Like);
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

            model.ObjectList = await _dal.SelectAllByFilterAsync(finalPred, cancellationToken, model.GetPageInfo(searchModel.CPage, searchModel.TPage, searchModel.TRows, searchModel.PSize, searchModel.UserId));
            return model;
        }


        private async Task<IWebApiObjectSearchViewModel> PoulateDropDownListAsync(IWebApiObjectSearchViewModel model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                model = new WebApiObjectSearchViewModel();
            }

            model.DDLSearch = await _ddlDal.GetWebApiObjectListAsync(userId, cancellationToken);
            return model;
        }

        #endregion
    }
}
