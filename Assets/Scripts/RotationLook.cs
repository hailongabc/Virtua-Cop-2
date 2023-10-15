using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RotationLook : MonoBehaviourPunCallbacks
{
    public static bool CursorLocked = true;
    public Transform player;
    public Transform cam;
    public float XSensivity;
    public float YSensivity;
    public float MaxAngle;
    private Quaternion camCenter;
    public Camera CamParents;

    void Start()
    {
        //CanvasManager.ins.canvas.worldCamera = CamParents;
        CamParents.gameObject.SetActive(photonView.IsMine);
        camCenter = cam.localRotation;
    }

    void LateUpdate()
    {
        if (!photonView.IsMine) return;
        setY();
        setX();
        UpdateCursorLock();
    }

    void setY()
    {
        float t_input = Input.GetAxis("Mouse Y") * YSensivity * Time.deltaTime;
        Quaternion quaternion = Quaternion.AngleAxis(t_input, -Vector3.right);
        Quaternion t_delta = cam.localRotation * quaternion;
        if(Quaternion.Angle(camCenter, t_delta) < MaxAngle)
        {
            cam.localRotation = t_delta;
        }
    }

    void setX()
    {
        float t_input = Input.GetAxis("Mouse X") * XSensivity * Time.deltaTime;
        Quaternion quaternion = Quaternion.AngleAxis(t_input, Vector3.up);
        Quaternion t_delta = player.localRotation * quaternion;
        
        player.localRotation = t_delta;

    }

    void UpdateCursorLock()
    {
        if (CursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                CursorLocked = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                CursorLocked = true;
            }
        }
    }
}
