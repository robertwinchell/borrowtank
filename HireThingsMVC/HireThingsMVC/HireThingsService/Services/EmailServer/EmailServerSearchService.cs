using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public class EmailServerSearchService : BaseService<IEmailServerSearchViewModel>, IEmailServerSearchService
    {
        EmailServerDAL _dal;
        DropDownListDAL _ddlDal;
        long userId = System.Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public EmailServerSearchService()
        {
            _dal = new EmailServerDAL();
            _ddlDal = new DropDownListDAL();
        }

        #region
        public async override Task<IEmailServerSearchViewModel> IndexAsync(HttpContext context, CancellationToken cancellationToken)
        {
            IEmailServerSearchViewModel model = await PoulateDropDownListAsync(null, cancellationToken);

            SearchModel searchModel = new SearchModel()
            {
                SearchField = System.Convert.ToInt32(model.DDLSearch[0].Value),
                SearchText = string.Empty
            };

            model = await SearchDataAsync(searchModel, model, cancellationToken);

            return model;
        }

        public async Task<IEmailServerSearchViewModel> SearchAsync(System.Web.HttpContext httpContext, ISearchModel model, CancellationToken cancellationToken)
        {
            IEmailServerSearchViewModel tdModel = await SearchDataAsync(model, null, cancellationToken);
            return tdModel;
        }

        private async Task<IEmailServerSearchViewModel> SearchDataAsync(ISearchModel searchModel, IEmailServerSearchViewModel model, CancellationToken cancellationToken)
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
                          Predicate<string>.GetPredicate("ServerName", searchModel.SearchText, Operator.Like),
                          Predicate<string>.GetPredicate("ServerIP", searchModel.SearchText, Operator.Like));


                    System.Int32 port;
                    if (System.Int32.TryParse(searchModel.SearchText, out port))
                    {
                        pred = CompoundPredicate.GetCompoundPredicate(
                           LogicalOperator.OR,
                           pred,
                           Predicate<System.Int32>.GetPredicate("CONVERT(int,EmailPort)", port, Operator.Equal)
                        );
                    }

                    list.Add(pred);
                }
                else if (searchModel.SearchField == 1)
                {
                    pred = Predicate<string>.GetPredicate("ServerName", searchModel.SearchText, Operator.Like);
                    list.Add(pred);
                }
                else if (searchModel.SearchField == 2)
                {
                    pred = Predicate<string>.GetPredicate("ServerIP", searchModel.SearchText, Operator.Like);
                    list.Add(pred);
                }

                else if (searchModel.SearchField == 3)
                {
                    System.Int32 port;
                    if (System.Int32.TryParse(searchModel.SearchText, out port))
                    {
                        pred = Predicate<System.Int32>.GetPredicate("CONVERT(int,EmailPort)", port, Operator.Equal);
                        list.Add(pred);
                    }
                    else
                    {
                        list.Add(Predicate<string>.GetPredicate("1", "2", Operator.Equal));
                    }
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

            model.EmailServerList = await _dal.SelectAllByFilterAsync(finalPred, cancellationToken, model.GetPageInfo(searchModel.CPage, searchModel.TPage, searchModel.TRows, searchModel.PSize, searchModel.UserId));
            return model;
        }

        private async Task<IEmailServerSearchViewModel> PoulateDropDownListAsync(IEmailServerSearchViewModel model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                model = new EmailServerSearchViewModel();
            }

            DropDownListDAL _ddlDal = new DropDownListDAL();
            model.DDLSearch = await _ddlDal.GetEmailServerSearchListAsync(userId, cancellationToken, true);

            return model;
        }

        #endregion


    }
}
