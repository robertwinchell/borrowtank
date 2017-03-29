using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public class UserService : BaseService<IHireThingsUser>, IUserService
    {
        TempUserDAL _dal = null;
        DropDownListDAL _ddlDAL = null;
        long _userId;
        long userId = Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());
        
        public UserService()
        {
            _dal = new TempUserDAL();
            _ddlDAL = new DropDownListDAL();
        }

        public ForgotPasswordViewModel IsValidateUser(ForgotPasswordViewModel model, ref int retVal)
        {
            string question = "";
            Int64 userId = 0;

            retVal = _dal.IsUserValidPoulateQuestion(model.EmailId, ref question, ref userId);
            if (retVal == 2)
            {
                model.Question = question;
                model.UserId = userId;
            }

            return model;
        }

        public bool ChangePassword(HttpContext context, ChangePasswordViewModel model)
        {
            IEmailLogModel emailLog = generateEmailLogModel(context);
            if (_dal.ChangePassword(model))
            {
                this.SendEmail(emailLog, "", new EmailServerModel() { UserId = model.UserId, UserName = model.UserName }, Constant.EmailType.ChangePassword);
                return true;
            }
            return false;
        }

        public bool sendResetPasswordEmail(HttpContext context, ResetPasswordViewModel model)
        {
            string userName = context.User.Identity.Name;
            if (this.SendEmail(model.EmailId, new EmailServerModel() { UserName = userName }) == 1)
                return true;

            return false;

        }

        public bool sendEmail(HttpContext context, string userEmail, string callBackUrl, IEmailServerModel model, Constant.EmailType emailType, long userId=0, long systemId=0)
        {
            if (model.UserName == null)
                model.UserName = context.User.Identity.Name;
            if (userEmail == null)
                userEmail = Convert.ToString(context.Session[Constant.EmailId]);

            //long userId = Convert.ToInt64(context.Session[Constant.UserId]);
            //long sysytemId = (long)context.Session[Constant.SystemId];
            //IEmailLogModel emailLog = generateEmailLogModel(context);

            IEmailLogModel emailLog = new EmailLogModel { UserId = userId, SystemId = systemId, To = userEmail };

            if (this.SendEmail(emailLog, callBackUrl, model, emailType) == 1)
                return true;

            return false;

        }

        private IEmailLogModel generateEmailLogModel(HttpContext context)
        {
            string userEmail = Convert.ToString(context.Session[Constant.EmailId]);
            long userId = Convert.ToInt64(context.Session[Constant.UserId]);
            long systemId = Convert.ToInt64(context.Session[Constant.SystemId]);
            IEmailLogModel emailLog = new EmailLogModel { UserId = userId, SystemId = systemId, To = userEmail };

            return emailLog;
        }

        

        #region  Async Region
        public async override Task<IHireThingsUser> IndexAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            IHireThingsUser model = new HireThingsUser() { UserId = userId };
            model = await PopulateInitialValuesAsync(model, cancellationToken);
            return model;
        }

        public async Task<IHireThingsUser> IndexPublicAsync(System.Web.HttpContext context, CancellationToken cancellationToken)
        {
            IHireThingsUser model = new HireThingsUser() { UserId = userId,IsActive=true,RoleId=0};
            model = await PopulateInitialValuesAsync(model, cancellationToken);
            return model;
        }
        
        public async Task<IHireThingsUser> IndexAsync(System.Web.HttpContext context, IHireThingsUser model, CancellationToken cancellationToken)
        {
            if (model == null)
                model = new HireThingsUser();
            model = await PopulateInitialValuesAsync(model, cancellationToken);
            return model;
        }

        public  async Task<IHireThingsUser> IndexAsync(HttpContext context, long userId, CancellationToken cancellationToken)
        {
            IHireThingsUser model = await _dal.SelectByIDAsync(userId, userId, cancellationToken);
            model = await PopulateInitialValuesAsync(model, cancellationToken);
            return model;
        }

        public async Task<IHireThingsUser> SaveAsync(HttpContext context, IHireThingsUser model, CancellationToken cancellationToken)
        {
            model.UserId = userId;

            if (model.Id != "0")
            {
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(await _dal.UpdateAsync(model, cancellationToken));
                context.Items[Constant.FormTitle] = "EDIT User";
            }
            else
            {
                model.Id = Convert.ToString(await _dal.SaveAsync(model, cancellationToken));
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(model.Id);
                context.Items[Constant.FormTitle] = "ADD User";
            }

           await PopulateInitialValuesAsync(model, cancellationToken);
            return model;
        }
        public async Task<IHireThingsUser> ProfileUpdateAsync(HttpContext context, IHireThingsUser model, CancellationToken cancellationToken)
        {
            model.UserId = userId;

            if (model.Id != "0")
            {
                context.Items[Constant.QuerySuccess] = Convert.ToBoolean(await _dal.UpdateProfileAsync(model, cancellationToken));
                context.Items[Constant.FormTitle] = "EDIT User";
            }           

            await PopulateInitialValuesAsync(model, cancellationToken);
            return model;
        }

        private async Task<IHireThingsUser> PopulateInitialValuesAsync(IHireThingsUser model, CancellationToken cancellationToken)
        {
           
            long securityQuestionId = 0;
            try
            {
                _userId = Convert.ToInt64(model.Id);
            }
            catch (Exception ex)
            {
                _userId = 0;
            }

            model.CountryList = await _ddlDAL.GetCountryListAsync(userId, cancellationToken, true);
            if (model.CountryId > 0)
                model.LocationList =  await _ddlDAL.GetLocationListAsync(userId, model.LocationId, cancellationToken, true);
            else
                model.LocationList = await _ddlDAL.GetLocationListAsync(userId, -1, cancellationToken, true);

           
            model.SecurityQuestionList = await _ddlDAL.GetSecurityQuestionListAsync(userId, cancellationToken, securityQuestionId, true);
            model.RoleList = await _ddlDAL.GetRoleListAsync(userId, cancellationToken, true);

            model.GenderList = await _ddlDAL.GetGenderListAsync(userId, cancellationToken, true);

           
            model.CountryList[0].Identifier = "ddlCountry";
            model.CountryList[0].SavedValue = model.CountryId;
            model.LocationList[0].Identifier = "ddlLocation";
            model.LocationList[0].SavedValue = model.LocationId;




            return model;
        }

        #endregion
    }
}
