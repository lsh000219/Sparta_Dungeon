using UnityEngine;

public class LazerTrap : MonoBehaviour
{
    public Transform rayOrigin;
    public Canvas PepperCanvas;

    private bool wasHitLastFrame = false;
    
    void Update()
    {
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        bool isHit = Physics.Raycast(ray, out RaycastHit hit, 50f);

        Debug.DrawRay(rayOrigin.position, rayOrigin.forward * 50f, Color.red);

        if (isHit)
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (!wasHitLastFrame)
                {
                    if (PepperCanvas != null)
                        PepperCanvas.gameObject.SetActive(true);
                }

                wasHitLastFrame = true;
            }
            else
            {
                if (wasHitLastFrame)
                {
                    if (PepperCanvas != null)
                        PepperCanvas.gameObject.SetActive(false);
                }

                wasHitLastFrame = false;
            }
        }
        else
        {
            if (wasHitLastFrame)
            {
                if (PepperCanvas != null)
                    PepperCanvas.gameObject.SetActive(false);
            }

            wasHitLastFrame = false;
        }
        
        wasHitLastFrame = isHit;
    }
}
