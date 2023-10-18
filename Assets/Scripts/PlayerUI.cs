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
    public Slider Reloading;
    private void Awake()
    {
        ins = this;
    }

}
