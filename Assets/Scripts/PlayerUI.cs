using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI ins;
    public TMP_Text txtBullet;
    public TMP_Text txtScore;
    public TMP_Text txtCurrentScore;
    public TMP_Text txtHighScore;
    public TMP_Text txtTime;
    public TMP_Text txtCurrentTime;
    public TMP_Text txtTimeHighScore;
    public Button Replay;
    public int Score;
    public TMP_Text txtHP;
    public GameObject GameOver;
    public Slider Reloading;
    public Image healthBarSprite;
    public GameObject main_cam;
   [HideInInspector] public float _curTimeSpin;
    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        Replay.onClick.AddListener(ReplayBtn);
        StartCoroutine(CountDownHeart());
    }

    private void ReplayBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator CountDownHeart()
    {
        while (!GameManager.ins.isPlayerDead)
        {
            var duration = TimeSpan.FromSeconds(_curTimeSpin);
            txtTime.text = "Time: " + duration.ToString(@"mm\:ss");
            yield return new WaitForSeconds(1);
            _curTimeSpin++;
        }
    }

}
