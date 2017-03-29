using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Core
{
    public abstract class AbstractSQLClient : ASOLDBClient
    {
        private string _connectionString;
        private const int _timeOut = 60;

        public AbstractSQLClient(string connStr)
        {
            _connectionString = connStr;
        }

        protected override bool ExecuteSP(string spName, int timeOut = _timeOut)
        {
            Dictionary<string, object> paramList = null;
            return ExecuteSP(spName, ref paramList, timeOut);
        }

        protected override bool ExecuteSP(string spName, ref Dictionary<string, object> parameters, int timeOut = _timeOut)
        {
            bool retVal = false;
            Exception ex = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = spName;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters.Values.ToArray());
                    cmd.CommandTimeout = timeOut;

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        if (parameters != null)
                        {
                            foreach (SqlParameter param in cmd.Parameters)
                            {
                                if (param.Direction == System.Data.ParameterDirection.Output || param.Direction == System.Data.ParameterDirection.InputOutput || param.Direction == System.Data.ParameterDirection.ReturnValue)
                                {
                                    ((SqlParameter)parameters[param.ParameterName]).Value = param.Value;
                                }
                            }
                        }

                        retVal = true;
                    }
                    catch (Exception e)
                    {
                        ex = e;
                    }
                    finally
                    {
                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }

                if (ex != null)
                {
                    throw ex;
                }

                return retVal;
            }
        }

        protected override bool ExecuteStatement(string statement, int timeOut = _timeOut)
        {
            bool retVal = false;
            Exception ex = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = statement;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandTimeout = timeOut;

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        retVal = true;
                    }
                    catch (Exception e)
                    {
                        ex = e;
                    }
                    finally
                    {
                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }

                if (ex != null)
                {
                    throw ex;
                }
            }

            return retVal;
        }

        protected override DataSet GetSPDataSet(string spName, int timeOut = _timeOut)
        {
            Dictionary<string, object> paramList = null;
            return GetSPDataSet(spName, ref paramList, timeOut);
        }

        protected override DataSet GetSPDataSet(string spName, ref Dictionary<string, object> parameters, int timeOut = _timeOut)
        {
            Exception ex = null;
            DataSet retVal = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = spName;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = timeOut;

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters.Values.ToArray());
                    }

                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    try
                    {
                        conn.Open();
                        adp.Fill(retVal);

                        if (parameters != null)
                        {
                            foreach (SqlParameter param in cmd.Parameters)
                            {
                                if (param.Direction == System.Data.ParameterDirection.Output || param.Direction == System.Data.ParameterDirection.InputOutput || param.Direction == System.Data.ParameterDirection.ReturnValue)
                                {
                                    ((SqlParameter)parameters[param.ParameterName]).Value = param.Value;
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        ex = e;
                    }
                    finally
                    {
                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }

                if (ex != null)
                {
                    throw ex;
                }
            }

            return retVal;
        }

        protected override DataTable GetDataTable(string statement, int timeOut = _timeOut)
        {
            Exception ex = null;
            DataTable dt = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = statement;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandTimeout = timeOut;

                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    try
                    {
                        conn.Open();
                        adp.Fill(dt);
                    }
                    catch (Exception e)
                    {
                        ex = e;
                    }
                    finally
                    {
                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }

                if (ex != null)
                {
                    throw ex;
                }
            }

            return dt;
        }



        protected override DataTable GetSPDataTable(string spName, int timeOut = 30)
        {
            Dictionary<string, object> paramList = null;
            return GetSPDataTable(spName, ref paramList, timeOut);
        }

        protected override DataTable GetSPDataTable(string spName, ref Dictionary<string, object> parameters, int timeOut = 30)
        {
            Exception ex = null;
            DataTable retVal = new DataTable();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = spName;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = timeOut;

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters.Values.ToArray());
                    }

                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    try
                    {
                        conn.Open();
                        adp.Fill(retVal);

                        if (parameters != null)
                        {
                            foreach (SqlParameter param in cmd.Parameters)
                            {
                                if (param.Direction == System.Data.ParameterDirection.Output || param.Direction == System.Data.ParameterDirection.InputOutput || param.Direction == System.Data.ParameterDirection.ReturnValue)
                                {
                                    ((SqlParameter)parameters[param.ParameterName]).Value = param.Value;
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        ex = e;
                    }
                    finally
                    {
                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }

                if (ex != null)
                {
                    throw ex;
                }
            }

            return retVal;
        }

        protected override object GetParameter(string name, System.Data.ParameterDirection direction, int dbType, int size = 0, object value = null)
        {
            SqlParameter param = new SqlParameter();

            param.Direction = direction;
            param.SqlDbType = (System.Data.SqlDbType)dbType;

            if (size > 0)
                param.Size = size;

            if (value != null)
                param.Value = value;

            param.ParameterName = name;

            return param;
        }




        #region async section

        protected override async Task<bool> ExecuteSPAsync(string spName, CancellationToken cancellationToken, Dictionary<string, object> parameters, int timeOut = _timeOut)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = spName;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters.Values.ToArray());
                    }

                    cmd.CommandTimeout = timeOut;


                    await conn.OpenAsync(cancellationToken);
                    await cmd.ExecuteNonQueryAsync(cancellationToken);

                    if (parameters != null)

                        foreach (SqlParameter param in cmd.Parameters)
                        {
                            if (param.Direction == System.Data.ParameterDirection.Output || param.Direction == System.Data.ParameterDirection.InputOutput || param.Direction == System.Data.ParameterDirection.ReturnValue)
                            {
                                ((SqlParameter)parameters[param.ParameterName]).Value = param.Value;
                            }
                        }
                }
            }

            return true;
        }

        protected override async Task<bool> ExecuteStatementAsync(string statement, CancellationToken cancellationToken, int timeOut = _timeOut)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = statement;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandTimeout = timeOut;

                    await conn.OpenAsync(cancellationToken);
                    await cmd.ExecuteNonQueryAsync(cancellationToken);

                }

            }

            return true;
        }

        protected Task<DataSet> GetSPDataSetAsync(string spName, CancellationToken cancellationToken, int timeOut = _timeOut)
        {
            return GetSPDataSetAsync(spName, cancellationToken, null, timeOut);
        }

        protected override async Task<DataSet> GetSPDataSetAsync(string spName, CancellationToken cancellationToken, Dictionary<string, object> parameters, int timeOut = _timeOut)
        {
            DataSet ds = new DataSet();
            DataTable dt = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = spName;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = timeOut;

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters.Values.ToArray());
                    }
                    await conn.OpenAsync(cancellationToken);

                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    using (SqlDataReader sqlReader = await cmd.ExecuteReaderAsync(cancellationToken))
                    {
                        while (true)
                        {
                            if (!sqlReader.IsClosed)
                            {
                                dt = new DataTable();
                                dt.Load(sqlReader, LoadOption.OverwriteChanges);
                                ds.Tables.Add(dt);
                            }
                            else
                                break;

                        }

                        if (parameters != null)
                        {
                            foreach (SqlParameter param in cmd.Parameters)
                            {
                                if (param.Direction == System.Data.ParameterDirection.Output || param.Direction == System.Data.ParameterDirection.InputOutput || param.Direction == System.Data.ParameterDirection.ReturnValue)
                                {
                                    ((SqlParameter)parameters[param.ParameterName]).Value = param.Value;
                                }
                            }
                        }
                    }

                }


            }

            return ds;
        }

        protected override async Task<DataTable> GetDataTableAsync(string statement, CancellationToken cancellationToken, int timeOut = _timeOut)
        {
            DataTable dt = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = statement;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandTimeout = timeOut;

                    await conn.OpenAsync(cancellationToken);

                    using (SqlDataReader sqlReader = await cmd.ExecuteReaderAsync(cancellationToken))
                    {
                        if (sqlReader.HasRows)
                        {
                            dt = new DataTable();
                            dt.Load(sqlReader);
                        }
                    }

                }
            }

            return dt;
        }

        protected Task<DataTable> GetSPDataTableAsync(string spName, CancellationToken cancellationToken, int timeOut = 30)
        {

            return GetSPDataTableAsync(spName, cancellationToken, null, timeOut);
        }

        protected override async Task<DataTable> GetSPDataTableAsync(string spName, CancellationToken cancellationToken, Dictionary<string, object> parameters, int timeOut = 30)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = spName;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = timeOut;

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters.Values.ToArray());
                    }

                    await conn.OpenAsync(cancellationToken);

                    using (SqlDataReader sqlReader = await cmd.ExecuteReaderAsync(cancellationToken))
                    {
                        // if (sqlReader.HasRows)
                        // {
                        dt = new DataTable();
                        dt.Load(sqlReader);
                        // }
                    }


                    if (parameters != null)
                    {
                        foreach (SqlParameter param in cmd.Parameters)
                        {
                            if (param.Direction == System.Data.ParameterDirection.Output || param.Direction == System.Data.ParameterDirection.InputOutput || param.Direction == System.Data.ParameterDirection.ReturnValue)
                            {
                                ((SqlParameter)parameters[param.ParameterName]).Value = param.Value;
                            }
                        }
                    }
                }
            }

            return dt;
        }
        #endregion
    }
}

