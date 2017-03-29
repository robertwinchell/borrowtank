using ASOL.HireThings.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Core
{
    public class TempUserDAL : AbstractBaseDAL<IHireThingsUser>
    {
        public int IsUserValidPoulateQuestion(string userEmail, ref string question, ref Int64 userId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.EMAIL, GetParameter(DBObjects.SPParameter.EMAIL, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, userEmail));
            param.Add(DBObjects.SPParameter.RetQuestion, GetParameter(DBObjects.SPParameter.RetQuestion, ParameterDirection.Output, ((int)SqlDbType.VarChar), 1024, null));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Output, ((int)SqlDbType.BigInt), 8, null));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, null));

            this.ExecuteSP(DBObjects.StoredProcedures.pspSecurityEmailVerify.ToString(), ref param);

            question = Convert.ToString(((SqlParameter)param[DBObjects.SPParameter.RetQuestion]).Value);
            if (question != "")
                userId = Convert.ToInt64(((SqlParameter)param[DBObjects.SPParameter.UserId]).Value);
            return (int)((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value;
        }

        public bool VerifySecurityAnswer(long userId, string answer)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            param.Add(DBObjects.SPParameter.Answer, GetParameter(DBObjects.SPParameter.Answer, ParameterDirection.Input, ((int)SqlDbType.VarChar), 1024, answer));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, null));

            this.ExecuteSP(DBObjects.StoredProcedures.pspSecurityAnswerVerify.ToString(), ref param);

            return Convert.ToBoolean((Int32)(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value));
        }

        public int GetEmailTemplate(long userId, string userName, string callBackUrl, ref string RetEmailTemplate, ref string RetEmailSubject, Constant.EmailType emailType)
        {
            //int retVal = 0;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            param.Add(DBObjects.SPParameter.UserName, GetParameter(DBObjects.SPParameter.UserName, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, userName));
            param.Add(DBObjects.SPParameter.CallBackUrl, GetParameter(DBObjects.SPParameter.CallBackUrl, ParameterDirection.Input, ((int)SqlDbType.VarChar), 1024, PublicFunctions.ConvertNulltoDBNull(callBackUrl)));
            param.Add(DBObjects.SPParameter.RetEmailTemplate, GetParameter(DBObjects.SPParameter.RetEmailTemplate, ParameterDirection.Output, ((int)SqlDbType.VarChar), 3096, RetEmailTemplate));
            param.Add(DBObjects.SPParameter.RetEmailSubject, GetParameter(DBObjects.SPParameter.RetEmailSubject, ParameterDirection.Output, ((int)SqlDbType.VarChar), 500, RetEmailSubject));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, null));
            param.Add(DBObjects.SPParameter.EmailType, GetParameter(DBObjects.SPParameter.EmailType, ParameterDirection.Input, ((int)SqlDbType.Int), 4, emailType));

            this.ExecuteSP(DBObjects.StoredProcedures.pspEmailTemplate.ToString(), ref param);

            RetEmailTemplate = (string)((SqlParameter)param[DBObjects.SPParameter.RetEmailTemplate]).Value;
            RetEmailSubject = (string)((SqlParameter)param[DBObjects.SPParameter.RetEmailSubject]).Value;
            return (int)(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value);

        }

        public int GetEmailTemplateChangePassword(long userId, string userName, ref string RetEmailTemplate, ref string RetEmailSubject)
        {
            //int retVal = 0;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            param.Add(DBObjects.SPParameter.UserName, GetParameter(DBObjects.SPParameter.UserName, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, userName));
            param.Add(DBObjects.SPParameter.RetEmailTemplate, GetParameter(DBObjects.SPParameter.RetEmailTemplate, ParameterDirection.Output, ((int)SqlDbType.VarChar), 3096, RetEmailTemplate));
            param.Add(DBObjects.SPParameter.RetEmailSubject, GetParameter(DBObjects.SPParameter.RetEmailSubject, ParameterDirection.Output, ((int)SqlDbType.VarChar), 500, RetEmailSubject));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, null));

            this.ExecuteSP(DBObjects.StoredProcedures.pspEmailTemplateChangePassword.ToString(), ref param);

            RetEmailTemplate = (string)((SqlParameter)param[DBObjects.SPParameter.RetEmailTemplate]).Value;
            RetEmailSubject = (string)((SqlParameter)param[DBObjects.SPParameter.RetEmailSubject]).Value;
            return (int)(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value);

        }

        public bool ChangePassword(ChangePasswordViewModel model)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            //param.Add(DBObjects.SPParameter.OldPIN, GetParameter(DBObjects.SPParameter.OldPIN, ParameterDirection.Input, ((int)SqlDbType.VarChar), 20, model.OldPassword));
            param.Add(DBObjects.SPParameter.PIN, GetParameter(DBObjects.SPParameter.PIN, ParameterDirection.Input, ((int)SqlDbType.VarChar), 20, model.NewPassword));
            //param.Add(DBObjects.SPParameter.PINHash, GetParameter(DBObjects.SPParameter.PINHash, ParameterDirection.Input, ((int)SqlDbType.VarChar), 255, model.PINHash));
            param.Add(DBObjects.SPParameter.UpdateUserId, GetParameter(DBObjects.SPParameter.UpdateUserId, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, model.UserId));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 100, 0));

            this.ExecuteSP(DBObjects.StoredProcedures.pspChangePassword.ToString(), ref param);

            return Convert.ToBoolean((Int32)(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value));
        }

        public bool EmailLog(IEmailLogModel model)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.UserId)));
            param.Add(DBObjects.SPParameter.SystemId, GetParameter(DBObjects.SPParameter.SystemId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.SystemId)));
            param.Add(DBObjects.SPParameter.OrganizationID, GetParameter(DBObjects.SPParameter.OrganizationID, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.OrganizationId)));
            param.Add(DBObjects.SPParameter.WSId, GetParameter(DBObjects.SPParameter.WSId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.WSId)));
            param.Add(DBObjects.SPParameter.To, GetParameter(DBObjects.SPParameter.To, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, PublicFunctions.ConvertNulltoDBNull(model.To)));
            param.Add(DBObjects.SPParameter.Cc, GetParameter(DBObjects.SPParameter.Cc, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, PublicFunctions.ConvertNulltoDBNull(model.Cc)));
            param.Add(DBObjects.SPParameter.Bcc, GetParameter(DBObjects.SPParameter.Bcc, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, PublicFunctions.ConvertNulltoDBNull(model.Bcc)));
            param.Add(DBObjects.SPParameter.Subject, GetParameter(DBObjects.SPParameter.Subject, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, model.Subject));
            param.Add(DBObjects.SPParameter.Body, GetParameter(DBObjects.SPParameter.Body, ParameterDirection.Input, ((int)SqlDbType.VarChar), 1024, model.Body));
            param.Add(DBObjects.SPParameter.SentStatus, GetParameter(DBObjects.SPParameter.SentStatus, ParameterDirection.Input, ((int)SqlDbType.TinyInt), 2, model.SentStatus));
            param.Add(DBObjects.SPParameter.UnSentReason, GetParameter(DBObjects.SPParameter.UnSentReason, ParameterDirection.Input, ((int)SqlDbType.VarChar), 50, PublicFunctions.ConvertNulltoDBNull(model.UnSentReason)));
            param.Add(DBObjects.SPParameter.EmailType, GetParameter(DBObjects.SPParameter.EmailType, ParameterDirection.Input, ((int)SqlDbType.Int), 4, model.EmailType));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 1));

            this.ExecuteSP(DBObjects.StoredProcedures.pspEmailLog.ToString(), ref param);

            return Convert.ToBoolean((Int32)(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value));

        }

        public bool UpdatePINHash(IHireThingsUser model)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.Id));
            param.Add(DBObjects.SPParameter.PINHash, GetParameter(DBObjects.SPParameter.PINHash, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, model.PINHash));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

            this.ExecuteSP(DBObjects.StoredProcedures.pspUpdatePINHash.ToString(), ref param);

            return Convert.ToBoolean((Int32)(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value));
        }

        #region deprecated on May 25,2015 - R
        public IHireThingsUser Select(IHireThingsUser user)
        {
            DataTable dt = null;
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add(DBObjects.SPParameter.EMAIL, GetParameter(DBObjects.SPParameter.EMAIL, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, user.UserName));
            dt = this.GetSPDataTable(DBObjects.StoredProcedures.pspValidateUserPORTAL.ToString(), ref param);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    user.EmailId = row["EmailId"].ToString();
                    user.UserName = row["UserName"].ToString();
                    user.Id = row["UserId"].ToString();
                  user.RoleId= Convert.ToInt64(row["RoleId"].ToString());
                    user.PINHash = row["PINHash"].ToString();
                    user.IsEmailConfirmed = Convert.ToBoolean(row["IsEmailConfirmed"]);
                 
                }
            }
            else
            {
                user = null;
            }

            return user;
        }
        #endregion


        #region
        public async  Task<string> SaveStrAsync(IHireThingsUser model, CancellationToken cancellationToken)
        {
            long retVal;
             retVal = (await SaveAsync(model, cancellationToken));
            model.Id = retVal.ToString();
            return retVal.ToString(); 


        }


        public async override Task<long> SaveAsync(IHireThingsUser model, CancellationToken cancellationToken)
        {
            
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.LocationID, GetParameter(DBObjects.SPParameter.LocationID, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.LocationId)));
            param.Add(DBObjects.SPParameter.CountryId, GetParameter(DBObjects.SPParameter.CountryId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.CountryId)));
            param.Add(DBObjects.SPParameter.RoleId, GetParameter(DBObjects.SPParameter.RoleId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.RoleId)));
            param.Add(DBObjects.SPParameter.FIRSTNAME, GetParameter(DBObjects.SPParameter.FIRSTNAME, ParameterDirection.Input, ((int)SqlDbType.VarChar), 30, PublicFunctions.ConvertNulltoDBNull(model.FirstName)));
            param.Add(DBObjects.SPParameter.MIDDLENAME, GetParameter(DBObjects.SPParameter.MIDDLENAME, ParameterDirection.Input, ((int)SqlDbType.VarChar), 1, PublicFunctions.ConvertNulltoDBNull(model.MiddleName)));
            param.Add(DBObjects.SPParameter.LASTNAME, GetParameter(DBObjects.SPParameter.LASTNAME, ParameterDirection.Input, ((int)SqlDbType.VarChar), 30, PublicFunctions.ConvertNulltoDBNull(model.LastName)));
            param.Add(DBObjects.SPParameter.EMAIL, GetParameter(DBObjects.SPParameter.EMAIL, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, PublicFunctions.ConvertNulltoDBNull(model.EmailId)));
            param.Add(DBObjects.SPParameter.PHONENUMBER, GetParameter(DBObjects.SPParameter.PHONENUMBER, ParameterDirection.Input, ((int)SqlDbType.VarChar), 30, PublicFunctions.ConvertNulltoDBNull(model.PhoneNo)));
            param.Add(DBObjects.SPParameter.Address1, GetParameter(DBObjects.SPParameter.Address1, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, PublicFunctions.ConvertNulltoDBNull(model.Address1)));
            param.Add(DBObjects.SPParameter.Address2, GetParameter(DBObjects.SPParameter.Address2, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, PublicFunctions.ConvertNulltoDBNull(model.Address2)));
            param.Add(DBObjects.SPParameter.Address3, GetParameter(DBObjects.SPParameter.Address3, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, PublicFunctions.ConvertNulltoDBNull(model.Address3)));
            param.Add(DBObjects.SPParameter.PIN, GetParameter(DBObjects.SPParameter.PIN, ParameterDirection.Input, ((int)SqlDbType.VarChar), 20, model.Pin));
            param.Add(DBObjects.SPParameter.Active, GetParameter(DBObjects.SPParameter.Active, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.RFIDPIN, GetParameter(DBObjects.SPParameter.RFIDPIN, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, "0x01000000428EC9DAF430F49BD0DBD9CF4994647BA7EABC48E7AF9D7E"));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Output, ((int)SqlDbType.BigInt), 8, 0));
            
            param.Add(DBObjects.SPParameter.PINHash, GetParameter(DBObjects.SPParameter.PINHash, ParameterDirection.Input, ((int)SqlDbType.VarChar), 255, model.PINHash));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 1));
            param.Add(DBObjects.SPParameter.EmailConfirmed, GetParameter(DBObjects.SPParameter.EmailConfirmed, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsEmailConfirmed));
            param.Add(DBObjects.SPParameter.AddUserId, GetParameter(DBObjects.SPParameter.AddUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.SecurityQuestionId, GetParameter(DBObjects.SPParameter.SecurityQuestionId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.SecurityQuestionID));
            param.Add(DBObjects.SPParameter.Answer, GetParameter(DBObjects.SPParameter.Answer, ParameterDirection.Input, ((int)SqlDbType.VarChar), 1024, model.SecurityAnswer));
            param.Add(DBObjects.SPParameter.DOB, GetParameter(DBObjects.SPParameter.DOB, ParameterDirection.Input, ((int)SqlDbType.Date), 8, PublicFunctions.ConvertNulltoDBNull(model.DOB)));
            param.Add(DBObjects.SPParameter.GenderId, GetParameter(DBObjects.SPParameter.GenderId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.GenderId)));
            await this.ExecuteSPAsync(DBObjects.StoredProcedures.pspUserSave.ToString(), cancellationToken, param);
            return (long)(((SqlParameter)param[DBObjects.SPParameter.UserId]).Value);
        }

        

        public async override Task<bool> UpdateAsync(IHireThingsUser model, CancellationToken cancellationToken)
        {
            
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.LocationID, GetParameter(DBObjects.SPParameter.LocationID, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.LocationId)));
            param.Add(DBObjects.SPParameter.CountryId, GetParameter(DBObjects.SPParameter.CountryId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.CountryId)));
            param.Add(DBObjects.SPParameter.RoleId, GetParameter(DBObjects.SPParameter.RoleId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.RoleId));
            param.Add(DBObjects.SPParameter.FIRSTNAME, GetParameter(DBObjects.SPParameter.FIRSTNAME, ParameterDirection.Input, ((int)SqlDbType.VarChar), 30, model.FirstName));
            param.Add(DBObjects.SPParameter.MIDDLENAME, GetParameter(DBObjects.SPParameter.MIDDLENAME, ParameterDirection.Input, ((int)SqlDbType.VarChar), 1, PublicFunctions.ConvertNulltoDBNull(model.MiddleName)));
            param.Add(DBObjects.SPParameter.LASTNAME, GetParameter(DBObjects.SPParameter.LASTNAME, ParameterDirection.Input, ((int)SqlDbType.VarChar), 30, model.LastName));
            param.Add(DBObjects.SPParameter.EMAIL, GetParameter(DBObjects.SPParameter.EMAIL, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, model.EmailId));
            param.Add(DBObjects.SPParameter.PHONENUMBER, GetParameter(DBObjects.SPParameter.PHONENUMBER, ParameterDirection.Input, ((int)SqlDbType.VarChar), 30, PublicFunctions.ConvertNulltoDBNull(model.PhoneNo)));
            param.Add(DBObjects.SPParameter.Address1, GetParameter(DBObjects.SPParameter.Address1, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, PublicFunctions.ConvertNulltoDBNull(model.Address1)));
            param.Add(DBObjects.SPParameter.Address2, GetParameter(DBObjects.SPParameter.Address2, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, PublicFunctions.ConvertNulltoDBNull(model.Address2)));
            param.Add(DBObjects.SPParameter.Address3, GetParameter(DBObjects.SPParameter.Address3, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, PublicFunctions.ConvertNulltoDBNull(model.Address3)));
            param.Add(DBObjects.SPParameter.Active, GetParameter(DBObjects.SPParameter.Active, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.Id));            
            param.Add(DBObjects.SPParameter.SecurityQuestionId, GetParameter(DBObjects.SPParameter.SecurityQuestionId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.SecurityQuestionID));
            param.Add(DBObjects.SPParameter.Answer, GetParameter(DBObjects.SPParameter.Answer, ParameterDirection.Input, ((int)SqlDbType.VarChar), 1024, model.SecurityAnswer));
            param.Add(DBObjects.SPParameter.DOB, GetParameter(DBObjects.SPParameter.DOB, ParameterDirection.Input, ((int)SqlDbType.Date), 8, PublicFunctions.ConvertNulltoDBNull(model.DOB)));
            param.Add(DBObjects.SPParameter.GenderId, GetParameter(DBObjects.SPParameter.GenderId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.GenderId)));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 1));
            param.Add(DBObjects.SPParameter.UpdateUserId, GetParameter(DBObjects.SPParameter.UpdateUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            await  this.ExecuteSPAsync(DBObjects.StoredProcedures.pspUserUpdate.ToString(),cancellationToken,  param);

            return Convert.ToBoolean((Int32)(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value));
        }

        public async  Task<bool> UpdateProfileAsync(IHireThingsUser model, CancellationToken cancellationToken)
        {
            
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.LocationID, GetParameter(DBObjects.SPParameter.LocationID, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.LocationId)));
            param.Add(DBObjects.SPParameter.CountryId, GetParameter(DBObjects.SPParameter.CountryId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.CountryId)));
            param.Add(DBObjects.SPParameter.RoleId, GetParameter(DBObjects.SPParameter.RoleId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.RoleId));
            param.Add(DBObjects.SPParameter.FIRSTNAME, GetParameter(DBObjects.SPParameter.FIRSTNAME, ParameterDirection.Input, ((int)SqlDbType.VarChar), 30, model.FirstName));
            param.Add(DBObjects.SPParameter.MIDDLENAME, GetParameter(DBObjects.SPParameter.MIDDLENAME, ParameterDirection.Input, ((int)SqlDbType.VarChar), 1, PublicFunctions.ConvertNulltoDBNull(model.MiddleName)));
            param.Add(DBObjects.SPParameter.LASTNAME, GetParameter(DBObjects.SPParameter.LASTNAME, ParameterDirection.Input, ((int)SqlDbType.VarChar), 30, model.LastName));
            param.Add(DBObjects.SPParameter.EMAIL, GetParameter(DBObjects.SPParameter.EMAIL, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, model.EmailId));
            param.Add(DBObjects.SPParameter.PHONENUMBER, GetParameter(DBObjects.SPParameter.PHONENUMBER, ParameterDirection.Input, ((int)SqlDbType.VarChar), 30, PublicFunctions.ConvertNulltoDBNull(model.PhoneNo)));
            param.Add(DBObjects.SPParameter.Address1, GetParameter(DBObjects.SPParameter.Address1, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, PublicFunctions.ConvertNulltoDBNull(model.Address1)));
            param.Add(DBObjects.SPParameter.Address2, GetParameter(DBObjects.SPParameter.Address2, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, PublicFunctions.ConvertNulltoDBNull(model.Address2)));
            param.Add(DBObjects.SPParameter.Address3, GetParameter(DBObjects.SPParameter.Address3, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, PublicFunctions.ConvertNulltoDBNull(model.Address3)));
            param.Add(DBObjects.SPParameter.Active, GetParameter(DBObjects.SPParameter.Active, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.Id));            
            param.Add(DBObjects.SPParameter.SecurityQuestionId, GetParameter(DBObjects.SPParameter.SecurityQuestionId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.SecurityQuestionID));
            param.Add(DBObjects.SPParameter.Answer, GetParameter(DBObjects.SPParameter.Answer, ParameterDirection.Input, ((int)SqlDbType.VarChar), 1024, model.SecurityAnswer));
            param.Add(DBObjects.SPParameter.DOB, GetParameter(DBObjects.SPParameter.DOB, ParameterDirection.Input, ((int)SqlDbType.Date), 8, PublicFunctions.ConvertNulltoDBNull(model.DOB)));
            param.Add(DBObjects.SPParameter.GenderId, GetParameter(DBObjects.SPParameter.GenderId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.GenderId)));            
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 1));
            param.Add(DBObjects.SPParameter.UpdateUserId, GetParameter(DBObjects.SPParameter.UpdateUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            await this.ExecuteSPAsync(DBObjects.StoredProcedures.pspUserProfileUpdate.ToString(), cancellationToken, param);

            return Convert.ToBoolean((Int32)(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value));
        }

        public async override Task<IHireThingsUser> SelectByIDAsync(long id, long userId, CancellationToken cancellationToken)
        {
            DataTable dt = null;
            Dictionary<string, object> param = new Dictionary<string, object>();
            IHireThingsUser user = null;

            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, id));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspSelectUserById.ToString(), cancellationToken, param);

            if (dt != null && dt.Rows.Count > 0)
            {
                user = new HireThingsUser();

                
                user.RoleId = Convert.ToInt64(PublicFunctions.ConvertDBNullToNull(dt.Rows[0]["RoleId"]));
                user.EmailId = Convert.ToString(dt.Rows[0]["EmailId"]);
                user.ConfirmEmailId = Convert.ToString(dt.Rows[0]["EmailId"]);
                user.UserName = Convert.ToString(dt.Rows[0]["UserName"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.MiddleName = Convert.ToString(dt.Rows[0]["MiddleName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.CountryId = Convert.ToInt64(PublicFunctions.ConvertDBNullToNull(dt.Rows[0]["CountryId"]));
                user.LocationId = Convert.ToInt64(PublicFunctions.ConvertDBNullToNull(dt.Rows[0]["LocationId"]));
                user.RoleName = Convert.ToString(dt.Rows[0]["RoleName"]);
                user.GenderId = Convert.ToInt64(PublicFunctions.ConvertDBNullToNull(dt.Rows[0]["GenderId"]));
                user.DOB = Convert.ToDateTime(PublicFunctions.ConvertDBNullToNull(dt.Rows[0]["DOB"]));
                user.PhoneNo = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dt.Rows[0]["PhoneNo"]));
                user.Address1 = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dt.Rows[0]["Address1"]));
                user.Address2 = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dt.Rows[0]["Address2"]));
                user.Address3 = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dt.Rows[0]["Address3"]));
                user.SecurityQuestionID = Convert.ToInt64(PublicFunctions.ConvertDBNullToNull(dt.Rows[0]["SecurityQuestionID"]));
                user.SecurityAnswer = "***";
                user.Id = dt.Rows[0]["UserId"].ToString();
                user.RoleId = Convert.ToInt64(dt.Rows[0]["RoleId"]);
                user.PINHash = dt.Rows[0]["PINHash"].ToString();                
                user.IsEmailConfirmed = Convert.ToBoolean(dt.Rows[0]["IsEmailConfirmed"]);
                user.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
            }

            return user;
        }

        public async override Task<List<IHireThingsUser>> SelectAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async override Task<List<IHireThingsUser>> SelectAllByFilterAsync(IPredicate predicate, CancellationToken cancellationToken,  IPageInfo pageInfo = null)
        {
            string retVal = predicate.Serialize();

            DataTable dt;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.Predicate, GetParameter(DBObjects.SPParameter.Predicate, ParameterDirection.Input, ((int)SqlDbType.NVarChar), retVal.Length, retVal));
            param.Add(DBObjects.SPParameter.CPage, GetParameter(DBObjects.SPParameter.CPage, ParameterDirection.Input, ((int)SqlDbType.Int), 8, pageInfo.CPage));
            param.Add(DBObjects.SPParameter.PSize, GetParameter(DBObjects.SPParameter.PSize, ParameterDirection.Input, ((int)SqlDbType.Int), 8, Convert.ToInt32((pageInfo.PSize))));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, pageInfo.UserId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.pspUserSearch.ToString(), cancellationToken, param);

            List<IHireThingsUser> list = new List<IHireThingsUser>();
            list = (from DataRow dr in dt.Rows
                    select new HireThingsUser
                    {
                        Id = Convert.ToString(dr["UserId"]),
                        FirstName = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dr["FirstName"])),
                        MiddleName = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dr["MiddleName"])),
                        LastName = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dr["LastName"])),
                        EmailId = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dr["EmailId"])),
                        LocationName = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dr["LocationName"])),
                        PhoneNo = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dr["PhoneNo"])),
                        Address2 = Convert.ToString(PublicFunctions.ConvertDBNullToNull(dr["Address2"])),
                        IsActive = Convert.ToBoolean(PublicFunctions.ConvertDBNullToNull(dr["IsActive"])),
                        IsSystemGenerated = Convert.ToBoolean(PublicFunctions.ConvertDBNullToNull(dr[DBObjects.Fields.IsSystemGenerated])),
                        TRows = Convert.ToInt32(dr[DBObjects.Fields.TRows]),
                        CPage = Convert.ToInt32(dr[DBObjects.Fields.CPage]),
                        PSize = dr[DBObjects.Fields.PSize].ToString()
                    }).ToList<IHireThingsUser>();

            return list;
        }

        public async Task<bool> SetEmailConfirmedAsync(IHireThingsUser model, CancellationToken cancellationToken)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.Id));
            param.Add(DBObjects.SPParameter.EmailConfirmed, GetParameter(DBObjects.SPParameter.EmailConfirmed, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsEmailConfirmed));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

            await this.ExecuteSPAsync(DBObjects.StoredProcedures.pspSetEmailConfirmed.ToString(), cancellationToken, param);
            return Convert.ToBoolean(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value);
        }
        #endregion
    }
}
