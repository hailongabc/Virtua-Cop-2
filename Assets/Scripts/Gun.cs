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
  
    

}
