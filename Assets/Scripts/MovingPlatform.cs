using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private PlayerController controller;
    
    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
    }

    private void Move(float x, float y, float z)
    {
        //gameObject.transform.position
    }

    private void FixedUpdate()
    {

    }
}
