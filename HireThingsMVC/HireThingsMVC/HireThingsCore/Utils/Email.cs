using System;
using System.Collections.Generic;
using System.Net.Mail;
using ASOL.HireThings.Model;

namespace ASOL.HireThings.Core
{
    public class Email : Object
    {
        private int _timeOut = 48000;
        private string _subject;
        private string _bodyText;
        private bool _isHTMLBody = true;

        private System.Net.Mail.MailAddress _fromAddress;
        private List<System.Net.Mail.MailAddress> _toAddressList;
        private List<System.Net.Mail.MailAddress> _ccAddressList;
        private List<System.Net.Mail.MailAddress> _bccAddressList;
        private List<System.Net.Mail.Attachment> _attachmentsList;
        private List<EmailServerModel> _emailServerList;
        private string _componentName;

        #region "Private Methods"

        public Email(string componentName, List<EmailServerModel> serverList)
        {
            _componentName = componentName;
            _emailServerList = serverList;
        }

        private void setEmailIDs(ref List<System.Net.Mail.MailAddress> addressList, string addressString)
        {
            if (addressList == null)
            {
                addressList = new List<System.Net.Mail.MailAddress>();
            }

            addressList.Clear();
            System.Net.Mail.MailAddress addr = null;

            string id, name;
            string[] email;
            foreach (string str in addressString.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                foreach (string addStr in str.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    email = addStr.Split(new string[] { "<" }, StringSplitOptions.RemoveEmptyEntries);
                    if (email.Length > 1)
                    {
                        name = email[0].Trim();
                        id = email[1].Replace(">", "").Trim();
                    }
                    else
                    {
                        id = email[0];
                        name = string.Empty;
                    }

                    addr = new System.Net.Mail.MailAddress(id, name);
                    addressList.Add(addr);
                }
            }
        }

        private void addEmailId(ref List<System.Net.Mail.MailAddress> addressList, string emailId, string name = "")
        {
            if (addressList == null)
            {
                addressList = new List<System.Net.Mail.MailAddress>();
            }

            System.Net.Mail.MailAddress addr = new System.Net.Mail.MailAddress(emailId, name);
        }

        #endregion

        #region "Public Methods"

        public void SetFromAddress(string emailID, string displayName = "")
        {
            _fromAddress = new System.Net.Mail.MailAddress(emailID, displayName);
        }

        public void SetToAddress(string toAddressList)
        {
            setEmailIDs(ref _toAddressList, toAddressList);
        }

        public void SetCCAddress(string toAddressList)
        {
            setEmailIDs(ref _ccAddressList, toAddressList);
        }

        public void SetBCCAddress(string toAddressList)
        {
            setEmailIDs(ref _bccAddressList, toAddressList);
        }

        public void AddToAddress(string emailId, string name = null)
        {
            addEmailId(ref _toAddressList, emailId, name);
        }

        public void AddCCAddress(string emailId, string name = null)
        {
            addEmailId(ref _ccAddressList, emailId, name);
        }

        public void AddBCCAddress(string emailId, string name = null)
        {
            addEmailId(ref _bccAddressList, emailId, name);
        }

        public void AddAttachment(System.IO.Stream content, string contentType, string fileName)
        {
            if (_attachmentsList == null)
            {
                _attachmentsList = new List<System.Net.Mail.Attachment>();
            }

            System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(content, fileName, contentType);
            _attachmentsList.Add(attachment);
        }

        public void SetSubject(string subject)
        {
            _subject = subject;
        }

        public void SetBody(string body)
        {
            _bodyText = body;
        }

        public bool IsBodyHTML
        {
            set
            {
                _isHTMLBody = value;
            }
        }

        public bool SendEmail()
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

            //if (_fromAddress == null)
            //{
            //    if (string.IsNullOrEmpty(_smtpUserId))
            //        throw new Exception("From Email Address not provided");

            //    SetFromAddress(_smtpUserId);  
            //}

            if (_fromAddress != null)
            {
                msg.From = _fromAddress;
            }

            if (_toAddressList == null || _toAddressList.Count < 1)
            {
                throw new Exception("To Email Address not provided");
            }

            foreach (System.Net.Mail.MailAddress adr in _toAddressList)
            {
                msg.To.Add(adr);
            }

            if (_ccAddressList != null)
            {
                foreach (System.Net.Mail.MailAddress adr in _ccAddressList)
                {
                    msg.CC.Add(adr);
                }
            }

            if (_bccAddressList != null)
            {
                foreach (System.Net.Mail.MailAddress adr in _bccAddressList)
                {
                    msg.Bcc.Add(adr);
                }
            }

            if (_attachmentsList != null)
            {
                foreach (System.Net.Mail.Attachment attch in _attachmentsList)
                {
                    msg.Attachments.Add(attch);
                }
            }

            if (!String.IsNullOrEmpty(_subject))
            {
                msg.Subject = _subject;
                msg.SubjectEncoding = System.Text.Encoding.UTF8;
            }

            if (!String.IsNullOrEmpty(_bodyText))
            {
                msg.Body = _bodyText;
            }

            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = _isHTMLBody;

            int index = 0;
            EmailServerModel server;
            bool isSent = false;

            while (index < _emailServerList.Count && isSent == false)
            {
                server = _emailServerList[index];                
                if (!server.UseDefaultCredentials && !string.IsNullOrEmpty(server.UserName) && !string.IsNullOrWhiteSpace(server.UserPswd))
                {
                    client.Credentials = new System.Net.NetworkCredential(server.UserName, server.UserPswd);
                }

                if (_fromAddress == null)
                {
                    msg.From = new MailAddress(server.UserName);
                }

                client.Port = server.EmailPort;
                client.Host = server.ServerIP;
                client.EnableSsl = server.EnableSSL;
                client.Timeout = _timeOut;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                try
                {
                    client.Send(msg);
                    isSent = true;
                }
                catch (Exception ex)
                {
                    if (index == _emailServerList.Count - 1)
                        throw ex;

                }

                index++;
            }
            return isSent;
        }

        #endregion
    }
}
