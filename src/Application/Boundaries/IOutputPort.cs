namespace Application.Boundaries;

public interface IOutputPort<TOutput>
    where TOutput : class
{
    void Standard(TOutput output);

    void Fail();
}

public interface IOutputPort<TOutput, TException>
    where TOutput : class
    where TException : Exception
{
    void Standard(TOutput output);

    void Fail(TException exception);
}