using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Radical_Ecart_UserManagementService.MongoDB_Products
{
    public class TransactionEntries
    {
        public bool SaveTransaction(TransactionDetails det)
        {
            try
            {
                var client = new MongoClient(@"mongodb+srv://weathersparkdbadmin:qwerty123Q@weathersparkcluster-vbcma.mongodb.net");
                var db = client.GetDatabase("RadicalECartProducts");
                var collection = db.GetCollection<TransactionDetails>("TransactionEntries");
                collection.InsertOneAsync(det);
                return true;
            }
            catch(System.Exception ex)
            {
                return false;
            }
        }
    }
}