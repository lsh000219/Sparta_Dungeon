using UnityEngine;

[System.Serializable]
public class ItemDataEquipable
{
    public float jumpValue;
    public float speenValue;
}
[CreateAssetMenu(fileName = "Item_Equipable", menuName = "Item/Item_Equipable")]
public class Item_Equipable:ItemData
{
    [SerializeField]
    private ItemDataEquipable equipable;
    public GameObject equipPrefab;
    
    public float jumpValue()
    {
        return equipable.jumpValue; 
    }
    
    public float speenValue()
    {
        return equipable.speenValue; 
    }
    
    public override void UseItem()
    {
        // 아이템 장착
        CharacterManager.Instance.Player.controller.EquipItem(this, equipPrefab);
    }
}