
using System;
namespace ASOL.HireThings.Model
{
    public abstract class BaseModel: Object, IBaseModel
    {
        public long UserId { get; set; }
        public int  CPage { get; set; }
        public int TPage { get; set; }
        public long TRows { get; set; }
        public double TCount { get; set; }
        public string PSize { get; set; }
        public string ColumnName { get; set; }
        public int SortType { get; set; }


        public IPageInfo GetPageInfo(int cpage, int tpage, long trows,string psize,long userid, string columnname="",int sorttype=-1)
        {
            var sdasd = this.CPage;
            
            IPageInfo pInfo = new PageInfo()
            {
                CPage = cpage,
                TPage = tpage,
                TRows = trows,
                PSize=(psize==null)?"10":psize,
                UserId= (userid<=0)?0:userid,
                ColumnName=columnname,
                SortType=sorttype


            };

            return pInfo;
        }
        
    }

    public class ErrorMessage 
    {
        public const string RequiredMessage = "is required.";
        public const string RequiredMessagePlural = "are required.";
        public const string NumberMessage = "accepts numbers only.";
        public const string AlphaNumericMessage = "Special Characters are not allowed.";
        public const string LetterMessage = "should accept only letters.";
        public const string EmailMessage = "is invalid.";
        public const string DecimalMessage = "max value can be upto 4 decimal places only.";
        public const string MacAdressMessage = "must be XX:XX:XX:XX:XX:XX";
        public const string DEANoMessage = "must be XXNNNNNNN e.g AS9432042.";
        public const string PhoneandFaxMessage = "must be (NNN) NNN-NNNN";
        public const string ZipCodeMessage = "invalid (Note: Zip code can either be 5 digits or 9 digits)";
        public const string TinyIntRange = "must be between 0-255.";
        public const string IPAddressMessage = "IP Address is required.";
    }

    public class RegularExpression
    {
        public const string NumberRegularExpression = @"^[0-9]+$";
        public const string AlphaNumericExceptSpechialCharRegularExpression = @"[^';]+";
        public const string AlphaNumericRegularExpression = @"^(?:[a-zA-Z0-9]+\s?)+$";
        public const string AlphaNumericRegularExpressionWithDash = @"^(?:[a-zA-Z0-9-]+\s?)+$";
        public const string AlphaNumericRegularExpressionWithAND = @"^(?:[a-zA-Z0-9&]+\s?)+$";
        public const string AlphaNumericRegularExpressionWithSpecialCharacters = @"^(?:[a-zA-Z0-9,<.\-_()]+\s?)+$";
        public const string AlphaNumericAndQuestionMarkRegularExpression = @"^(?:[a-zA-Z0-9?]+\s?)+$";
        public const string LetterRegularExpression = @"^[a-zA-Z]+$";
        public const string DecimalRegularExpression = @"^(0|-?\d{0,5}(\.\d{1,4})?)$";
        public const string MacAdressRegularExpression = @"^([a-fA-F0-9]{2}[:][a-fA-F0-9]{2}[:][a-fA-F0-9]{2}[:][a-fA-F0-9]{2}[:][a-fA-F0-9]{2}[:][a-fA-F0-9]{2})$";
        public const string IpAddressRegularExpression = @"^([0-9]{1,3}[.][0-9]{1,3}[.][0-9]{1,3}[.][0-9]{1,3})$";
        public const string DEANoRegularExpression = @"^([A-Za-z]{2}[0-9]{7})$";
        public const string PhoneandFaxRegularExpression = @"^(\([0-9]{3}\) |[0-9]{3})[0-9]{3}-[0-9]{4}$";
        public const string ZipCodeRegularExpression = @"[0-9]{5}([0-9]{4})?";
    }

    public enum ReadingSampleType
    {
        Organization = 1,
        Facility = 2,
        Device = 4
    }


}

