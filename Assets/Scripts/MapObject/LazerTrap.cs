using UnityEngine;

public class LazerTrap : MonoBehaviour
{
    public Transform rayOrigin;
    public Canvas PepperCanvas;

    void Update()
    {
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 50f))
        {
            PepperCanvas.gameObject.SetActive(true);
        }
        else
        {
            PepperCanvas.gameObject.SetActive(false);
        }

        // Scene 뷰에 시각화
        Debug.DrawRay(rayOrigin.position, rayOrigin.forward * 50f, Color.red);
    }
}
