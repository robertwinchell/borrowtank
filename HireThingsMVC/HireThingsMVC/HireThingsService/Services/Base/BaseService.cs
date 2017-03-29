using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;


namespace ASOL.HireThings.Service
{
    public abstract class BaseService<T> :  IBaseAsyncService<T>
    {
        TempUserDAL _dal;
        long userId = Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());

        public BaseService()
        {
            _dal = new TempUserDAL();
        }
        //public abstract T Index(HttpContext context);



        public async Task<string> SelectCurrentDateTimeLocalizedAsync(System.Web.HttpContext context, long userId, CancellationToken cancellationToken)
        {
            DropDownListDAL _dal = new DropDownListDAL();
            return await _dal.SelectCurrentDateTimeLocalizedAsync(userId, cancellationToken);
        }



        public Int32 SendEmail(IEmailLogModel emailLog, string callBackUrl, IEmailServerModel model, Constant.EmailType emailType)
        {
            TempUserDAL _dal = new TempUserDAL();
            IEmailLogModel emailLogModel = null;
            string emailBody = "";
            string emailSubject = "";
            int retVal = 0;

            retVal = _dal.GetEmailTemplate(model.UserId, model.UserName, callBackUrl, ref emailBody, ref emailSubject, emailType);
            // Initialize emailLogModel
            //emailLogModel = new EmailLogModel() { UserId = model.UserId, To = user.EmailId, Subject = emailSubject, Body = emailBody, EmailType = (int)emailType };
            emailLogModel = new EmailLogModel() { UserId = emailLog.UserId, To = emailLog.To, Subject = emailSubject, Body = emailBody, EmailType = (int)emailType, SystemId = emailLog.SystemId };

            {
                model.SysEmailServers = new List<EmailServerModel>();
                model.SysEmailServers = GetEmailServerList();

                if (model.SysEmailServers.Count > 0)
                {
                    //MailMessage message = new MailMessage();
                    //message.From = new MailAddress("your email address");

                    //message.To.Add(new MailAddress("your recipient"));

                    //message.Subject = "your subject";
                    //message.Body = "content of your email";

                    //SmtpClient client = new SmtpClient();
                    //client.Send(message);



                    Email email = new Email("HireThingsAdminPortal", model.SysEmailServers);
                    email.SetToAddress(emailLog.To);
                    email.SetSubject(emailSubject);
                    email.SetBody(Convert.ToString(emailBody));
                    email.IsBodyHTML = true;
                    if (email.SendEmail())
                    {
                        // Email Log
                        emailLogModel.SentStatus = 1;
                        _dal.EmailLog(emailLogModel);
                        // Email sending successful 
                        retVal = 1;
                    }
                    else
                    {
                        // Email Log
                        emailLogModel.SentStatus = 0;
                        emailLogModel.UnSentReason = "Write Exception Log here";
                        _dal.EmailLog(emailLogModel);
                        retVal = 2;
                    }
                }
                else
                {
                    retVal = 2;
                }
            }

            return retVal;
        }

        public Int32 SendEmail(string userEmail, IEmailServerModel model)
        {
            TempUserDAL _dal = new TempUserDAL();
            string emailBody = "";
            string emailSubject = "";
            int retVal = 0;

            retVal = _dal.GetEmailTemplateChangePassword(model.UserId, model.UserName, ref emailBody, ref emailSubject);

            model.SysEmailServers = new List<EmailServerModel>();

            model.SysEmailServers = GetEmailServerList();

            if (model.SysEmailServers.Count > 0)
            {
                //Email email = new Email("HireThingsAdminPortal", model.SysEmailServers);
                //email.SetToAddress(userEmail);
                //email.SetSubject(emailSubject);
                //email.SetBody(Convert.ToString(emailBody));
                //email.IsBodyHTML = true;
                //if (email.SendEmail())
                //{
                //    // Email sending successful 
                //    retVal = 1;
                //}
                //else
                //{
                //    retVal = 2;
                //}
            }
            else
            {
                retVal = 2;
            }


            return retVal;
        }

        public List<EmailServerModel> GetEmailServerList()
        {
            EmailServerDAL _dal = new EmailServerDAL();
            List<EmailServerModel> serverList = null;

            DataTable dt = _dal.GetSystemEmailServer(userId);

            if (dt == null)
            {
                return null;
            }

            serverList = new List<EmailServerModel>();
            EmailServerModel entity = null;

            foreach (DataRow row in dt.Rows)
            {
                entity = new EmailServerModel();

                entity.IsActive = (Boolean)(row["IsActive"]);
                entity.EmailPort = (int)(row["EmailPort"]);
                entity.EnableSSL = (Boolean)(row["EnableSSL"]);
                entity.ServerId = (Int64)(row["ServerId"]);
                entity.ServerIP = (string)(row["ServerIP"]);
                entity.ServerName = (string)(row["ServerName"]);
                entity.UseDefaultCredentials = (Boolean)(row["UseDefaultCredentials"]);
                entity.UserName = Convert.ToString(row["UserName"]);
                entity.UserPswd = Convert.ToString((row["UserPswd"]));
                entity.Priority = Convert.ToByte((row["Priority"]));

                serverList.Add(entity);
            }

            return serverList;
        }


        #region async

        virtual public async Task<T> IndexAsync(HttpContext context, CancellationToken cancellationToken)
        {
            return await Task.FromResult(default(T));
        }
    

        #endregion


    }
}
