using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    public string Name;
    public float FireRate;
    public float Recoil;
    public float KickBack;
    public float damage;
    public GameObject prefabs;
    public float AimSpeed;
  
    public float TotalBullet;
    public float MaxBullet;
    public float TimeReload;
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
    

}
