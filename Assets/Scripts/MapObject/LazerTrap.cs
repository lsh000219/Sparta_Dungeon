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

        if (isHit) // 레이저에 맞았을 때
        {
            if (hit.collider.CompareTag("Player"))  //맞은게 플레이어라면
            {
                if (!wasHitLastFrame)
                {
                    if (PepperCanvas != null)
                        PepperCanvas.gameObject.SetActive(true);   //화면 빨개짐
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
        else  //맞은게 없다면
        {
            if (wasHitLastFrame) //화면이 빨개지는 효과 없앰, 플레이어가 레이를 벗어났을 때 한 번만 호출
            {
                if (PepperCanvas != null)
                    PepperCanvas.gameObject.SetActive(false);
            }

            wasHitLastFrame = false;
        }
        
        wasHitLastFrame = isHit;
    }
}
