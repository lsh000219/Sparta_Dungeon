using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    private Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            
            float viewRange;
            
            if (!CharacterManager.Instance.Player.controller.giant)
            {
                viewRange = 1.0f;
            }
            else
            {
                viewRange = 3.0f;
            }
            
            Ray ray; RaycastHit hit;
            if (CharacterManager.Instance.Player.controller.pov)
            {
                ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            }
            else
            {
                ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                viewRange += 3.0f;
                if (CharacterManager.Instance.Player.controller.giant)
                {
                    viewRange += 10.0f;
                }
            }

            
            
            
            if (Physics.Raycast(ray, out hit, maxCheckDistance + viewRange, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}