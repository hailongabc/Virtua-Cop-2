using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Sway : MonoBehaviourPunCallbacks
{
    public float Instensity;
    public float Smooth;
    public bool isPickUp = false;
    private Quaternion origi_rotation;
    void Start()
    {
        origi_rotation = transform.localRotation; 
    }

    void Update()
    {
        // if (!photonView.IsMine) return;
        if (isPickUp)
        {
        UpdateSway();
        }

    }

    private void UpdateSway()
    {
        float x_Mouse = Input.GetAxis("Mouse X");
        float y_Mouse = Input.GetAxis("Mouse Y");
        Quaternion x_adj = Quaternion.AngleAxis(-Instensity * x_Mouse, Vector3.up);
        Quaternion y_adj = Quaternion.AngleAxis(-Instensity * y_Mouse, Vector3.right);

        Quaternion target_rotation = origi_rotation * x_adj * y_adj;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, target_rotation, Smooth * Time.deltaTime);
    }
}
