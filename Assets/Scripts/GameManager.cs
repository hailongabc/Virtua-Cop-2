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
        if(listEnemy.Count < 3)
        {
            Instantiate(Enemy, listEnemySpawn[Random.Range(0, listEnemySpawn.Count)]);
        }
    }
}
