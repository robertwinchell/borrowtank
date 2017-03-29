
using System.Web;
namespace ASOL.HireThings.Service.Utils
{
    class SessionManager
    {
        public class Sessions
        {
            public const string UserID = "UserID";
            
        }

        public static object GetSessionValue(string sessionName, HttpContext context)
        {
            object retVal = null;
            retVal = context.Session[sessionName]; 

            return retVal;
        }

        public static void SetSessionValue(string sessionName, object value, HttpContext context)
        {
            context.Session[sessionName] = value; 
        }
    }
}
