using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField]
    public Canvas pepperEffect;
    public GameObject playerSprite;

    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;
    public float jumpPower;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;

    [HideInInspector]
    public bool canLook = true;

    public Action Inventory;
    private Rigidbody rigidbody;

    public bool pov = true;  //1, 3인칭 설정 - true면 1인칭
    public bool giant = false;  //거대화 되었는지 확인, Interaction에서 사용
    
    Item_Equipable equipable;
    private GameObject equipPrefab;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void  Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void ChangeView(InputAction.CallbackContext context)  //1인칭, 3인칭에 따른 카메라 위치 설정
    {
        if (pov)
        {
            cameraContainer.localPosition = new Vector3(cameraContainer.localPosition.x, 3f, -5f);
            cameraContainer.localEulerAngles = new Vector3(20f, 10f, 0f);
            
            pov = false;
        }
        else
        {
            cameraContainer.localPosition = new Vector3(cameraContainer.localPosition.x, 1f, 0);
            cameraContainer.localEulerAngles = new Vector3(0f, 1f, 0f);
            
            pov = true;
        }
    }
    
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            Jump();
            CharacterManager.Instance.Player.condition.jump(20.0f);
        }
    }

    public void ForcedJump(Vector3 v)  //빨간색 플랫폼 효과
    {
        rigidbody.AddForce(v, ForceMode.Impulse);
    }
    
    public void Jump()  // 스페이스 바로 할수있는 일반적인 점프
    {
        Vector2 v;
        if (equipable != null)
        {
            v = Vector2.up * (this.jumpPower + equipable.jumpValue());
        }
        else
        {
            v = Vector2.up * this.jumpPower;
        }
        
        rigidbody.AddForce(v, ForceMode.Impulse);
    }
    
    public void FixedJump(float value)  //하늘색 플랫폼 효과
    {
        rigidbody.AddForce(Vector2.up * value, ForceMode.Impulse);
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        
        Vector3 targetVelocity;
        if (equipable != null)
        {
            targetVelocity = dir * (moveSpeed + equipable.speenValue());
        }
        else
        {
            targetVelocity = dir * moveSpeed;
        }


        Vector3 velocity = Vector3.Lerp(
            new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z), 
            targetVelocity,
            0.1f 
        );

        velocity.y = rigidbody.velocity.y;

        rigidbody.velocity = velocity;
    }

    void CameraLook()
    {
        if (pov)
        {
            camCurXRot += mouseDelta.y * lookSensitivity;
            camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
            cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

            transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
        }
        else
        {
            camCurXRot += mouseDelta.y * lookSensitivity;
            camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
            cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 10f, 0);

            transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
        }
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public void OnInventoryButton(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
        {
            Inventory?.Invoke();
            ToggleCursor();
        }
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    public void ChangeScale(float scale, bool toggle)  //EffectManager의 스테이크 효과 실행
    {
        giant = toggle;
        rigidbody.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void EquipItem(Item_Equipable item, GameObject equipPrefab)  // 장비 아이템 장착
    {
        UnEquipItem();
            
        equipable = item;
        this.equipPrefab = Instantiate(equipPrefab, playerSprite.transform);
    }  
    
    public void UnEquipItem()   //장착 아이템 해제
    {
        equipable = null;
        Destroy(equipPrefab);

    }
}