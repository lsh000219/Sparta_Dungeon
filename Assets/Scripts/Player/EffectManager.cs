using UnityEngine;
using System.Collections;

public class EffectManager:MonoBehaviour    // 아이템 효과 구현, 실행 
{
    Coroutine coroutine;
    
    public Canvas PepperCanvas;

    public void PepperEffect(float value)   // 고추 아이템 효과
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(PepperTimer(2.0f, value));
    }
    
    IEnumerator PepperTimer(float battleTime, float value)  //화면 빨개짐, value만큼 대기후 점프
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

    
    
    public void SteakEffect(float value)// 스테이크 아이템 효과
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(SteakTimer(15.0f, value));
    }
    
    IEnumerator SteakTimer(float battleTime, float value)  //15초 동안 value만큼 커짐, 그에따라 상호작용 가능 범위도 증가
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