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
            if (!wasHitLastFrame)
            {
                if (PepperCanvas != null)
                    PepperCanvas.gameObject.SetActive(true);
            }
        }
        else
        {
            if (wasHitLastFrame)
            {
                if (PepperCanvas != null)
                    PepperCanvas.gameObject.SetActive(false);
            }
        }

        // 상태 업데이트
        wasHitLastFrame = isHit;
        Debug.DrawRay(rayOrigin.position, rayOrigin.forward * 50f, Color.red);
    }
}
