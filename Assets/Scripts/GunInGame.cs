using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GunType
{
    pistol, akm
}
public class GunInGame : MonoBehaviour
{
    public GunType guntype;
     public float CurrentAmmo;
     public float AmmoLeft;
    public Transform PointBullet;
    public bool IsReadyFire = false;
    [SerializeField]
    private bool AddBulletSpread = true;
    [SerializeField]
    private Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField]
    private ParticleSystem ParticleShoot;
    [SerializeField]
    private Transform ImpactParticleSystem;
    [SerializeField]
    private TrailRenderer BulletTrail;
    public Gun DataGun;

    private void Awake()
    {
        CurrentAmmo = DataGun.TotalBullet;
        AmmoLeft = DataGun.MaxBullet;
    }

   
    public void Init(float CurrentAmmo, float AmmoLeft)
    {
        this.CurrentAmmo = CurrentAmmo;
        this.AmmoLeft = AmmoLeft;
        PlayerUI.ins.txtBullet.gameObject.SetActive(true);
        PlayerUI.ins.txtBullet.text = CurrentAmmo + "/" + AmmoLeft;
    }
    public void DecreaseBullet()
    {
        CurrentAmmo--;
        PlayerUI.ins.txtBullet.text = CurrentAmmo + "/" + AmmoLeft;
    }

    public bool CheckBullet()
    {
        if (CurrentAmmo > 0)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    public IEnumerator ReloadBullet()
    {
        yield return new WaitForSeconds(DataGun.TimeReload);
        while (CurrentAmmo < DataGun.TotalBullet)
        {
            if(AmmoLeft > 0)
            {
            CurrentAmmo++;
            AmmoLeft--;
            }
            else
            {
                break;
            }
        }
        PlayerUI.ins.txtBullet.text = CurrentAmmo + "/" + AmmoLeft;
    }

  public IEnumerator Shoot()
    {
        if (!IsReadyFire)
        {
            GameObject bullet = Instantiate(GameManager.ins.bullet.gameObject, PointBullet.position, GameManager.ins.PlayerCam.transform.rotation);
            bullet.GetComponent<Bullet>().damage = DataGun.damage;
            IsReadyFire = true;
            ParticleShoot.Play();
            DecreaseBullet();
            /* if (Physics.Raycast(t_cam.position, t_cam.forward, out hit, 1000f, canBeShot))
             {
                 GameObject NewHole = Instantiate(BulleHolePf, (hit.point + hit.normal), Quaternion.identity);

                 NewHole.transform.LookAt(hit.point + hit.normal);

                 Destroy(NewHole, 10f);
             }
     */
            //giat len
            transform.Rotate(-DataGun.Recoil, 0, 0);

            //giat ve sau
            transform.position -= transform.forward * DataGun.KickBack;
            //delay
            yield return new WaitForSeconds(DataGun.FireRate);
            IsReadyFire = false;
        }
        //RaycastHit hit = new RaycastHit();



    }
}
