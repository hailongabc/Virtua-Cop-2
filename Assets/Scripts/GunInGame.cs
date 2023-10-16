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
    [HideInInspector] public float CurrentAmmo;
    [HideInInspector] public float AmmoLeft;
    [SerializeField]
    private bool AddBulletSpread = true;
    [SerializeField]
    private Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField]
    private ParticleSystem ShootingSystem;
    [SerializeField]
    private Transform ImpactParticleSystem;
    [SerializeField]
    private TrailRenderer BulletTrail;
    public Gun DataGun;
    void Start()
    {
        CurrentAmmo = DataGun.TotalBullet;
        AmmoLeft = DataGun.MaxBullet;
        //PlayerUI.ins.txtBullet.text = CurrentAmmo + "/" + AmmoLeft;

    }
    public void Init(float CurrentAmmo, float AmmoLeft)
    {
        this.CurrentAmmo = CurrentAmmo;
        this.AmmoLeft = AmmoLeft;
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

  
}