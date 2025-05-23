using UnityEngine;

public class SpringPlatform : MonoBehaviour  // 높이 점프하는 하늘색 플랫폼
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
            controller.FixedJump(jumpPower);
        }
    }
}
