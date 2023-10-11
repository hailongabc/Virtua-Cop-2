using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Manager : MonoBehaviourPunCallbacks
{
    public string PlayerPf;
    public Transform SpawnPoint;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        PhotonNetwork.Instantiate(PlayerPf, SpawnPoint.position, SpawnPoint.rotation);
    }
}
