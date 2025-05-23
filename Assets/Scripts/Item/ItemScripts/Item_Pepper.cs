using UnityEngine;

[CreateAssetMenu(fileName = "Item_Pepper", menuName = "Item/Item_Pepper")]
public class Item_Pepper:Item_Useable
{
    public override void ItemEffect()  // Player.effectManager에서 효과 구현
    {
        CharacterManager.Instance.Player.effectManager.PepperEffect(itemDataUseable.value);
    }
}