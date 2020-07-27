using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Radical_Ecart_UserManagementService.MongoDB_Products
{
    public class GetProducts
    {
        public ProductDetails[] GetAllProducts()
        {
            var client = new MongoClient(@"mongodb+srv://weathersparkdbadmin:qwerty123Q@weathersparkcluster-vbcma.mongodb.net");
            var db = client.GetDatabase("RadicalECartProducts");
            var collection = db.GetCollection<ProductDetails>("RadicalECartProducts");
            var data= collection.Find(_ => true).ToListAsync().Result.ToArray();
            return data;

        }
        public bool UpdateProduct(ProductDetails det)
        {
            var client = new MongoClient(@"mongodb+srv://weathersparkdbadmin:qwerty123Q@weathersparkcluster-vbcma.mongodb.net");
            var db = client.GetDatabase("RadicalECartProducts");
            var collection = db.GetCollection<ProductDetails>("RadicalECartProducts");
            var info = Builders<ProductDetails>.Update.Set(x => x.Quantity, det.Quantity);
            collection.UpdateOne<ProductDetails>(o => o.Name == det.Name, info
                );
            return true;
        }
    }
}