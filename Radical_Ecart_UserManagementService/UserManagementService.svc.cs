using LogEntries;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public bool abc()
        {
            return true;
        }
       public bool RegisterUser([FromBody] User member)
        {
            bool isUserRegistered = false;
            try
            {
               var s= new RegisterUser().InsertUser(member);
            }
            catch(System.Exception ex)
            {
                StackFrame frame = null;
                int line = 0;
                LogData.GetFrameDetails(ex, out frame, out line);
                LogData.LogExceptionData(ex.Message,frame.GetFileName(),ex.InnerException!=null?ex.InnerException.Message:"No InnerException",line.ToString(),frame.GetMethod().Name);
            }
            return isUserRegistered;
        }
    }
}
