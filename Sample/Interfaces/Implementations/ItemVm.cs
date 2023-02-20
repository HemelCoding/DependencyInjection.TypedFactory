namespace Sample;

public class ItemVm : IItemVm
{
    public ItemVm(ILogger logger, Guid id)
    {
        Id = id;

        logger.WriteMessage($"Item {id} created.");
    }

    public Guid Id { get; }
}
