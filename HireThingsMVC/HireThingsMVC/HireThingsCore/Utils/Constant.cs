namespace ASOL.HireThings.Core
{
    public class Constant
    {
        public enum NullCheckType
        {
            DBNull, NULL, Both
        }
        
            public const string RoleId = "RoleId";

        public const string MainController = "Main";
        public const string MainAction = "Index";
        public const string HomeController = "Home";
        public const string HomeAction = "Index";
        public const string LoginController = "Account";
        public const string LoginAction = "Login";
        // public const string PassPhrase = "Hello";
        public const string EmailId = "EmailId";
        public const string HeaderTitle = "HeaderTitle";
        public const string QuerySuccess = "QuerySuccess";
        public const string AuthObj = "AuthObj";
        public const string FormTitle = "FormTitle";
        public const string CustomSuccessMessage = "CustomSuccessMessage";
        public const string CustomValidationErrorMessage = " Error : Errors in below fields.";
        public const string ObjectModel = "ObjectModel";
        public const string AllowAllUser = "AllowAllUser";
        public const string TimeZoneId = "TimeZoneId";
        public const string UserId = "UserId";
        public const string SystemId = "SystemId";

        public enum EmailType
        {
            UserSignup = 1,
            ChangePassword = 2,
            ForgotPassword = 3
        }


    }
    public class Sessions {
        public const string DDLPageSize = "DDLPageSize";
        public const string LogoutType = "LogoutType";
    }
}
