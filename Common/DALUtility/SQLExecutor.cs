using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Common
{
    public class SQLExecutor
    {
        private string connectionStr;
        private SqlTransaction trans = null;
        private SqlConnection conn = null;

        private List<SqlParameter> parameters;

        /// <summary>
        /// 赋给sql的参数，执行完一次命令自动清空
        /// </summary>
        private List<SqlParameter> Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        public SQLExecutor()
            : this(DALConfig.Instance.ConnectionStr)
        {

        }

        public SQLExecutor(string sqlConnectionStr)
        {
            try
            {
                conn = new SqlConnection(sqlConnectionStr);
                connectionStr = conn.ConnectionString;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(LogManager.LogFile.Exception, ex.Message);
            }
        }

        public SQLExecutor(SqlTransaction trans)
        {
            this.conn = trans.Connection;
            this.trans = trans;
            this.connectionStr = trans.Connection.ConnectionString;
        }

        #region 执行返回影响行数
        /// <summary>
        /// 执行命令返回影响行数
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(SqlCommand cmd)
        {
            try
            {
                if (this.trans == null)
                {
                    using (SqlConnection connection = new SqlConnection(this.connectionStr))
                    {
                        if (connection.State == System.Data.ConnectionState.Closed)
                        {
                            connection.Open();
                        }
                        cmd.Connection = connection;
                        AddParameters(cmd);
                        LogManager.WriteLog(LogManager.LogFile.SQL, cmd.CommandText);
                        return cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    if (this.trans.Connection == null)
                        throw new Exception("连接已经关闭");
                    if (this.trans.Connection.State == System.Data.ConnectionState.Closed)
                    {
                        this.trans.Connection.Open();
                    }
                    cmd.Connection = this.trans.Connection;
                    cmd.Transaction = this.trans;
                    AddParameters(cmd);
                    LogManager.WriteLog(LogManager.LogFile.SQL, cmd.CommandText);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql);
                return ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 执行返回单个值
        /// <summary>
        /// 执行命令返回单个值，可以通过Parameters添加参数
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public object ExecuteScalar(SqlCommand cmd)
        {
            try
            {
                if (this.trans == null)
                {
                    using (SqlConnection connection = new SqlConnection(this.connectionStr))
                    {
                        if (connection.State == System.Data.ConnectionState.Closed)
                        {
                            connection.Open();
                        }
                        cmd.Connection = connection;
                        AddParameters(cmd);
                        LogManager.WriteLog(LogManager.LogFile.SQL, cmd.CommandText);
                        return cmd.ExecuteScalar();
                    }
                }
                else
                {
                    if (this.trans.Connection == null)
                        throw new Exception("连接已经关闭");
                    if (this.trans.Connection.State == System.Data.ConnectionState.Closed)
                    {
                        this.trans.Connection.Open();
                    }
                    cmd.Connection = this.trans.Connection;
                    cmd.Transaction = this.trans;
                    AddParameters(cmd);
                    LogManager.WriteLog(LogManager.LogFile.SQL, cmd.CommandText);
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行SQL返回单个值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql);
                return ExecuteScalar(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 执行返回DataTable
        /// <summary>
        /// 执行命令返回DataTable，可以通过Parameters添加参数
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public DataTable ExecuteGetDataTable(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            try
            {
                if (this.trans == null)
                {
                    using (SqlConnection connection = new SqlConnection(this.connectionStr))
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }
                        cmd.Connection = connection;
                        AddParameters(cmd);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        LogManager.WriteLog(LogManager.LogFile.SQL, cmd.CommandText);
                        return dt;
                    }
                }
                else
                {
                    if (this.trans.Connection == null)
                        throw new Exception("连接已经关闭，无法执行SQL");
                    if (this.conn.State == ConnectionState.Closed)
                    {
                        this.conn.Open();
                    }
                    cmd.Connection = this.conn;
                    cmd.Transaction = this.trans;
                    AddParameters(cmd);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    LogManager.WriteLog(LogManager.LogFile.SQL, cmd.CommandText);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 执行SQL返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable ExecuteGetDataTable(string sql)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql);
                return ExecuteGetDataTable(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 执行返回DataSet

        /// <summary>
        /// 执行命令返回DataSet，可以通过Parameters添加参数
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public DataSet ExecuteGetDataSet(SqlCommand cmd)
        {
            DataSet ds = new DataSet();
            try
            {
                if (this.trans == null)
                {
                    using (SqlConnection connection = new SqlConnection(this.connectionStr))
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }
                        cmd.Connection = connection;
                        AddParameters(cmd);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(ds);
                        LogManager.WriteLog(LogManager.LogFile.SQL, cmd.CommandText);
                        return ds;
                    }
                }
                else
                {
                    if (this.trans.Connection == null)
                    {
                        throw new Exception("连接已经关闭，无法执行SQL");
                    }
                    if (this.conn.State == ConnectionState.Closed)
                        this.conn.Open();
                    cmd.Connection = this.conn;
                    cmd.Transaction = this.trans;
                    AddParameters(cmd);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                    LogManager.WriteLog(LogManager.LogFile.SQL, cmd.CommandText);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行SQL返回DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet ExecuteGetDataSet(string sql)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql);
                return ExecuteGetDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 执行返回SqlDataReader
        /// <summary>
        /// 执行命令返回SqlDataReader，可以通过Parameters添加参数
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(SqlCommand cmd)
        {
            try
            {
                if (this.trans == null)
                {
                    using (SqlConnection connection = new SqlConnection(this.connectionStr))
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }
                        cmd.Connection = connection;
                        AddParameters(cmd);
                        LogManager.WriteLog(LogManager.LogFile.SQL, cmd.CommandText);
                        return cmd.ExecuteReader();
                    }
                }
                else
                {
                    if (this.trans.Connection == null)
                    {
                        throw new Exception("连接已经关闭，无法执行SQL");
                    }
                    if (this.conn.State == ConnectionState.Closed)
                        this.conn.Open();
                    cmd.Connection = this.conn;
                    cmd.Transaction = this.trans;
                    AddParameters(cmd);
                    LogManager.WriteLog(LogManager.LogFile.SQL, cmd.CommandText);
                    return cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行SQL返回SqlDataReader
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(string sql)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql);
                return ExecuteReader(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 事务相关
        
        public SQLExecutor BeginTransaction()
        {
            return BeginTransaction(DALConfig.Instance.ConnectionStr);
        }

        public SQLExecutor BeginTransaction(string sqlConnectionStr)
        {
            this.conn = new SqlConnection(sqlConnectionStr);
            if (this.conn.State == ConnectionState.Closed)
                conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            SQLExecutor helper = new SQLExecutor(trans);
            return helper;
        }

        public void Commit()
        {
            if (this.trans != null)
                trans.Commit();
            else
                throw new Exception("未启用事务");
        }

        public void RollBack()
        {
            if (this.trans != null)
                trans.Rollback();
            else
                throw new Exception("未启用事务");
        }
        #endregion

        public void Close()
        {
            if (this.conn != null)
                this.conn.Close();
            else
                throw new Exception("连接未初始化");
        }


        public void Dispose()
        {
            if (this.trans != null)
                this.trans.Dispose();
            if (this.conn != null)
                this.conn.Dispose();
        }

        private void AddParameters(SqlCommand cmd)
        {
            if (parameters.Count > 0)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter item in parameters)
                {
                    cmd.Parameters.Add(item);
                }
                parameters.Clear();
            }
        }
    }
}
