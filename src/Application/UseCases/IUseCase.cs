using Application.Boundaries;

namespace Application.UseCases;

public interface IUseCase<TOutput>
    where TOutput : class
{
    void Execute(IOutputPort<TOutput> outputPort);
}

public interface IUseCase<TRequest, TOutput>
    where TOutput : class
    where TRequest : BaseRequest
{
    void Execute(TRequest input, IOutputPort<TOutput> outputPort);
}

public interface IUseCase<TRequest, TOutput, TException>
    where TRequest : BaseRequest
    where TOutput : class
    where TException : Exception
{
    void Execute(TRequest request, IOutputPort<TOutput, TException> outputPort);
}

public interface IDeleteUseCase<TRequest, TException>
    where TRequest : BaseRequest
    where TException : Exception
{
    void Execute(TRequest request, IOutputPort<string, Exception> outputPort);
}