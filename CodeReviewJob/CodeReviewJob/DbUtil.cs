using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeReviewJob
{
    public static class DbUtil
    {
        public static IDbConnection GetConnection()
        {
            return new SqlConnection(AppConfig.ConnectionString);
        }
    }
}
