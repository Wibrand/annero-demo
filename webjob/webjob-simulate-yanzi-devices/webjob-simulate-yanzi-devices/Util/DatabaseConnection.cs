using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webjob_simulate_yanzi_devices.Util
{
    class DatabaseConnection
    {
        private SqlConnection _sqlConnection;

        public DatabaseConnection(string connectionString)
        {
            var scsb = new SqlConnectionStringBuilder(connectionString)
            {
                MultipleActiveResultSets = true
            };

            _sqlConnection = new SqlConnection(scsb.ConnectionString);
        }

        public SqlConnection Connection { get { return _sqlConnection; } }
    }
}
