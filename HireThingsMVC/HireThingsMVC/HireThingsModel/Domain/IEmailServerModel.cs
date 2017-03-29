using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface IEmailServerModel:IBaseModel
    {
         long? ServerId { get; set; }
         string ServerName { get; set; }
         string ServerIP { get; set; }
         Int32 EmailPort { get; set; }
         string UserName { get; set; }
         string UserPswd { get; set; }
         bool UseDefaultCredentials { get; set; }
         bool EnableSSL { get; set; }
         bool IsActive { get; set; }
         DateTime UpdateDate { get; set; }
         long UpdateUserID { get; set; }
         DateTime AddDate { get; set; }
         long AddUserID { get; set; }        
         Byte Priority { get; set; }
         List<EmailServerModel> SysEmailServers { get; set; }

    }
}
