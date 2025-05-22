using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Coroutine coroutine;

    [SerializeField]
    public Canvas pepperEffect;

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

    private bool pov = true;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
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

    public void ChangeView(InputAction.CallbackContext context)
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
            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            CharacterManager.Instance.Player.condition.jump(20.0f);
        }
    }

    public void ForcedJump(float jumpForce)
    {
        Vector3 upward = transform.up * (jumpForce * 0.4f);
        Vector3 forward = transform.forward * (jumpForce * 10f);
        rigidbody.AddForce((upward + forward) * jumpForce, ForceMode.Impulse);
    }
    
    public void SpringJump(float jumpForce)
    {
        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
    }

    public void PepperEffect()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(PepperTimer(2.0f));
    }

    IEnumerator PepperTimer(float battleTime)
    {
        float curTime = battleTime; int i = 5;
        pepperEffect.gameObject.SetActive(true);

        while (curTime > 0)
        {
            curTime -= Time.deltaTime;

            if (curTime <= 0)
            {
                rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse); i--;
                if (i <= 0) { 
                    pepperEffect.gameObject.SetActive(false);
                    coroutine = null;  yield break;
                }
                curTime = 3.0f;
            }
            yield return null;
        }
        coroutine = null;
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        Vector3 targetVelocity = dir * moveSpeed;


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

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
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
}