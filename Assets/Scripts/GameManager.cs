using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager ins;
    public Bullet bullet;
    public List<GunInGame> listGun;
    public GameObject PlayerCam;
    public GameObject Player;
    public List<Enemy> listEnemy;
    public List<Transform> listEnemySpawn;
    public GameObject Enemy;
    public Camera camHealth;
    public Camera main_cam;
    public GameObject playerUI;
    public bool isPlayerDead = false;
    public bool isUpdate = false;


    private void Awake()
    {
        ins = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        if (!isPlayerDead)
        {
            if (listEnemy.Count < 3)
            {
                Instantiate(Enemy, listEnemySpawn[Random.Range(0, listEnemySpawn.Count)]);
            }
        }
        else
        {
            if (!isUpdate)
            {
                for (int i = 0; i < listEnemySpawn.Count; i++)
                {
                    if (listEnemySpawn[i].transform.childCount > 0)
                    {
                        Destroy(listEnemySpawn[i].GetChild(0).gameObject);
                    }
                }
                isUpdate = true;
            }

        }

    }
}
