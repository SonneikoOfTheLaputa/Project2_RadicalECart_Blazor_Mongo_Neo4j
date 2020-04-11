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
        bool Login(User user);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
    UriTemplate = "/CheckUserId", RequestFormat = WebMessageFormat.Json,
    BodyStyle = WebMessageBodyStyle.Bare)]
        bool CheckUserId(string userId);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/RegisterUser",RequestFormat =WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        Tuple<int,string> RegisterUser([FromBody] User member);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
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
        public string State { get; set; }
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
