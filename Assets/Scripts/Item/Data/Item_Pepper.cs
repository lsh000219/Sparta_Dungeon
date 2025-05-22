using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[CreateAssetMenu(fileName = "Item_Pepper", menuName = "Item/Item_Pepper")]
public class Item_Pepper:Item_Useable
{
    public override void ItemEffect()
    {
        CharacterManager.Instance.Player.effectManager.PepperEffect();
    }
}