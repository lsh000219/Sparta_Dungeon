using UnityEngine;

[System.Serializable]
public class ItemDataEquipable
{
    public float jumpValue;
    public float speenValue;
}
[CreateAssetMenu(fileName = "Item_Equipable", menuName = "Item/Item_Equipable")]
public class Item_Equipable:ItemData   //장착할 수 있는 아이템
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
    
    public override void UseItem()  //부모의 UseItem 구현
    {
        CharacterManager.Instance.Player.controller.EquipItem(this, equipPrefab);
    }
}