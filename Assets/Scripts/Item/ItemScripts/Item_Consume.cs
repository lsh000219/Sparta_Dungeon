using UnityEngine;
 
public enum ConsumableType
{
    Health,
    
}
[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Item_Consume", menuName = "Item/Item_Consume")]
public class Item_Consume:ItemData  //소모하여 체력에 영향을 끼치는 아이템들
{
    [Header("Consumable")]
    public ItemDataConsumable[] consumables;
    
    public override void UseItem() //부모의 UseItem 구현
    {
        base.UseItem();
        for (int i = 0; i < consumables.Length; i++)
        {
            CharacterManager.Instance.Player.condition.uiCondition.health.curValue += consumables[i].value;
        }
        
    }
}