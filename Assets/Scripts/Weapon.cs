using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Weapon : MonoBehaviourPunCallbacks
{
    public static Weapon ins;
    public Gun[] LoadOut;
    public Transform WeaponParent;
    public Transform DropPoint;
    [SerializeField] private GameObject CurrentWeapon;
    private int this_index;

    public GameObject BulleHolePf;
    public LayerMask canBeShot;
    private float currentCoolDown;
    bool isShot = false;
    private void Awake()
    {
        ins = this;
    }


    void Update()
    {
        if (!photonView.IsMine) return;
        if (CurrentWeapon != null)
        {
            aim(Input.GetMouseButton(1));

            if (Input.GetKeyDown(KeyCode.G))
            {
                for (int i = 0; i < GameManager.ins.listGun.Count; i++)
                {
                    if (CurrentWeapon.GetComponent<GunInGame>().guntype == GameManager.ins.listGun[i].guntype)
                    {
                        GameObject gun = Instantiate(GameManager.ins.listGun[i].gameObject, DropPoint.position, DropPoint.rotation);
                        gun.GetComponent<GunInGame>().Init(CurrentWeapon.GetComponent<GunInGame>().CurrentAmmo, CurrentWeapon.GetComponent<GunInGame>().AmmoLeft);
                        gun.GetComponent<Sway>().isPickUp = false;
                        gun.GetComponent<BoxCollider>().enabled = true;
                        PlayerUI.ins.txtBullet.text = "0/0";
                    }
                }
                Destroy(CurrentWeapon);
                CurrentWeapon = null;
            }
            if (Input.GetMouseButton(0))
            {
                if (CurrentWeapon.GetComponent<GunInGame>().CheckBullet())
                {

                    if (!isShot)
                    {
                        StartCoroutine(Shoot());

                    }
                }
                else
                {
                    ReloadBullet();
                }
            }
            if(CurrentWeapon != null)
            {
            CurrentWeapon.transform.localPosition = Vector3.Lerp(CurrentWeapon.transform.localPosition, Vector3.zero, Time.deltaTime * 10);
            }
            if (currentCoolDown > 0)
            {
                currentCoolDown -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ReloadBullet();
            }
        }
    }
    void PickUpGun(string GunType)
    {
        if (CurrentWeapon != null) return;
        for (int i = 0; i < GameManager.ins.listGun.Count; i++)
        {
            if (GameManager.ins.listGun[i].guntype.ToString() == GunType)
            {
                GameObject gun = Instantiate(GameManager.ins.listGun[i].gameObject, WeaponParent.position, WeaponParent.rotation, WeaponParent);
                gun.transform.localPosition = Vector3.zero;
                gun.transform.localEulerAngles = Vector3.zero;
                gun.GetComponent<Sway>().isPickUp = true;
                gun.GetComponent<BoxCollider>().enabled = false;
                CurrentWeapon = gun;
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
    IEnumerator Shoot()
    {
        isShot = true;
        yield return new WaitForSeconds(LoadOut[this_index].FireRate);
        Transform t_cam = transform.Find("PlayerCamera");
        //RaycastHit hit = new RaycastHit();
        GameObject bullet = Instantiate(GameManager.ins.bullet.gameObject, CurrentWeapon.GetComponent<Sway>().PointBullet.position, t_cam.rotation);
        bullet.GetComponent<Bullet>().damage = LoadOut[this_index].damage;
        CurrentWeapon.GetComponent<GunInGame>().DecreaseBullet();


        /* if (Physics.Raycast(t_cam.position, t_cam.forward, out hit, 1000f, canBeShot))
         {
             GameObject NewHole = Instantiate(BulleHolePf, (hit.point + hit.normal), Quaternion.identity);

             NewHole.transform.LookAt(hit.point + hit.normal);

             Destroy(NewHole, 10f);
         }
 */
        //giat len
        CurrentWeapon.transform.Rotate(-LoadOut[this_index].Recoil, 0, 0);

        //giat ve sau
        CurrentWeapon.transform.position -= CurrentWeapon.transform.forward * LoadOut[this_index].KickBack;

        isShot = false;
        currentCoolDown = LoadOut[this_index].FireRate;
    }

    void ReloadBullet()
    {
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
                    PickUpGun(GunType.pistol.ToString());
                    Destroy(other.gameObject);
                    break;
                case GunType.akm:
                    if (CurrentWeapon != null) return;
                    PickUpGun(GunType.akm.ToString());
                    Destroy(other.gameObject);
                    break;
            }


        }
    }

}
