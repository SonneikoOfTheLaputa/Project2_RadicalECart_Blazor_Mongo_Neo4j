using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Radical_Ecart_UserManagementService.Neo4j_User_Functionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Http;

namespace Radical_Ecart_UserManagementService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IUserManagementService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/Login", RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        string Login(User user);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "/UpdatePassword", RequestFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare)]
        bool UpdatePassword(User user);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/CheckUserEmailAddress", RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        string CheckUserEmailAddress(string emailAddress);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "/CheckMobileNumber", RequestFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare)]
        bool CheckMobileNumber(string mobileNo);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
    UriTemplate = "/CheckUserId", RequestFormat = WebMessageFormat.Json,
    BodyStyle = WebMessageBodyStyle.Bare)]
        bool CheckUserId(string userId);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/RegisterUser",RequestFormat =WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        bool RegisterUser([FromBody] User member);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/RemoveUser", RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]

        bool RemoveUser(User u);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
               UriTemplate = "/GetUserDetails", RequestFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        User GetUserDetails(string userId);
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
               UriTemplate = "/EditDetails", RequestFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]

        string EditDetails(User u);
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
               UriTemplate = "/GetProductDetails", RequestFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]

        ProductDetails[] GetProductDetails();


        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
               UriTemplate = "/NotifyUser", RequestFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        bool NotifyUser(NotifyUser obj);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
               UriTemplate = "/GetEmailAddress", RequestFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        string GetEmailAddress(string User);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
               UriTemplate = "/SavePaymentTransaction", RequestFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        bool SavePaymentTransaction(TransactionDetails det);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "/UpdateTrackingDetails", RequestFormat = WebMessageFormat.Json,
              BodyStyle = WebMessageBodyStyle.Bare)]
        bool UpdateTrackingDetails(TrackingDetails det);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
             UriTemplate = "/UpdateProducts", RequestFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare)]
        bool UpdateProducts(ProductDetails det);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
             UriTemplate = "/GetTrackingDetails", RequestFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare)]
        TrackingDetails GetTrackingDetails(string user);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/EditTrackingDetails", RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        bool EditTrackingDetails(TrackingDetails det);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "/GetPendingOrders", RequestFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare)]
        string[] GetPendingOrders(string det);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "/GetCompletedOrders", RequestFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare)]
        TrackingDetails[] GetCompletedOrders(string det);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
          UriTemplate = "/GetCorrespondingOrderedProducts", RequestFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare)]
        CorrespondingOrderedProducts GetCorrespondingOrderedProducts(string det);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
         UriTemplate = "/AddCorrespondingOrderedProducts", RequestFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare)]
        bool AddCorrespondingOrderedProducts(CorrespondingOrderedProducts det);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
         UriTemplate = "/GetAllTrackingDetails", RequestFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare)]
        TrackingDetails[] GetAllTrackingDetails();


        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "/GetCouponCodes", RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
        CouponCode[] GetCouponCodes(string user);
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "/UpdateCouponCode", RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
        bool UpdateCouponCode(CouponCode det);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
       UriTemplate = "/GetTransactionDetails", RequestFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare)]
        TransactionDetails[] GetTransactionDetails();


        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
       UriTemplate = "/SaveComplaint", RequestFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare)]
        bool SaveComplaint(Complaint det);
        // TODO: Add your service operations here
    }
    [DataContract]
    [BsonIgnoreExtraElements]
    public class CouponCode
    {
        [DataMember] public string CouponName { get; set; }
        [DataMember] public string CouponValidFrom { get; set; }
        [DataMember] public string CouponValidTo { get; set; }
        [DataMember] public string AmountOff { get; set; }
        [DataMember] public string MaxAllow { get; set; }
        [DataMember] public string Assign { get; set; }
    }
    [DataContract]
    [BsonIgnoreExtraElements]
    public class Complaint
    {
        [DataMember] public string Subject { get; set; }
        [DataMember] public string complaintId { get; set; }
        [DataMember] public string Message { get; set; }
        [DataMember] public string Status { get; set; }
        [DataMember] public string Note { get; set; }
    }
    [DataContract]
    [BsonIgnoreExtraElements]
    public class CorrespondingOrderedProducts
    {
        [DataMember] public string OrderId { get; set; }
        [DataMember] public string Products { get; set; }
        [DataMember] public string Total { get; set; }

    }
    [DataContract]
    [BsonIgnoreExtraElements]
    public class TrackingDetails
    {[DataMember] public string OrderId { get; set; }
        [DataMember] public string UserId { get; set; }
    [DataMember] public string OrderedDate { get; set; }
    [DataMember] public string DeliveredDate { get; set; }
    [DataMember] public string ShippedDate { get; set; }
        [DataMember] public string InPackingDate { get; set; }
        [DataMember] public string InLocalShopDate { get; set; }
        [DataMember] public string ColumnToBeUpdated { get; set; }
        [DataMember] public string Address { get; set; }
        [DataMember] public string BillingAddress { get; set; }
        [DataMember] public string shippingAddress { get; set; }
    }
    [DataContract]
    [BsonIgnoreExtraElements]
    public class TransactionDetails
    {
        [DataMember]
        public string TransactionId { get; set; }
        [DataMember]
        public string EmailAddress { get; set; }
        [DataMember]
        public string PaymentDate { get; set; }
        [DataMember]
        public string Amount { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public string Quantity { get; set; }
    }
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class ProductDetails
    {
        [DataMember]
        public ObjectId Id { get; set; }
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public string Warranty { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public byte[] image { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string HyperLink { get; set; }
        [DataMember]
        public string Quantity { get; set; }
        [DataMember]
        public string Price { get; set; }
        [DataMember]
        public string Seller { get; set; }
        public string Chosen { get; set; }
    }
    public class User
    {
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Age { get; set; }
        [DataMember]
        public string Address1 { get; set; }
        [DataMember]
        public string Address2 { get; set; }
        [DataMember]
        public string City { get; set; }
       
        [DataMember]
        public string PinCode { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string MobileNumber { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string AreaCode { get; set; }
    }
}
