using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "TestProject/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float maxHealth;
    public float speed;
    public float damage;
    public float attackCooldown;
    public float agroRadius;
    public float attackRange;
}
