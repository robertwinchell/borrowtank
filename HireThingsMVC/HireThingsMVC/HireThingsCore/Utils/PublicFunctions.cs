using ASOL.HireThings.Model;
using System;


namespace ASOL.HireThings.Core
{
   public class PublicFunctions
    {
        private static string _connStr = "";
        private static object _lock = new object();

        public static string DBConnectionString
        {
            get
            {
                lock (_lock)
                {
                    if (string.IsNullOrEmpty(_connStr))
                    {
                        _connStr = System.Configuration.ConfigurationManager.ConnectionStrings["HireThingsConnStr"].ToString();
                    }
                }

                return _connStr;
            }
        }

        #region DBNULL
        public static Object ConvertDBNullToNull(object input)
        {
            return (input == DBNull.Value) ? null : input;
        }

        public static object ConvertNULL(object input, object defaultVal, Constant.NullCheckType check = Constant.NullCheckType.DBNull)
        {
            switch (check)
            {
                case Constant.NullCheckType.NULL:
                    return (input == null) ? defaultVal : input;
                case Constant.NullCheckType.DBNull:
                    return (input == DBNull.Value) ? defaultVal : input;
                case Constant.NullCheckType.Both:
                    return (input == null || input == DBNull.Value) ? defaultVal : input;
            }

            return input;
        }

        public static int ConvertNULL(object input, int defaultVal, Constant.NullCheckType check = Constant.NullCheckType.DBNull)
        {
            switch (check)
            {
                case Constant.NullCheckType.NULL:
                    return (input == null) ? defaultVal : (int)input;
                case Constant.NullCheckType.DBNull:
                    return (input == DBNull.Value) ? defaultVal : (int)input;
                case Constant.NullCheckType.Both:
                    return (input == null || input == DBNull.Value) ? defaultVal : (int)input;
            }

            return (int)input;
        }

        public static Int16 ConvertNULL(object input, Int16 defaultVal, Constant.NullCheckType check = Constant.NullCheckType.DBNull)
        {
            switch (check)
            {
                case Constant.NullCheckType.NULL:
                    return (input == null) ? defaultVal : (Int16)input;
                case Constant.NullCheckType.DBNull:
                    return (input == DBNull.Value) ? defaultVal : (Int16)input;
                case Constant.NullCheckType.Both:
                    return (input == null || input == DBNull.Value) ? defaultVal : (Int16)input;
            }

            return (Int16)input;
        }


        public static Int64 ConvertNULL(object input, Int64 defaultVal, Constant.NullCheckType check = Constant.NullCheckType.DBNull)
        {
            switch (check)
            {
                case Constant.NullCheckType.NULL:
                    return (input == null) ? defaultVal : (Int64)input;
                case Constant.NullCheckType.DBNull:
                    return (input == DBNull.Value) ? defaultVal : Convert.ToInt64(input);
                case Constant.NullCheckType.Both:
                    return (input == null || input == DBNull.Value) ? defaultVal : (Int64)input;
            }

            return (Int64)input;
        }

        public static Decimal ConvertNULL(object input, Decimal defaultVal, Constant.NullCheckType check = Constant.NullCheckType.DBNull)
        {
            switch (check)
            {
                case Constant.NullCheckType.NULL:
                    return (input == null) ? defaultVal : (Decimal)input;
                case Constant.NullCheckType.DBNull:
                    return (input == DBNull.Value) ? defaultVal : (Decimal)input;
                case Constant.NullCheckType.Both:
                    return (input == null || input == DBNull.Value) ? defaultVal : (Decimal)input;
            }

            return (Decimal)input;
        }

        public static byte ConvertNULL(object input, byte defaultVal, Constant.NullCheckType check = Constant.NullCheckType.DBNull)
        {
            switch (check)
            {
                case Constant.NullCheckType.NULL:
                    return (input == null) ? defaultVal : (byte)input;
                case Constant.NullCheckType.DBNull:
                    return (input == DBNull.Value) ? defaultVal : (byte)input;
                case Constant.NullCheckType.Both:
                    return (input == null || input == DBNull.Value) ? defaultVal : (byte)input;
            }

            return (byte)input;
        }

        public static string ConvertNULL(object input, string defaultVal, Constant.NullCheckType check = Constant.NullCheckType.DBNull)
        {
            switch (check)
            {
                case Constant.NullCheckType.NULL:
                    return (input == null) ? defaultVal : (string)input;
                case Constant.NullCheckType.DBNull:
                    return (input == DBNull.Value) ? defaultVal : (string)input;
                case Constant.NullCheckType.Both:
                    return (input == null || input == DBNull.Value) ? defaultVal : (string)input;
            }

            return (string)input;
        }

        public static bool ConvertNULL(object input, bool defaultVal, Constant.NullCheckType check = Constant.NullCheckType.DBNull)
        {
            switch (check)
            {
                case Constant.NullCheckType.NULL:
                    return (input == null) ? defaultVal : (bool)input;
                case Constant.NullCheckType.DBNull:
                    return (input == DBNull.Value) ? defaultVal : (bool)input;
                case Constant.NullCheckType.Both:
                    return (input == null || input == DBNull.Value) ? defaultVal : (bool)input;
            }

            return (bool)input;
        }

        public static DateTime ConvertNULL(object input, DateTime defaultVal, Constant.NullCheckType check = Constant.NullCheckType.DBNull)
        {
            switch (check)
            {
                case Constant.NullCheckType.NULL:
                    return (input == null) ? defaultVal : (DateTime)input;
                case Constant.NullCheckType.DBNull:
                    return (input == DBNull.Value) ? defaultVal : (DateTime)input;
                case Constant.NullCheckType.Both:
                    return (input == null || input == DBNull.Value) ? defaultVal : (DateTime)input;
            }

            return (DateTime)input;
        }

        public static DateTime? ConvertNULL(object input, DateTime? defaultVal, Constant.NullCheckType check = Constant.NullCheckType.DBNull)
        {
            switch (check)
            {
                case Constant.NullCheckType.NULL:
                    return (input == null) ? defaultVal : (DateTime?)input;
                case Constant.NullCheckType.DBNull:
                    return (input == DBNull.Value) ? defaultVal : (DateTime?)input;
                case Constant.NullCheckType.Both:
                    return (input == null || input == DBNull.Value) ? defaultVal : (DateTime?)input;
            }

            return (DateTime?)input;
        }

        


        public static object ConvertNulltoDBNull(object input)
        {
            if (input == null)
            {
                return DBNull.Value;
            }
            else{ return input; }
            //return (input == null) ? DBNull.Value : input;
        }

        public static object ConvertNulltoDBNull(long input)
        {
            return (input > 0) ? (object)input : DBNull.Value;
        }

        #endregion


        #region Global functions

        public static void LogUserActvity(IUserLogActvityModel model)
        {
            AccountDAL _accDal = new AccountDAL();
            _accDal.LogUserActvity(model);
        }

        
        #endregion
    }
}

