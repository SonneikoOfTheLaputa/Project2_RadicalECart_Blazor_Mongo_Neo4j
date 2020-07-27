using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Radical_Ecart_UserManagementService.MongoDB_Products
{
    public class TrackingEntries
    {
        public bool UpdateTrackingDetails(TrackingDetails det)
        {
            try
            {
                var client = new MongoClient(@"mongodb+srv://weathersparkdbadmin:qwerty123Q@weathersparkcluster-vbcma.mongodb.net");
                var db = client.GetDatabase("RadicalECartProducts");
                var collection = db.GetCollection<TrackingDetails>("TrackingDetails");
                collection.InsertOneAsync(det);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }

        }
        public TrackingDetails GetAllTrackingDetails(string id)
        {
            try
            {
                var client = new MongoClient(@"mongodb+srv://weathersparkdbadmin:qwerty123Q@weathersparkcluster-vbcma.mongodb.net");
                var db = client.GetDatabase("RadicalECartProducts");
                var collection = db.GetCollection<TrackingDetails>("TrackingDetails");
               return collection.FindAsync<TrackingDetails>(x => x.OrderId == id).Result.FirstOrDefault();
             
            }
            catch (System.Exception ex)
            {
                return null;
            }

        }
        public bool EditTrackingDates(TrackingDetails det)
        {
            try
            {
                var client = new MongoClient(@"mongodb+srv://weathersparkdbadmin:qwerty123Q@weathersparkcluster-vbcma.mongodb.net");
                var db = client.GetDatabase("RadicalECartProducts");
                var collection = db.GetCollection<TrackingDetails>("TrackingDetails");
                if(det.ColumnToBeUpdated.Contains("InPackingDate"))
                {
                    var info = Builders<TrackingDetails>.Update.Set(x => x.InPackingDate, det.InPackingDate);
                    collection.UpdateOne<TrackingDetails>(o => o.OrderId == det.OrderId, info);
                }
                else if (det.ColumnToBeUpdated.Contains("InLocalShopDate"))
                {
                    var info = Builders<TrackingDetails>.Update.Set(x => x.InLocalShopDate, det.InLocalShopDate);
                    collection.UpdateOne<TrackingDetails>(o => o.OrderId == det.OrderId, info);
                }
                else if (det.ColumnToBeUpdated.Contains("DeliveredDate"))
                {
                    var info = Builders<TrackingDetails>.Update.Set(x => x.DeliveredDate, det.DeliveredDate);
                    collection.UpdateOne<TrackingDetails>(o => o.OrderId == det.OrderId, info);
                }
                else if (det.ColumnToBeUpdated.Contains("ShippedDate"))
                {
                    var info = Builders<TrackingDetails>.Update.Set(x => x.ShippedDate, det.ShippedDate);
                    collection.UpdateOne<TrackingDetails>(o => o.OrderId == det.OrderId, info);
                }

                return true;

            }
            catch (System.Exception ex)
            {
                return false;
            }

        }

        public TrackingDetails[] GetPendingOrders(string det)
        {
            try
            {
                var client = new MongoClient(@"mongodb+srv://weathersparkdbadmin:qwerty123Q@weathersparkcluster-vbcma.mongodb.net");
                var db = client.GetDatabase("RadicalECartProducts");
                var collection = db.GetCollection<TrackingDetails>("TrackingDetails");
                return collection.FindAsync<TrackingDetails>(x => x.UserId == det).Result.ToList().ToArray();
            }
            catch (System.Exception ex)
            {
                return null;
            }

        }
    }
}