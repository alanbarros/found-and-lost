using Application.Boundaries;

namespace Application.UseCases;

public interface IUseCase<TOutput>
    where TOutput : class
{
    void Execute(IOutputPort<TOutput> outputPort);
}

public interface IUseCase<TInput, TOutput>
    where TOutput : class
{
    void Execute(TInput input, IOutputPort<TOutput> outputPort);
}

public interface IUseCase<TRequest, TOutput, TException>
    where TRequest : BaseRequest
    where TOutput : class
    where TException : Exception
{
    void Execute(TRequest request, IOutputPort<TOutput, TException> outputPort);
}