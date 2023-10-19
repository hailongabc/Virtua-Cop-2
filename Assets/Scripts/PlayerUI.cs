using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI ins;
    public TMP_Text txtBullet;
    public TMP_Text txtHP;
    public TMP_Text GameOver;
    public Slider Reloading;
    public Image healthBarSprite;
    public GameObject main_cam;
    private void Awake()
    {
        ins = this;
    }

}
