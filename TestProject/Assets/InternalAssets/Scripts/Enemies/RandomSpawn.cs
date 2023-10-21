using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public List<Transform> spawns;
    public int enemyCount;
    public GameObject EnemyPrefab;

    void Start()
    {
        spawns.Shuffle();
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(EnemyPrefab, spawns[i].position, spawns[i].rotation);
        }
    }
}
