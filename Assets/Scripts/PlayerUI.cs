using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI ins;
    public TMP_Text txtBullet;
    public TMP_Text txtHP;
    private void Awake()
    {
        ins = this;
    }



    public void DecreaseHP()
    {

    }
}
