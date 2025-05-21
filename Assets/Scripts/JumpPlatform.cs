using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    private PlayerController controller;

    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
    }

    private void OnCollisionEnter(Collision collision)
    {
        controller.ForcedJump(500.0f);
    }
}
