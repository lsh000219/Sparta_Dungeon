using UnityEngine;

public class ForcedJumpPlatform : MonoBehaviour
{
    private PlayerController controller;

    [SerializeField]
    public float jumpPower;
    
    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 upward = CharacterManager.Instance.Player.controller.transform.up * (jumpPower * 0.4f);
        Vector3 forward = CharacterManager.Instance.Player.controller.transform.forward * (jumpPower * 10f);
        CharacterManager.Instance.Player.controller.ForcedJump((upward + forward) * jumpPower);
    }
}
