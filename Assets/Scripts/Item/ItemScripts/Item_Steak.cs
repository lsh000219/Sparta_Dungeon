using UnityEngine;

[CreateAssetMenu(fileName = "Item_Steak", menuName = "Item/Item_Steak")]
public class Item_Steak:Item_Useable
{
    public override void ItemEffect()  // Player.effectManager에서 효과 구현
    {
        CharacterManager.Instance.Player.effectManager.SteakEffect(itemDataUseable.value);
    }
}