namespace Application.UseCases;

public abstract class Handler<TRequest> : IHandler<TRequest>
    where TRequest : BaseRequest
{
    private Handler<TRequest>? next;

    public Handler<TRequest> SetNext(Handler<TRequest> next) => this.next = next;

    public virtual void ProcessRequest(TRequest request)
    {
        if (request.Logs.Any(l => l.Status == Domain.Enums.LogStatus.Fatal))
            return;

        next?.ProcessRequest(request);
    }
}