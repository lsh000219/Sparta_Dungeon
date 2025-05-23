using UnityEngine;

[CreateAssetMenu(fileName = "Item_Steak", menuName = "Item/Item_Steak")]
public class Item_Steak:Item_Useable
{
    public override void ItemEffect()
    {
        CharacterManager.Instance.Player.effectManager.SteakEffect(itemDataUseable.value);
    }
}