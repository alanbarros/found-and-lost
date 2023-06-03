using Domain.Common;

namespace Application.UseCases;

public abstract class BaseRequest
{
    public List<RequestLog> Logs { get; private set; } = new List<RequestLog>();

    protected void AddInfo(string message) => Logs.Add(RequestLog.CreateInfo(message));
    protected void AddWarning(string message) => Logs.Add(RequestLog.CreateWarning(message));
    protected void AddDebug(string message) => Logs.Add(RequestLog.CreateDebug(message));
    protected void AddFatal(string message) => Logs.Add(RequestLog.CreateFatal(message));
}