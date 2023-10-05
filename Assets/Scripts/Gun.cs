using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    public string Name;
    public float FireRate;
    public GameObject prefabs;
}
