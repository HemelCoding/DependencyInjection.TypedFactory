namespace Sample;

public class Logger : ILogger
{
    public void WriteError(string error)
    {
        Console.Error.WriteLine(error);
    }

    public void WriteMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void WriteWarning(string warning)
    {
        Console.WriteLine(warning);
    }
}