using UnityEngine;
public enum ItemType
{
    Consumable,
    Useable,
    Equipable,
}

public class ItemData:ScriptableObject
{
    [SerializeField]
    public ItemType type;
    
    [Header("Info")]
    public string displayName;
    public string description;
    public Sprite icon;
    public GameObject dropPrefab;
    
    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    public virtual void UseItem(){}
}