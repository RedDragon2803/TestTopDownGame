using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "TestProject/WeaponStats")]
public class WeaponStats : ScriptableObject
{
    public float damage;
    public float bulletSpeed;
    public float fireRate;
    public int clipSize;
    public float reloadTime;
    public float range;
    public Sprite sprite;
}
