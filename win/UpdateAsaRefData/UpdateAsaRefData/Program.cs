using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace UpdateAsaRefData
{
    class Program
    {
        private static SqlConnection _sqlConnection;

        static void Main(string[] args)
        {

            var scsb = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["SqlDatabase"].ConnectionString);

            _sqlConnection = new SqlConnection(scsb.ConnectionString);
            _sqlConnection.Open();

            var assets = _sqlConnection.Query<Asset>("dbo.GetChangesInAssetCollection", commandType: CommandType.StoredProcedure).ToList();

            if(assets.Count > 0)
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["Storage"].ConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("asa-refdata");                
                CloudBlockBlob blob = container.GetBlockBlobReference(string.Format("assets/{0:yyy-MM-dd'/'HH-mm}/asset.json", DateTime.UtcNow));
                StringBuilder sb = new StringBuilder();

                foreach (var asset in assets)
                {
                    var jsonasset = JsonConvert.SerializeObject(asset);
                    sb.Append(jsonasset + "\n");
                }

                blob.UploadText(sb.ToString());

            }

        }
    }
}
