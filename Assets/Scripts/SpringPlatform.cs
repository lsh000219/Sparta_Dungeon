using UnityEngine;

public class SpringPlatform : MonoBehaviour
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
        controller.SpringJump(jumpPower);
    }
}
