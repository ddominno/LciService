using System;
using System.Data;
using System.Data.SqlClient;


namespace DriscTAF.Utilities
{
    public class DbConnect : IDisposable
    {
        /// <summary>when the connection is created it is held here</summary>
        private SqlConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Magenic.MaqsFramework.BaseDatabaseTest.DatabaseConnectionWrapper" /> class
        /// </summary>
        /// <param name="connectionString">The base database connection string</param>
        public DbConnect(string connectionString)
        {
            this.connection = this.SetupDataBaseConnection(connectionString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Magenic.MaqsFramework.BaseDatabaseTest.DatabaseConnectionWrapper" /> class
        /// </summary>
        /// <param name="setupDataBaseConnectionOverride">A function that returns the database connection</param>
        public DbConnect(Func<SqlConnection> setupDataBaseConnectionOverride)
        {
            this.connection = setupDataBaseConnectionOverride();
        }

        /// <summary>Dispose of the database connection</summary>
        public virtual void Dispose()
        {
            if (this.connection == null || this.connection.State != ConnectionState.Open)
                return;
            this.connection.Close();
        }

        /// <summary>Runs a query</summary>
        /// <param name="query"> the SQL query the test provides</param>
        /// <returns>A DataTable containing the results of the query</returns>
        /// <returns>The result data table</returns>
        /// <example>
        /// <code source="../DatabaseUnitTests/DatabaseUnitTestsWithWrapper.cs" region="QueryAndGetTableData" lang="C#" />
        /// </example>
        public virtual DataTable QueryAndGetDataTable(string query)
        {
            SqlCommand sqlCommand = new SqlCommand(query, this.connection);
            sqlCommand.CommandTimeout = 1000;
            DataTable dataTable = new DataTable();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            dataTable.Load((IDataReader)sqlDataReader);
            return dataTable;
        }

        /// <summary>Run non query SQL</summary>
        /// <param name="nonQuery">SQL that does not return query results</param>
        /// <returns>The number of affected rows</returns>
        /// <example>
        /// <code source="../DatabaseUnitTests/DatabaseUnitTestsWithWrapper.cs" region="NonQuerySqlCall" lang="C#" />
        /// </example>
        public virtual int NonQueryAndGetRowsAffected(string nonQuery)
        {
            SqlCommand sqlCommand = new SqlCommand(nonQuery, this.connection);
            int queryTimeout = 1000;
            sqlCommand.CommandTimeout = queryTimeout;
            return sqlCommand.ExecuteNonQuery();
        }

        /// <summary>Checks if a stored procedure exists</summary>
        /// <param name="procedureName"> the procedure name</param>
        /// <returns>A boolean representing whether the procedure was found</returns>
        /// <example>
        /// <code source="../DatabaseUnitTests/DatabaseUnitTestsWithWrapper.cs" region="ProcedureExists" lang="C#" />
        /// </example>
        public virtual bool CheckForProcedure(string procedureName)
        {
            SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM sysobjects WHERE name=@sprocName", this.connection);
            sqlCommand.Parameters.Add("@sprocName", SqlDbType.VarChar, 100).Value = (object)procedureName;
            int queryTimeout = 1000;
            sqlCommand.CommandTimeout = queryTimeout;
            return (int)sqlCommand.ExecuteScalar() > 0;
        }

        /// <summary>
        /// Runs a procedure that does an action and returns the number of elements affected
        /// </summary>
        /// <param name="procedureName">The procedure name</param>
        /// <param name="parameters">The procedure parameters</param>
        /// <returns>The number of rows affected</returns>
        /// <example>
        /// <code source="../DatabaseUnitTests/DatabaseUnitTestsWithWrapper.cs" region="RunActionProcedure" lang="C#" />
        /// </example>
        public virtual int RunActionProcedure(string procedureName, params SqlParameter[] parameters)
        {
            SqlCommand sqlCommand = new SqlCommand(procedureName, this.connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = 1000;
            foreach (SqlParameter parameter in parameters)
                sqlCommand.Parameters.Add(parameter);
            return sqlCommand.ExecuteNonQuery();
        }

        /// <summary>Runs a procedure that returns query results</summary>
        /// <param name="procedureName">The procedure name</param>
        /// <param name="parameters">The procedure parameters</param>
        /// <returns>A DataTable containing the results of the procedure</returns>
        /// <example>
        /// <code source="../DatabaseUnitTests/DatabaseUnitTestsWithWrapper.cs" region="RunQueryProcedure" lang="C#" />
        /// </example>
        public virtual DataTable RunQueryProcedure(string procedureName, params SqlParameter[] parameters)
        {
            SqlCommand sqlCommand = new SqlCommand(procedureName, this.connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = 1000;
            foreach (SqlParameter parameter in parameters)
                sqlCommand.Parameters.Add(parameter);
            DataTable dataTable = new DataTable();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            dataTable.Load((IDataReader)sqlDataReader);
            return dataTable;
        }

        /// <summary>
        /// Default database connection setup - Override this function to create your own connection
        /// </summary>
        /// <param name="connectionString">The database connection string</param>
        /// <returns>The http client</returns>
        protected virtual SqlConnection SetupDataBaseConnection(string connectionString)
        {
            SqlConnection sqlConnection = new SqlConnection();
            string str = connectionString;
            sqlConnection.ConnectionString = str;
            sqlConnection.Open();
            return sqlConnection;
        }
    }
}