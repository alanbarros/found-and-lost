using Application.Boundaries;

namespace Application.UseCases;

public interface IUseCase<TRequest, TOutput>
    where TRequest : BaseRequest
    where TOutput : class
{
    void Execute(TRequest request, IOutputPort<TOutput> outputPort);
}

public interface IUseCase<TOutput>
    where TOutput : class
{
    void Execute(IOutputPort<TOutput> outputPort);
}

public interface IUseCase<TRequest, TOutput, TException>
    where TRequest : class
    where TOutput : class
    where TException : Exception
{
    void Execute(TRequest request, IOutputPort<TOutput, TException> outputPort);
}