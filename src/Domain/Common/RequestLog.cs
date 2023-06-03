using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Common
{
    public class RequestLog
    {
        public string Message { get; set; }
        public LogStatus Status { get; set; }

        public RequestLog(string message, LogStatus status)
        {
            Message = message;
            Status = status;
        }

        public static RequestLog CreateInfo(string message) => new RequestLog(message, LogStatus.Info);
        public static RequestLog CreateDebug(string message) => new RequestLog(message, LogStatus.Debug);
        public static RequestLog CreateWarning(string message) => new RequestLog(message, LogStatus.Warning);
        public static RequestLog CreateFatal(string message) => new RequestLog(message, LogStatus.Fatal);
    }
}