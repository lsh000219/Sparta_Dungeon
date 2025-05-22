public class Item_Useable : ItemData
{
    public override void UseItem()
    {
        base.UseItem();
        ItemEffect();
    }

    public virtual void ItemEffect(){}
}