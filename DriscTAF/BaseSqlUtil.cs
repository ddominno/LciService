using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriscTAF
{
    using System.Data;
    using System.Threading;

    using DriscTAF.Utilities;

    public class BaseSqlUtil
    {
        /// <summary>
        /// The connection string
        /// </summary>
        protected string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSQLUtil" /> class. 
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        public BaseSqlUtil(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Builds a new database connection
        /// </summary>
        public DbConnect DatabaseWrapper => new DbConnect(this.connectionString);

        /// <summary>
        /// Performs a SQL query
        /// </summary>
        /// <param name="statement">The query statement</param>
        /// <returns>The data table response</returns>
        public DataTable DoQuery(string statement)
        {
            DataTable res;

            try
            {
                res = this.DatabaseWrapper.QueryAndGetDataTable(statement);
            }
            catch (Exception)
            {
                // wait a bit and retry after an exception
                Thread.Sleep(3000);

                res = this.DatabaseWrapper.QueryAndGetDataTable(statement);
            }

            return res;
        }
    }
}
