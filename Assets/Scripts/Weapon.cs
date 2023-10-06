using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Gun[] LoadOut;
    public Transform WeaponParent;
    private GameObject CurrentWeapon;
    private int this_index;
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
}
