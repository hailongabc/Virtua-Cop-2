using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float currentHP = 100;
    public float maxHP;
    private Camera cam;
    public bool isDead;
    [SerializeField] private Image healthBarSprite;
    [SerializeField] private GameObject healthBar;
    private float target = 1;
    [SerializeField] private float reduceSpeed = 2;


    private void OnEnable()
    {
        GameManager.ins.listEnemy.Add(this);
      
    }
    private void Start()
    {
        maxHP = currentHP;
        UpdateHealthBar(maxHP, currentHP);
        cam = GameManager.ins.camHealth;
    }
    public void getShotInHead(float damage)
    {
        currentHP = currentHP - damage * 1.2f;
        UpdateHealthBar(maxHP, currentHP);
        Debug.Log("head" + currentHP);
        Dead();

    }

    public void getShotInBody(float damage)
    {
        currentHP = currentHP - damage;
        UpdateHealthBar(maxHP, currentHP);
        Debug.Log("body" + currentHP);
        Dead();
    }

    public void Dead()
    {
        if(currentHP <= 0)
        {
            GameManager.ins.listEnemy.Remove(this);
            isDead = true;
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        healthBar.transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, target, reduceSpeed * Time.deltaTime);
    }
    public void UpdateHealthBar(float maxHealth, float currentHealh)
    {
        target = currentHealh / maxHealth;
    }
}
