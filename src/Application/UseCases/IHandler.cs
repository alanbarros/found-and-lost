namespace Application.UseCases;

public interface IHandler<TRequest> where TRequest : BaseRequest
{
    void ProcessRequest(TRequest request);
}