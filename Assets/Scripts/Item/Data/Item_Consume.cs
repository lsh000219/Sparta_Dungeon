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
public class Item_Consume:ItemData
{
    [Header("Consumable")]
    public ItemDataConsumable[] consumables;
    
    public override void UseItem()
    {
        base.UseItem();
        for (int i = 0; i < consumables.Length; i++)
        {
            CharacterManager.Instance.Player.condition.uiCondition.health.curValue += consumables[i].value;
        }
        
    }
}