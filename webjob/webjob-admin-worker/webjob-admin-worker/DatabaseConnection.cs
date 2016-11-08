using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webjob_admin_worker
{
    class DatabaseConnection
    {
        private SqlConnection _sqlConnection;

        public DatabaseConnection(string connectionString)
        {
            Console.WriteLine($"**** {connectionString}");

            var scsb = new SqlConnectionStringBuilder(connectionString)
            {
                MultipleActiveResultSets = true
            };

            _sqlConnection = new SqlConnection(scsb.ConnectionString);
            _sqlConnection.Open();
        }

        public SqlConnection Connection { get { return _sqlConnection; } }

    }
}
