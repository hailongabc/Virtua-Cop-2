using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true; //tự đồng bộ hóa sence
        Connect();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected");
        Join();
        base.OnConnectedToMaster();
    }
    public void Connect()
    {
        Debug.Log("Trying to Connect...");
        PhotonNetwork.GameVersion = "0.0.0"; //phiên bản
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedRoom()
    {
        StartGame();
        base.OnJoinedRoom();
    }
    public void Join()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void StartGame()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LoadLevel(1);
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Create();
        base.OnJoinRandomFailed(returnCode, message);
    }

    public void Create()
    {
        PhotonNetwork.CreateRoom("");
    }
}
