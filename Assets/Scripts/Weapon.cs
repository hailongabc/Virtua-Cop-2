using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Gun[] LoadOut;
    public Transform WeaponParent;
    private GameObject CurrentWeapon;
    private int this_index;

    public GameObject BulleHolePf;
    public LayerMask canBeShot;

    private float currentCoolDown;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Equip(0);
        }
        if (CurrentWeapon != null)
        {
        aim(Input.GetMouseButton(1));
            if (Input.GetMouseButtonDown(0) && currentCoolDown <= 0)
            {
                Shoot();
            }

            CurrentWeapon.transform.localPosition = Vector3.Lerp(CurrentWeapon.transform.localPosition, Vector3.zero, Time.deltaTime * 10);
            if(currentCoolDown > 0)
            {
                currentCoolDown -= Time.deltaTime;
            }
        }
    }

    void Equip(int index)
    {
        if(CurrentWeapon != null)
        {
            Destroy(CurrentWeapon);
        }
        this_index = index;
        GameObject newWeapon = Instantiate(LoadOut[index].prefabs, WeaponParent.position, WeaponParent.rotation, WeaponParent) as GameObject;
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localEulerAngles = Vector3.zero;

        CurrentWeapon = newWeapon;
    }
    void aim(bool p_aim)
    {
        Transform anchor = CurrentWeapon.transform.GetChild(0);
        Transform Status_hip = CurrentWeapon.transform.GetChild(1).GetChild(0);
        Transform Status_ADS = CurrentWeapon.transform.GetChild(1).GetChild(1);
        if (p_aim)
        {
            anchor.position = Vector3.Lerp(anchor.position, Status_ADS.position, Time.deltaTime * LoadOut[this_index].AimSpeed);
        }
        else
        {
            anchor.position = Vector3.Lerp(anchor.position, Status_hip.position, Time.deltaTime * LoadOut[this_index].AimSpeed);
        }
    }
    void Shoot()
    {
        Transform t_cam = transform.Find("PlayerCamera");
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(t_cam.position, t_cam.forward, out hit, 1000f, canBeShot))
        {
            GameObject NewHole = Instantiate(BulleHolePf, (hit.point + hit.normal), Quaternion.identity);

            NewHole.transform.LookAt(hit.point + hit.normal);

            Destroy(NewHole, 10f);
        }

        //giat len
        CurrentWeapon.transform.Rotate(-LoadOut[this_index].Recoil, 0, 0);

        //giat ve sau
        CurrentWeapon.transform.position -= CurrentWeapon.transform.forward * LoadOut[this_index].KickBack;

        currentCoolDown = LoadOut[this_index].FireRate;
    }



}
