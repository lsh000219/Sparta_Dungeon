﻿using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;
    public EffectManager effectManager;

    public ItemData itemData;
    public Action addItem;

    public Transform dropPosition;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }
}