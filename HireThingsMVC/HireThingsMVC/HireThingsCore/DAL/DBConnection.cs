using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace ASOL.HireThings.Core
{
    public class DBConnection : AbstractSQLClient
    {
        public DBConnection() : base(ASOL.HireThings.Core.PublicFunctions.DBConnectionString) { }
        public DBConnection(string connStr) : base(connStr){}
    }
}
