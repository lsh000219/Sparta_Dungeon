using System;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }
    
    public float healthDecay;
    public float staminaRecover;

    private void Update()
    {
        health.Subtract(healthDecay * Time.deltaTime);
        stamina.Add(staminaRecover * Time.deltaTime);

        if (health.curValue < 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }
    
    public void jump(float amount){stamina.Subtract(amount);}

    public void Die()
    {
        Debug.Log("플레이어가 죽었다.");
    }
}