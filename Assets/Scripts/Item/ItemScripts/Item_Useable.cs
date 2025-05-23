
[System.Serializable]
public class ItemDataUseable
{
    public float value;
}
public class Item_Useable : ItemData  // 특수한 효과를 지닌 아이템
{
    public ItemDataUseable itemDataUseable;
    public override void UseItem() //부모의 UseItem 구현
    {
        base.UseItem();
        ItemEffect();
    }

    public virtual void ItemEffect(){} //자식 클래스에서 아이템 효과 메소드 호출
}