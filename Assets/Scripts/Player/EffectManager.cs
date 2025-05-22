using UnityEngine;
using System.Collections;

public class EffectManager:MonoBehaviour
{
    Coroutine coroutine;
    
    public Canvas PepperCanvas;

    public void PepperEffect()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(PepperTimer(2.0f));
    }
    
    IEnumerator PepperTimer(float battleTime)
    {
        float curTime = battleTime; int i = 5;
        PepperCanvas.gameObject.SetActive(true);

        while (curTime > 0)
        {
            curTime -= Time.deltaTime;

            if (curTime <= 0)
            {
                CharacterManager.Instance.Player.controller.Jump(); i--;
                if (i <= 0) { 
                    PepperCanvas.gameObject.SetActive(false);
                    coroutine = null;  yield break;
                }
                curTime = 3.0f;
            }
            yield return null;
        }
        coroutine = null;
    }
}