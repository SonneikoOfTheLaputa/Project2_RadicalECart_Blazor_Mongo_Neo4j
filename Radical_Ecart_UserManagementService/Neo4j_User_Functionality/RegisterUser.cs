using Neo4j.Driver.V1;
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
        public static readonly IDriver driver= GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "qwerty123@Q"),new Config { EncryptionLevel=EncryptionLevel.None});
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
        public Tuple<int,string> InsertUser(User member)
        {            
            string response = "";int c = 0;
            try
            {
                using (driver)
                {
                    using (var session = driver.Session())
                    {
                       
                        using(var scope=new System.Transactions.TransactionScope())
                        {
                            session.Run(
    "create (a:User{FirstName:'" + member.FirstName + "',LastName:'" + member.LastName + "'," +
    "Age:'" + member.Age + "',Email:'" + member.Email + "'," +
    "Address1:'" + member.Address1 + "',Address2:'" + member.Address2 + "'," +
    "City:'" + member.City + "',Country:'" + member.Country + "',PinCode:'" + member.PinCode + "'," +
    "AreaCode:'" + member.AreaCode + "',Mobile:'" + member.MobileNumber + "',UserId:'" + member.UserId + "'," +
    "Password:'" + member.Password + "',State:'" + member.State + "',Role:'NormalUser'})");



                            var str = "match (n:User),(a:User) where a.UserId='" + member.UserId + "'" +
                                " and n.Name='Admin' create (n)-[r:CONTROLS_USER]->(a)";
                         
                            session.Run(str);
                            session.Dispose();
                            driver.Dispose();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                response
                    = ex.Message;c = 0;
                LogData.GetFrameDetails(ex, out StackFrame frame, out int line);
                LogData.LogExceptionData(ex.Message, frame!=null &&!string.IsNullOrEmpty(frame.GetFileName())?frame.GetFileName():this.GetType().Name+".cs",
                    ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return new Tuple<int, string>(c,response);
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
                        isPresent = !string.IsNullOrEmpty(session.Run("match (n:User) where n.Userid='" + UserId+"' return n.UserId as UserId").
                            Single().Values["UserId"].ToString()) ? false : true;
                    }
                }
            }
            catch(System.Exception ex)
            {
                LogData.GetFrameDetails(ex, out StackFrame frame, out int line);
                LogData.LogExceptionData(ex.Message, frame.GetFileName(), ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return isPresent;
        }

        public bool Login(User member)
        {
            bool isValid = false;
            try
            {
                using(driver)
                {
                    using(var session=driver.Session())
                    {
                        isValid = !string.IsNullOrEmpty(session.Run("match (n:User) where n.UserId='" + member.UserId + "' and " +
                            "n.Password='" + Encrypt(member.Password) + "' return n.UserId as UserId").Single().
                            Values["UserId"].ToString()) ? true : false;
                    }
                }
            }
            catch (System.Exception ex)
            {
                LogData.GetFrameDetails(ex, out StackFrame frame, out int line);
                LogData.LogExceptionData(ex.Message, frame.GetFileName(), ex.InnerException != null ? ex.InnerException.Message : "No InnerException", line.ToString(), frame.GetMethod().Name);
            }
            return isValid;

        }

        //match (n:User {Name:'Admin'})-[r:CONTROLS_USER]->() delete r
    }
}