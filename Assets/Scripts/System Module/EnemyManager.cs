using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] GameObject[] enemyPrefabs;
    List<GameObject> enemyList;
    WaitUntil waitUntilNoEnemy;

    protected override void Awake()
    {
        base.Awake();
        enemyList = new List<GameObject>();
        //waitUntilNoEnemy = new WaitUntil(NoEnemy);
        waitUntilNoEnemy = new WaitUntil(() => enemyList.Count == 0);
    }

    public void AddToList(GameObject enemy)
    {
        enemyList.Add(enemy);
    }

    public void RemoveFromList(GameObject enemy)
    {
        enemyList.Remove(enemy);
    }
}
