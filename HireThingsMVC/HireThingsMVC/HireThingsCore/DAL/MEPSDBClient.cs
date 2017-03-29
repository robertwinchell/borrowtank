using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Core
{
    public abstract class ASOLDBClient
    {
        protected abstract bool ExecuteSP(string spName, int timeOut);
        protected abstract bool ExecuteSP(string spName, ref Dictionary<string, object> parameters, int timeOut = 30);
        protected abstract bool ExecuteStatement(string statement, int timeOut = 30);
        protected abstract DataSet GetSPDataSet(string spName, int timeOut = 30);
        protected abstract DataSet GetSPDataSet(string spName, ref Dictionary<string, object> parameters, int timeOut = 30);
        protected abstract DataTable GetDataTable(string statement, int timeOut = 30);
        
        protected abstract DataTable GetSPDataTable(string spName, int timeOut = 30);
        protected abstract DataTable GetSPDataTable(string spName, ref Dictionary<string, object> parameters, int timeOut = 30);

        protected abstract object GetParameter(string name, System.Data.ParameterDirection direction, int dbType, int size = 0, object value = null);

        //Async
        protected abstract Task<bool> ExecuteSPAsync(string spName, CancellationToken cancelToken, Dictionary<string, object> parameters, int timeOut = 30);
        protected abstract Task<bool> ExecuteStatementAsync(string statement, CancellationToken cancelToken, int timeOut = 30);
        protected abstract Task<DataSet> GetSPDataSetAsync(string spName, CancellationToken cancelToken, Dictionary<string, object> parameters, int timeOut = 30);
        protected abstract Task<DataTable> GetSPDataTableAsync(string spName, CancellationToken cancelToken, Dictionary<string, object> parameters, int timeOut = 30);
        protected abstract Task<DataTable> GetDataTableAsync(string statement, CancellationToken cancelToken, int timeOut = 30);
        //protected abstract Task<DataSet> GetDataSetAsync(string statement, CancellationToken cancelToken, int timeOut = 30);                
    }
}
