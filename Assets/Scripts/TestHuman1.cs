using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestHuman1 : MonoBehaviour
{
    public float currentHP = 100;
    public float maxHP = 100;
    private float target = 1;
    [SerializeField] private float reduceSpeed = 2;
    void Start()
    {
       // PlayerUI.ins.txtHP.text = currentHP.ToString();
        UpdateHealthBar(maxHP, currentHP);

    }

    public void getShotInHead(float damage)
    {
        currentHP = currentHP - damage * 1.2f;
        Debug.Log("head" + currentHP);
        //PlayerUI.ins.txtHP.text = currentHP.ToString();

        Dead();
    }

    public void getShotInBody(float damage)
    {
        currentHP = currentHP - damage;
        Debug.Log("body" + currentHP);
       // PlayerUI.ins.txtHP.text = currentHP.ToString();
        UpdateHealthBar(maxHP, currentHP);
        Dead();
    }

    public void Dead()
    {
        if(currentHP <= 0)
        {
            Destroy(gameObject);
            //Time.timeScale = 0;
            PlayerUI.ins.GameOver.gameObject.SetActive(true);
            GameManager.ins.playerUI.SetActive(false);
            GameManager.ins.main_cam.gameObject.SetActive(true);
            GameManager.ins.isPlayerDead = true;
        }
    }
    private void Update()
    {
        PlayerUI.ins.healthBarSprite.fillAmount = Mathf.MoveTowards(PlayerUI.ins.healthBarSprite.fillAmount, target, reduceSpeed * Time.deltaTime);
    }
    public void UpdateHealthBar(float maxHealth, float currentHealh)
    {
        target = currentHealh / maxHealth;
    }
}
