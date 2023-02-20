namespace Sample;

public interface ILogger
{
    void WriteMessage(string message);
    void WriteWarning(string warning);
    void WriteError(string error);
}
