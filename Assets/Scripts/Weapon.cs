using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Gun[] LoadOut;
    public Transform WeaponParent;
    public GameObject CurrentWeapon;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Equip(0);
        }
    }

    void Equip(int index)
    {
        if(CurrentWeapon != null)
        {
            Destroy(CurrentWeapon);
        }
        GameObject newWeapon = Instantiate(LoadOut[index].prefabs, WeaponParent.position, WeaponParent.rotation, WeaponParent) as GameObject;
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localEulerAngles = Vector3.zero;

        CurrentWeapon = newWeapon;
    }
}
