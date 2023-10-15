using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHuman1 : MonoBehaviour
{
    public float currentHP = 100;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
   
    public void getShotInHead(float damage)
    {
        currentHP = currentHP - damage * 1.2f;
        Debug.Log("head" + currentHP);
    }

    public void getShotInBody(float damage)
    {
        currentHP = currentHP - damage;
        Debug.Log("body" + currentHP);
    }
}
