using LogEntries;
using MongoDB.Driver;
using Neo4j.Driver.V1;
using Neo4jClient.Cypher;
using Radical_Ecart_UserManagementService.MongoDB_Products;
using Radical_Ecart_UserManagementService.Neo4j_User_Functionality;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Http;
//using Exception = System.Exception;

namespace Radical_Ecart_UserManagementService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
   [ServiceBehavior(AddressFilterMode=AddressFilterMode.Any)]
    public class UserManagementService : IUserManagementService
    {
        public string GetEmailAddress(string User)
        {
            try
            {
                return  new RegisterUser().GetEmailAddress(User);
            }
            catch(System.Exception ex)
            {
                StackFrame frame = null;
                int line = 0;
                LogData.GetFrameDetails(ex, out frame, out line);
                LogData.LogExceptionData(ex.Message, frame.GetFileName(), ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
                return "";
            }
        }
        public bool NotifyUser(NotifyUser obj)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("mailme.radicalecarts@gmail.com");
                mail.To.Add(obj.User);
                mail.Subject = "Notification mail - Radical ECart support team.";
                mail.Body = "You have subscribed for notification of the product "+obj.item+". We will notify once its available online. "
                    +"Thanks for your patience.";
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Port = 587; SmtpServer.EnableSsl = true;
                SmtpServer.Credentials = new System.Net.NetworkCredential("mailme.radicalecarts@gmail.com", "qwerty123@Q");
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

                SmtpServer.Send(mail);
                return true;
            }
            catch(System.Exception ex)
            {
                StackFrame frame = null;
                int line = 0;
                LogData.GetFrameDetails(ex, out frame, out line);
                LogData.LogExceptionData(ex.Message, frame.GetFileName(), ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
                return false;
            }
        }
        ProductDetails[] list = null;
        public ProductDetails[] GetProductDetails()
        {
            try
            {
                list = new GetProducts().GetAllProducts();
            }
            catch(System.Exception ex)
            {
                StackFrame frame = null;
                int line = 0;
                LogData.GetFrameDetails(ex, out frame, out line);
                LogData.LogExceptionData(ex.Message, frame.GetFileName(), ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return list;
        }
        public bool RemoveUser([FromBody] User u)
        {
            bool isRemoved = false;
            try
            {
                isRemoved = new RegisterUser().RemoveUser(u);
            }
            catch (System.Exception ex)
            {
                StackFrame frame = null;
                int line = 0;
                LogData.GetFrameDetails(ex, out frame, out line);
                LogData.LogExceptionData(ex.Message, frame.GetFileName(), ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return isRemoved;
        }
        public bool CheckMobileNumber([FromBody] string mobileNo)
        {
            bool isPresent = false;
            try
            {
                isPresent = new RegisterUser().CheckMobileNumber(mobileNo);
            }
            catch (System.Exception ex)
            {
                StackFrame frame = null;
                int line = 0;
                LogData.GetFrameDetails(ex, out frame, out line);
                LogData.LogExceptionData(ex.Message, frame.GetFileName(), ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return isPresent;
        }
        public string Login([FromBody] User member)
        {
            string ans = "";
            try
            {
                ans = new RegisterUser().Login(member);
            }
            catch(System.Exception ex)
            {
                StackFrame frame = null;
                int line = 0;
                LogData.GetFrameDetails(ex, out frame, out line);
                LogData.LogExceptionData(ex.Message, frame.GetFileName(), ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return ans;
        }
        public bool CheckUserId([FromBody] string userId)
        {
            bool isPresent = false;
            try
            {
                isPresent = new RegisterUser().CheckUserId(userId);
            }
            catch (System.Exception ex)
            {
                StackFrame frame = null;
                int line = 0;
                LogData.GetFrameDetails(ex, out frame, out line);
                LogData.LogExceptionData(ex.Message, frame.GetFileName(), ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return isPresent;
        }
        public User GetUserDetails(string userId)
        {
            try
            {
               return new RegisterUser().GetUserDetails(userId);
            }
            catch (System.Exception ex)
            {
                StackFrame frame = null;
                int line = 0;
                LogData.GetFrameDetails(ex, out frame, out line);
                LogData.LogExceptionData(ex.Message, frame.GetFileName(), ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return null;
        }
        public string EditDetails(User u)
        {
            try
            {
                return new RegisterUser().EditDetails(u);
            }
            catch (System.Exception ex)
            {
                StackFrame frame = null;
                int line = 0;
                LogData.GetFrameDetails(ex, out frame, out line);
                LogData.LogExceptionData(ex.Message, frame.GetFileName(), ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return "false" ;

        }
        public bool UpdatePassword(User user)
        {
            bool isUpdated = false;
            try
            {
                isUpdated = new RegisterUser().UpdatePassword(user);
            }
            catch (System.Exception ex)
            {
                isUpdated = false;
                StackFrame frame = null;
                int line = 0;
                LogData.GetFrameDetails(ex, out frame, out line);
                LogData.LogExceptionData(ex.Message, frame.GetFileName(), ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return isUpdated;
        }
        public bool RegisterUser([FromBody] User member)
        {
            bool isRegistered = false;
            try
            {
                isRegistered= new RegisterUser().InsertUser(member);                
            }
            catch(System.Exception ex)
            {
                isRegistered = false;
                StackFrame frame = null;
                int line = 0;
                LogData.GetFrameDetails(ex, out frame, out line);
                LogData.LogExceptionData(ex.Message,frame.GetFileName(),ex.InnerException!=null?ex.InnerException.Message:"No InnerException",line.ToString(),frame.GetMethod().Name);
            }
            return isRegistered;
        }
        public string CheckUserEmailAddress(string emailAddress)
        {
            string res = "";
            try
            {
                res = new RegisterUser().CheckUserEmailAddress(emailAddress);
            }
            catch (System.Exception ex)
            {
                res = "";
                StackFrame frame = null;
                int line = 0;
                LogData.GetFrameDetails(ex, out frame, out line);
                LogData.LogExceptionData(ex.Message, frame.GetFileName(), ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return res;
        }
        public bool SavePaymentTransaction(TransactionDetails det)
        {
            bool successSave = false;
            try
            {
                successSave = new TransactionEntries().SaveTransaction(det);
            }
            catch(System.Exception ex)
            {
                StackFrame frame = null;
                int line = 0;
                LogData.GetFrameDetails(ex, out frame, out line);
                LogData.LogExceptionData(ex.Message, frame.GetFileName(), ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return successSave;
        }
        public bool UpdateTrackingDetails(TrackingDetails det)
        {
            bool isUpdated = false;
            try
            {
                isUpdated = new TrackingEntries().UpdateTrackingDetails(det);
            }
            catch (System.Exception ex)
            {
                StackFrame frame = null;
                int line = 0;
                LogData.GetFrameDetails(ex, out frame, out line);
                LogData.LogExceptionData(ex.Message, frame.GetFileName(), ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return isUpdated;
        }
        public bool UpdateProducts(ProductDetails det)
        {
            bool isUpdated = false;
            try
            {
                isUpdated = new GetProducts().UpdateProduct(det);
            }
            catch (System.Exception ex)
            {
                StackFrame frame = null;
                int line = 0;
                LogData.GetFrameDetails(ex, out frame, out line);
                LogData.LogExceptionData(ex.Message, frame.GetFileName(), ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return isUpdated;
        }
        public TrackingDetails GetTrackingDetails(string id)
        {
            TrackingDetails det = null;
            try
            {
                det = new TrackingEntries().GetAllTrackingDetails(id);
            }
            catch (System.Exception ex)
            {
                StackFrame frame = null;
                int line = 0;
                LogData.GetFrameDetails(ex, out frame, out line);
                LogData.LogExceptionData(ex.Message, frame.GetFileName(), ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return det;
        }
        public bool EditTrackingDetails(TrackingDetails det)
        {
            bool res = false;
            try
            {
                res = new TrackingEntries().EditTrackingDates(det);
            }
            catch (System.Exception ex)
            {
                StackFrame frame = null;
                int line = 0;
                LogData.GetFrameDetails(ex, out frame, out line);
                LogData.LogExceptionData(ex.Message, frame.GetFileName(), ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return res;
        }
        public string[] GetPendingOrders(string det)
        {
            List<string> list = new List<string>();
            TrackingDetails[] res = null;
            try
            {
                res = new TrackingEntries().GetPendingOrders(det);
                if(res?.Count()>0)
                {
                    foreach(var item in res)
                    {
                        if(string.IsNullOrEmpty(item?.DeliveredDate))
                        {
                            list.Add(item.OrderId);
                        }
                    }
                }
                return list.ToArray();
            }
            catch (System.Exception ex)
            {
                StackFrame frame = null;
                int line = 0;
                LogData.GetFrameDetails(ex, out frame, out line);
                LogData.LogExceptionData(ex.Message, frame.GetFileName(), ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return list.ToArray() ;
        }
        public TrackingDetails[] GetCompletedOrders(string det)
        {
            List<TrackingDetails> newlist = new List<TrackingDetails>();
            List<string> list = new List<string>();
            TrackingDetails[] res = null;
            try
            {
                res = new TrackingEntries().GetPendingOrders(det);
                if (res?.Count() > 0)
                {
                    foreach (var item in res)
                    {
                        if (!string.IsNullOrEmpty(item?.DeliveredDate))
                        {
                            newlist.Add(item);
                        }
                    }
                }
                return newlist.ToArray();
            }
            catch (System.Exception ex)
            {
                StackFrame frame = null;
                int line = 0;
                LogData.GetFrameDetails(ex, out frame, out line);
                LogData.LogExceptionData(ex.Message, frame.GetFileName(), ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return newlist.ToArray();
        }
        public CorrespondingOrderedProducts GetCorrespondingOrderedProducts(string det)
        {
            try
            {
                var client = new MongoClient(@"mongodb+srv://weathersparkdbadmin:qwerty123Q@weathersparkcluster-vbcma.mongodb.net");
                var db = client.GetDatabase("RadicalECartProducts");
                var collection = db.GetCollection<CorrespondingOrderedProducts>("MappedTransactionWithProductOrder");
                return collection.FindAsync<CorrespondingOrderedProducts>(x=>x.OrderId==det).Result.FirstOrDefault();
            }
            catch(System.Exception ex)
            {
                return null;
            }
        }
        public bool AddCorrespondingOrderedProducts(CorrespondingOrderedProducts det)
        {
            try
            {
                var client = new MongoClient(@"mongodb+srv://weathersparkdbadmin:qwerty123Q@weathersparkcluster-vbcma.mongodb.net");
                var db = client.GetDatabase("RadicalECartProducts");
                var collection = db.GetCollection<CorrespondingOrderedProducts>("MappedTransactionWithProductOrder");
                collection.InsertOneAsync(det);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public TrackingDetails[] GetAllTrackingDetails()
        {
            try
            {
                var client = new MongoClient(@"mongodb+srv://weathersparkdbadmin:qwerty123Q@weathersparkcluster-vbcma.mongodb.net");
                var db = client.GetDatabase("RadicalECartProducts");
                var collection = db.GetCollection<TrackingDetails>("TrackingDetails");
              return collection.Find(_ => true).ToListAsync().Result.ToArray();
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        public CouponCode[] GetCouponCodes(string user)
        {
            try
            {
                var client = new MongoClient(@"mongodb+srv://weathersparkdbadmin:qwerty123Q@weathersparkcluster-vbcma.mongodb.net");
                var db = client.GetDatabase("RadicalECartProducts");
                var collection = db.GetCollection<CouponCode>("Coupons");
                var response= collection.Find(_ => true).ToListAsync().Result.ToArray();
                response = response.ToList().Where(x => x.Assign == user).ToArray();
                return response;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        public bool UpdateCouponCode(CouponCode det)
        {
            try
            {
                var client = new MongoClient(@"mongodb+srv://weathersparkdbadmin:qwerty123Q@weathersparkcluster-vbcma.mongodb.net");
                var db = client.GetDatabase("RadicalECartProducts");
                var collection = db.GetCollection<CouponCode>("Coupons");
                var existingRecords = collection.Find(_ => true).ToListAsync().Result.ToArray();
                var allow=existingRecords.Where(x => x.Assign == det.Assign && x.CouponName==det.CouponName).Select(y => y.MaxAllow).FirstOrDefault();
                var info = Builders<CouponCode>.Update.Set(x => x.MaxAllow, (int.Parse(allow)-1).ToString());
                collection.UpdateOne<CouponCode>(o => o.Assign == det.Assign && o.CouponName==det.CouponName, info);return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public TransactionDetails[] GetTransactionDetails()
        {
            try
            {
                var client = new MongoClient(@"mongodb+srv://weathersparkdbadmin:qwerty123Q@weathersparkcluster-vbcma.mongodb.net");
                var db = client.GetDatabase("RadicalECartProducts");
                var collection = db.GetCollection<TransactionDetails>("TransactionEntries");
                return collection.Find(x => true).ToList().ToArray();
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        public bool SaveComplaint(Complaint det)
        {
            try
            {
                var client = new MongoClient(@"mongodb+srv://weathersparkdbadmin:qwerty123Q@weathersparkcluster-vbcma.mongodb.net");
                var db = client.GetDatabase("RadicalECartProducts");
                var collection = db.GetCollection<Complaint>("Complaints");
                collection.InsertOneAsync(det);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
    }
}
