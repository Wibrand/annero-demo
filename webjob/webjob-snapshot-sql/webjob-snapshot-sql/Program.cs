using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webjob_snapshot_sql
{
    class Program
    {


        static void Main()
        {
            var dbConnection = new DatabaseConnection(MyConfigurationManager.GetConnectionString("SqlConnectionString", MyConfigurationManager.ConnectionType.AzureSqlDatabase));

            var currentDate = DateTime.Now;
            var currentMinute = currentDate.Minute;
            var minute10 = currentMinute / 10 * 10;

            var dateToUse = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day,
                                         currentDate.Hour, minute10, 0);


            dbConnection.Connection.Execute("dbo.CreateSnapshotOfCurrentStatusOfAllSamples", commandType: System.Data.CommandType.StoredProcedure,
               param: new
               {
                   SampleCollectedTime = dateToUse
               });
        }
    }
}
