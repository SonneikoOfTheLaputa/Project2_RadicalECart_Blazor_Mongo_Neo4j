using Neo4j.Driver.V1;
using Neo4jClient;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Radical_Ecart_UserManagementService
{
    public class RegisterUser
    {
        private readonly IDriver driver = GraphDatabase.Driver("bolt://localhost:11002", AuthTokens.Basic("neo4j", "qwerty123@Q"), new Config { EncryptionLevel = EncryptionLevel.None });
        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public string GetEmailAddress(string User)
        {
            string mail = "";
            try
            {
                using (driver)
                {
                    using (var session = driver.Session())
                    {
                        mail = session.Run("match (n:User) where n.UserId='" + User + "' return n.Email as Email").
                              Single().Values["Email"].ToString();
                    }
                }
            }
            catch(System.Exception ex)
            {
                if (ex.Message == "Sequence contains no elements")
                    mail = "";
                else
                {
                    LogData.GetFrameDetails(ex, out StackFrame frame, out int line);
                    LogData.LogExceptionData(ex.Message, string.IsNullOrEmpty(frame.GetFileName()) ? "" : frame.GetFileName(),
                        ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
                }
            }
            return mail;
        }
        public bool UpdatePassword(User user)
        {
            //            MATCH(n { name: 'Andy' })
            //SET n.age = toString(n.age)
            //RETURN n.name, n.age
            bool isUpdated = false;
            try
            {
                using (driver)
                {
                    using (var session = driver.Session())
                    {
                        string command1 = "MATCH (n:User { UserId: '" + user.UserId + "' }) " +
                              "RETURN n.Password as OldPassword";
                        string EncryptedOldPassword = session.Run(command1).
                              Single().Values["OldPassword"].ToString();
                        string command = "MATCH (n:User { UserId: '" + user.UserId + "' }) SET n.Password" +
                            " = '" + Encrypt(user.Password) + "' " +
                            "RETURN n.Password as NewPassword";
                        string EncryptedNewPassword = session.Run(command).
                               Single().Values["NewPassword"].ToString();
                        if (EncryptedOldPassword != EncryptedNewPassword)
                        {
                            isUpdated = true;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                if (ex.Message == "Sequence contains no elements")
                    isUpdated = false;
                else
                {
                    LogData.GetFrameDetails(ex, out StackFrame frame, out int line);
                    LogData.LogExceptionData(ex.Message, string.IsNullOrEmpty(frame.GetFileName()) ? "" : frame.GetFileName(),
                        ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
                }
            }
            return isUpdated;
        }
        public string EditDetails(User u)
        {
            bool isProceed = false;
            try
            {
                using(driver)
                {
                    using(var session=driver.Session())
                    {
                        var mobilecmd = "match (n:User{Mobile:'"
                            +u.MobileNumber+"'}) return n.UserId as UserId";
                        var emailcmd = "match (n:User{Email:'"
                            + u.Email + "'}) return n.UserId as UserId";
                       var mobResponse= session.Run(mobilecmd);
                        var emailResponse = session.Run(emailcmd);
                        try
                        {
                            var value = mobResponse.Single().Values["UserId"].ToString();
                            if (value == u.UserId)
                                isProceed = true;
                            else
                            { isProceed = false;
                                return "Mobile number already taken. Please try with another number."; }
                        }
                        catch
                        {
                            isProceed = true;
                        }
                        try
                        {
                            var value = emailResponse.Single().Values["UserId"].ToString();
                            if (value == u.UserId)
                                isProceed = true;
                            else
                            {
                                isProceed = false; return "Email address already taken. Please try with another email address.";
                            }
                        }
                        catch
                        {
                            isProceed = true;
                        }
                        if (isProceed) { 
                        var cmd = "match (n:User{UserId:'" + u.UserId + "'}) " +
                            "set n.Age='" + u.Age + "', n.PinCode='" + u.PinCode + "'," +
                            "n.FirstName='" + u.FirstName + "'," +
                            "n.LastName='" + u.LastName + "',n.City='" + u.City + "'," +
                            "n.Country='"+u.Country+"',n.AreaCode='"+u.AreaCode+"'," +
                            "n.Password='" +Encrypt( u.Password) + "',n.Email='" + u.Email + "'," +
                            "n.Address1='" +u.Address1+"',n.Address2='"+u.Address2+"'," +
                            " n.Mobile = '" + u.MobileNumber+"'";
                        session.Run(cmd);
                        return "true";
                        }
                    }
                }
            }
            catch(System.Exception ex)
            {
                LogData.GetFrameDetails(ex, out StackFrame frame, out int line);
                LogData.LogExceptionData(ex.Message, string.IsNullOrEmpty(frame.GetFileName()) ? "" : frame.GetFileName(),
                    ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
           
            }return "false";
        }
        public User GetUserDetails(string id)
        {
            try
            {
                using(driver)
                {
                    using(var session=driver.Session())
                    {
                        var cmd = "match (n:User{UserId:'" + id + "'}) return n.UserId as UserId," +
                            "n.Email as Email,n.Age as Age,n.Address1 as Address1,n.Address2 as Address2," +
                            "n.FirstName as Fname,n.LastName as Lname,n.Mobile as Phone,n.AreaCode as AreaCode," +
                            "n.PinCode as PinCode,n.City as City,n.Country as Country";
                        var response = session.Run(cmd).
                              Single();
                        return new User
                        {
                            Age = response.Values["Age"].ToString(),
                            UserId = response.Values["UserId"].ToString(),
                            Email = response.Values["Email"].ToString(),
                            Address1 = response.Values["Address1"].ToString(),
                            Address2 = response.Values["Address2"].ToString(),
                            FirstName = response.Values["Fname"].ToString(),
                            LastName = response.Values["Lname"].ToString(),
                            MobileNumber = response.Values["Phone"].ToString(),
                            AreaCode = response.Values["AreaCode"].ToString(),
                            PinCode = response.Values["PinCode"].ToString(),
                            City = response.Values["City"].ToString(),
                            Country = response.Values["Country"].ToString(),
                        };
                    }
                }
            }
            catch (System.Exception ex)
            {
                LogData.GetFrameDetails(ex, out StackFrame frame, out int line);
                LogData.LogExceptionData(ex.Message, string.IsNullOrEmpty(frame.GetFileName()) ? "" : frame.GetFileName(),
                    ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);

            }
            return null ;
        }
        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public bool InsertUser(User member)
        {
            bool isRegistered = true;
            try
            {
                using (driver)
                {
                    using (var session = driver.Session())
                    {
                        session.Run(
"create (a:User{FirstName:'" + member.FirstName + "',LastName:'" + member.LastName + "'," +
"Age:'" + member.Age + "',Email:'" + member.Email + "'," +
"Address1:'" + member.Address1 + "',Address2:'" + member.Address2 + "'," +
"City:'" + member.City + "',Country:'" + member.Country + "',PinCode:'" + member.PinCode + "'," +
"AreaCode:'" + member.AreaCode + "',Mobile:'" + member.MobileNumber + "',UserId:'" + member.UserId + "'," +
"Password:'" + Encrypt(member.Password) + "',Role:'NormalUser'})");

                        isRegistered = MapUserToAdmin(member.UserId);
                    }
                }
            }
            catch (System.Exception ex)
            {
                isRegistered = false;
                LogData.GetFrameDetails(ex, out StackFrame frame, out int line);
                LogData.LogExceptionData(ex.Message, frame != null && !string.IsNullOrEmpty(frame.GetFileName()) ? frame.GetFileName() : this.GetType().Name + ".cs",
                    ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return isRegistered;
        }
        public string CheckUserEmailAddress(string emailAddress)
        {
            string res = "";
            try
            {
                using (driver)
                {
                    using (var session = driver.Session())
                    {
                        res = session.Run("match (n:User) where n.Email='" + emailAddress + "' return n.UserId as UserId").
                              Single().Values["UserId"].ToString();
                    }
                }
            }
            catch (System.Exception ex)
            {
                if (ex.Message == "Sequence contains no elements")
                    res = "";
                else
                {
                    LogData.GetFrameDetails(ex, out StackFrame frame, out int line);
                    LogData.LogExceptionData(ex.Message, string.IsNullOrEmpty(frame.GetFileName()) ? "" : frame.GetFileName(),
                        ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
                }
            }
            return res;
        }
        private bool MapUserToAdmin(string userId)
        {
            bool isMapped = false;
            try
            {
                using (driver)
                {
                    using (var session = driver.Session())
                    {
                        var str = "match (n:User),(a:User) where a.UserId='" + userId + "'" +
                            " and n.Name='Admin' create (n)-[r:CONTROLS_USER]->(a)";

                        session.Run(str);
                        isMapped = true;
                    }
                }

            }
            catch (System.Exception ex)
            {
                isMapped = false;
                LogData.GetFrameDetails(ex, out StackFrame frame, out int line);
                LogData.LogExceptionData(ex.Message, frame != null && !string.IsNullOrEmpty(frame.GetFileName()) ? frame.GetFileName() : this.GetType().Name + ".cs",
                            ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return isMapped;
        }
        public bool CheckUserId(string UserId)
        {
            bool isPresent = false;
            try
            {
                using (driver)
                {
                    using (var session = driver.Session())
                    {
                        isPresent = !string.IsNullOrEmpty(session.Run("match (n:User) where n.UserId='" + UserId + "' return n.UserId as UserId").
                            Single().Values["UserId"].ToString()) ? true : false;
                    }
                }
            }
            catch (System.Exception ex)
            {
                if (ex.Message == "Sequence contains no elements")
                    isPresent = false;
                else
                {
                    LogData.GetFrameDetails(ex, out StackFrame frame, out int line);
                    LogData.LogExceptionData(ex.Message, string.IsNullOrEmpty(frame.GetFileName()) ? "" : frame.GetFileName(),
                        ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
                }
            }
            return isPresent;
        }

        public string Login(User member)
        {
            string ans = "";
            try
            {
                using (driver)
                {
                    using (var session = driver.Session())
                    {
                        ans = session.Run("match (n:User) where n.Email='" + member.Email + "' and " +
                            "n.Password='" + Encrypt(member.Password) + "' return n.UserId as UserId").Single().
                            Values["UserId"].ToString();
                    }
                }
            }
            catch (System.Exception ex)
            {
                LogData.GetFrameDetails(ex, out StackFrame frame, out int line);
                LogData.LogExceptionData(ex.Message, string.IsNullOrEmpty(frame.GetFileName()) ? "" : frame.GetFileName(),
                     ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return ans;

        }

        public bool RemoveUser(User u)
        {
            bool isRemoved = false;
            try
            {
                //MATCH (n:User { UserId: 'Sylar' }) detach DELETE n
                using (driver)
                {
                    using (var session = driver.Session())
                    {
                        session.Run("" +
                              "match (n:User { UserId:'" + u.UserId + "' ,Password:'" + Encrypt(u.Password) + "'}) " +
                              "detach delete n");
                        isRemoved = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                if (ex.Message == "Sequence contains no elements")
                    isRemoved = false;
                else
                {
                    LogData.GetFrameDetails(ex, out StackFrame frame, out int line);
                    LogData.LogExceptionData(ex.Message, string.IsNullOrEmpty(frame.GetFileName()) ? "" : frame.GetFileName(),
                       ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
                }
            }
            return isRemoved;
        }
        public bool CheckMobileNumber(string number)
        {
            bool isPresent = false;
            try
            {
                using (driver)
                {
                    using (var session = driver.Session())
                    {
                        isPresent = !string.IsNullOrEmpty(session.Run("match (n:User) where n.Mobile='" + number + "' " +
                            "return n.Mobile as Mobile").
                            Single().Values["Mobile"].ToString()) ? true : false;
                    }
                }
            }
            catch (System.Exception ex)
            {
                if (ex.Message == "Sequence contains no elements")
                    isPresent = false;
                else
                {
                    LogData.GetFrameDetails(ex, out StackFrame frame, out int line);
                    LogData.LogExceptionData(ex.Message, string.IsNullOrEmpty(frame.GetFileName()) ? "" : frame.GetFileName(),
                       ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
                }
            }
            return isPresent;
        }

        //match (n:User {Name:'Admin'})-[r:CONTROLS_USER]->() delete r
    }
}