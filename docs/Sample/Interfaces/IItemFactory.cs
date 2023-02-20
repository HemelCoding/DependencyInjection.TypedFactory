namespace Sample;

public interface IItemFactory
{
    IItemVm Create(Guid id);
}
