using UnityEngine;

[CreateAssetMenu(fileName = "Item_Pepper", menuName = "Item/Item_Pepper")]
public class Item_Pepper:Item_Useable
{
    public override void ItemEffect()
    {
        CharacterManager.Instance.Player.effectManager.PepperEffect(itemDataUseable.value);
    }
}