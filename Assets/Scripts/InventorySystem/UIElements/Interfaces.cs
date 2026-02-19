public interface ICanBePicked
{
    void PickedUp();
    ItemBase GetItem();
}

public interface IPickUp
{
    void PickUp(ICanBePicked item);
}

public interface IConsume
{
    void Use(ConsumableItem item);
}
