
[System.Serializable]
public class ItemDataUseable
{
    public float value;
}
public class Item_Useable : ItemData
{
    public ItemDataUseable itemDataUseable;
    public override void UseItem()
    {
        base.UseItem();
        ItemEffect();
    }

    public virtual void ItemEffect(){}
}