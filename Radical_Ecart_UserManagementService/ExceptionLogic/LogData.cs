using LogEntries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using ObjectContext = System.Data.Entity.Core.Objects.ObjectContext;
using ObjectParameter = System.Data.Entity.Core.Objects.ObjectParameter;

namespace Radical_Ecart_UserManagementService
{
    public class Exception
    {
        public string ExceptionMessage { get; set; }
        public string FileName { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int LineNumber { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string MethodName { get; set; }
    }
    public class ExceptionLogWriterDB : DbContext
    {
        public ExceptionLogWriterDB() : base() { }
        public DbSet<Exception> ExceptionData { get; set; }
        public virtual int LogException(params string[] s)
        {
             var CreatedDateTime = new SqlParameter("createddatetime", DateTime.Now);
            var ExceptionMessage = new SqlParameter("exmessage", s[0]);
            var FileName = new SqlParameter("filename",s[1]);
            var InnerExceptionMessage = new SqlParameter("inner",s[2]);
            var LineNumber = new SqlParameter("lineno",int.Parse(s[3]));
            var MethodName = new SqlParameter("methodname",s[4]);
            var id = new SqlParameter("id",Guid.NewGuid().ToString());

            return new ObjectContext(ConfigurationManager
                .ConnectionStrings["ExceptionDatabaseEntities"].ConnectionString).ExecuteStoreCommand
                ("Exec ExceptionLog @exmessage,@filename,@createddatetime,@lineno,@inner,@methodname,@id",
                ExceptionMessage, FileName, CreatedDateTime, LineNumber,
          InnerExceptionMessage, MethodName, id);

        }
    }
    public static class LogData
    {
        public static void GetFrameDetails(System.Exception ex,out StackFrame frame,out int line)
        {
            frame = null;
            line = 0;
            var st = new StackTrace(ex, true);
             frame = st.GetFrame(0);
             line = frame.GetFileLineNumber();
        }
        public static void LogExceptionData(params string[] s)
        {
            using(var ctx=new ExceptionLogWriterDB())
            {
                var val = ctx.LogException(s);
                if(val!=1)
                {

                }
            }
        }
    }
}