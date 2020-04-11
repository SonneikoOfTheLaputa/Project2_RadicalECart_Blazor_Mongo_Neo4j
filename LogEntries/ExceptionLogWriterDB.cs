using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using System.IO;

namespace LogEntries
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
    public class ExceptionLogWriterDB:DbContext
    {
        public ExceptionLogWriterDB() : base() { }
        public DbSet<Exception> ExceptionData { get; set; }
    }
}
