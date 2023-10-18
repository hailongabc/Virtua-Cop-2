using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject PlayerPf;
    public Transform SpawnPoint;

    private void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        Instantiate(PlayerPf, SpawnPoint.position, SpawnPoint.rotation);
        //PhotonNetwork.Instantiate(PlayerPf, SpawnPoint.position, SpawnPoint.rotation);
    }


}
