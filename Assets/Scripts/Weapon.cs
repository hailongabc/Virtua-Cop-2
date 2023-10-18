using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Weapon : MonoBehaviour
{
    public Gun[] LoadOut;
    public Transform WeaponParent;
    public Transform DropPoint;
    [SerializeField] private GameObject CurrentWeapon;
    private int this_index;

    public GameObject BulleHolePf;
    public LayerMask canBeShot;
    private float currentCoolDown;
    bool isTap = false;
    bool isPistol = false;
    private void Awake()
    {
    }


    void Update()
    {
        // if (!photonView.IsMine) return;
        if (CurrentWeapon != null)
        {
            // aim(Input.GetMouseButton(1));

            if (Input.GetKeyDown(KeyCode.G))
            {
                DropGun();
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (isPistol)
                {
                    isTap = true;
                }
            }
            if (Input.GetMouseButton(0))
            {
                if (!CurrentWeapon.GetComponent<GunInGame>().isOutOfBullet)
                {
                    if (CurrentWeapon.GetComponent<GunInGame>().CheckBullet())
                    {
                        if (isPistol)
                        {
                            if (isTap)
                            {
                                isTap = false;
                                CurrentWeapon.GetComponent<GunInGame>().Shoot2();
                            }
                        }
                        else
                        {
                            CurrentWeapon.GetComponent<GunInGame>().Shoot2();
                        }
                        //StartCoroutine(CurrentWeapon.GetComponent<GunInGame>().Shoot());
                    }
                    else
                    {
                        ReloadBullet();
                    }
                }
            }

            if (CurrentWeapon != null)
            {
                CurrentWeapon.transform.localPosition = Vector3.Lerp(CurrentWeapon.transform.localPosition, Vector3.zero, Time.deltaTime * 10);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (!CurrentWeapon.GetComponent<GunInGame>().isReload)
                {
                    ReloadBullet();
                }
            }
        }
    }

    void DropGun()
    {
        for (int i = 0; i < GameManager.ins.listGun.Count; i++)
        {
            if (CurrentWeapon.GetComponent<GunInGame>().guntype == GameManager.ins.listGun[i].guntype)
            {
                GameObject gun = Instantiate(GameManager.ins.listGun[i].gameObject, DropPoint.position, DropPoint.rotation);
                gun.GetComponent<GunInGame>().Init(CurrentWeapon.GetComponent<GunInGame>().CurrentAmmo, CurrentWeapon.GetComponent<GunInGame>().AmmoLeft);
                Debug.Log("current " + gun.GetComponent<GunInGame>().CurrentAmmo);
                Debug.Log("left " + gun.GetComponent<GunInGame>().AmmoLeft);
                gun.GetComponent<Sway>().isPickUp = false;
                gun.GetComponent<Rigidbody>().useGravity = true;
                gun.GetComponent<BoxCollider>().enabled = true;
                PlayerUI.ins.txtBullet.gameObject.SetActive(false);
            }
        }
        Destroy(CurrentWeapon);
        CurrentWeapon = null;
    }
    void PickUpGun(GunType gunType, GunInGame dropGun)
    {
        if (CurrentWeapon != null) return;
        for (int i = 0; i < GameManager.ins.listGun.Count; i++)
        {
            if (GameManager.ins.listGun[i].guntype == gunType)
            {
                GameObject gun = Instantiate(GameManager.ins.listGun[i].gameObject, WeaponParent.position, WeaponParent.rotation, WeaponParent);
                gun.transform.localPosition = Vector3.zero;
                gun.transform.localEulerAngles = Vector3.zero;
                gun.GetComponent<Sway>().isPickUp = true;
                gun.GetComponent<BoxCollider>().enabled = false;
                gun.GetComponent<Rigidbody>().useGravity = false;
                CurrentWeapon = gun;
                CurrentWeapon.GetComponent<GunInGame>().Init(dropGun.CurrentAmmo, dropGun.AmmoLeft);
            }
        }

    }
    void Equip(int index)
    {
        if (CurrentWeapon != null)
        {
            Destroy(CurrentWeapon);
        }

        this_index = index;
        GameObject newWeapon = Instantiate(LoadOut[index].prefabs, WeaponParent.position, WeaponParent.rotation, WeaponParent) as GameObject;
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localEulerAngles = Vector3.zero;
        newWeapon.GetComponent<Sway>().isPickUp = true;
        CurrentWeapon = newWeapon;
        newWeapon.GetComponent<BoxCollider>().enabled = false;
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

    void ReloadBullet()
    {
        if (CurrentWeapon.GetComponent<GunInGame>().AmmoLeft > 0)
            StartCoroutine(CurrentWeapon.GetComponent<GunInGame>().ReloadBullet());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            switch (other.GetComponent<GunInGame>().guntype)
            {
                case GunType.pistol:
                    if (CurrentWeapon != null) return;
                    PickUpGun(GunType.pistol, other.GetComponent<GunInGame>());
                    isPistol = true;
                    Destroy(other.gameObject);
                    break;
                case GunType.akm:
                    if (CurrentWeapon != null) return;
                    PickUpGun(GunType.akm, other.GetComponent<GunInGame>());
                    isPistol = false;
                    Destroy(other.gameObject);
                    break;
                case GunType.yellowGun:
                    if (CurrentWeapon != null) return;
                    PickUpGun(GunType.yellowGun, other.GetComponent<GunInGame>());
                    isPistol = false;
                    Destroy(other.gameObject);
                    break;
            }


        }
    }

}
