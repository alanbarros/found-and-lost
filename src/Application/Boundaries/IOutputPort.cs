namespace Application.Boundaries;

public interface IOutputPort<TOutput>
{
    void Standard(TOutput output);

    void Fail();

    void NotFound();
}

public interface IOutputPort<TOutput, TException> : IOutputPort<TOutput>
    where TException : Exception
{
    void Fail(TException exception);
}