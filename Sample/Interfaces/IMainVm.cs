namespace Sample;

public interface IMainVm
{
    void NewItem();
    IEnumerable<IItemVm> Items { get; }
}
