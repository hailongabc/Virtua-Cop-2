using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float currentHP = 100;

    private void OnEnable()
    {
        GameManager.ins.listEnemy.Add(this);
    }
    public void getShotInHead(float damage)
    {
        currentHP = currentHP - damage * 1.2f;
        Debug.Log("head" + currentHP);
        Dead();

    }

    public void getShotInBody(float damage)
    {
        currentHP = currentHP - damage;
        Debug.Log("body" + currentHP);
        Dead();
    }

    public void Dead()
    {
        if(currentHP <= 0)
        {
            GameManager.ins.listEnemy.Remove(this);
            Destroy(gameObject);
        }
    }
}
