namespace Sample;

internal class MainVm : IMainVm
{
    private readonly IItemFactory itemFactory;
    private readonly ILogger logger;
    private readonly List<IItemVm> items = new();

    public MainVm(IItemFactory itemFactory, ILogger logger)
    {
        this.itemFactory = itemFactory;
        this.logger = logger;
    }

    public void NewItem()
    {
        var itemId = Guid.NewGuid();
        items.Add(itemFactory.Create(itemId));
        logger.WriteMessage($"Added item {itemId}.");
    }

    public IEnumerable<IItemVm> Items => items.AsEnumerable();
}
