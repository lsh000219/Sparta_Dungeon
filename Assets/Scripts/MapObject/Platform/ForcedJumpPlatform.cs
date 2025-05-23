using UnityEngine;

public class ForcedJumpPlatform : MonoBehaviour  //보는 방향으로 날아가는 빨간색 플랫폼
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
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 upward = CharacterManager.Instance.Player.controller.transform.up * (jumpPower * 0.5f);
            Vector3 forward = CharacterManager.Instance.Player.controller.transform.forward * (jumpPower * 5f);
            CharacterManager.Instance.Player.controller.ForcedJump((upward + forward) * jumpPower);
        }
    }
}
