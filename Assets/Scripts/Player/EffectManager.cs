using UnityEngine;
using System.Collections;

public class EffectManager:MonoBehaviour
{
    Coroutine coroutine;
    
    public Canvas PepperCanvas;

    public void PepperEffect(float value)
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(PepperTimer(2.0f, value));
    }
    
    IEnumerator PepperTimer(float battleTime, float value)
    {
        float curTime = battleTime; float i = value;
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

    public void SteakEffect(float value)
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(SteakTimer(15.0f, value));
    }
    
    IEnumerator SteakTimer(float battleTime, float value)
    {
        float curTime = battleTime; float i = value;
        CharacterManager.Instance.Player.controller.ChangeScale(value, true);

        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            yield return null;
        }
        CharacterManager.Instance.Player.controller.ChangeScale(1f, false);
        coroutine = null;
    }
}