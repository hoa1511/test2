public interface ICollectable
{
    public void HandleCollectItem();
}

public interface ICollectAndRestorable
{
    public void HandleCollectItem(float amountToRestore);
}
