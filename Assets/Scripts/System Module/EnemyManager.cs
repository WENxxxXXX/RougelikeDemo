using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] GameObject[] enemy, boss;
    List<GameObject> enemyList;
    WaitUntil waitUntilNoEnemy;
    Vector3[] enemyPosition;
    Vector3 generatePos;

    protected override void Awake()
    {
        base.Awake();
        enemyList = new List<GameObject>();
        //waitUntilNoEnemy = new WaitUntil(NoEnemy);
        waitUntilNoEnemy = new WaitUntil(() => enemyList.Count == 0);

        enemyPosition = new Vector3[]
        {
            new Vector3(7.8f, 3.4f, 0f),
            new Vector3(7.8f, -3.4f, 0f),
            new Vector3(-7.8f, 3.4f, 0f),
            new Vector3(-7.8f, -3.4f, 0f),
            new Vector3(-3f, 0f, 0f),
            new Vector3(3f, 0f, 0f),
            new Vector3(1.5f, 3f, 0f),
            new Vector3(-1.5f, 3f, 0f),
            new Vector3(-3f, -3f, 0f),
            new Vector3(3f, -3f, 0f)
        };
    }

    public void GenerateRandomEnemy(int mount, Vector3 centra)
    {
        for (int i = 0; i < mount; i++)
        {
            do
            {
                generatePos = centra + enemyPosition[Random.Range(0, enemyPosition.Length)];
            } while (IsExistingEnemyPos(generatePos));

            enemyList.Add(PoolManager.Release(enemy[Random.Range(0, enemy.Length)],
                generatePos, Quaternion.identity));
        }

    }

    bool IsExistingEnemyPos(Vector3 position)
    {
        foreach (var enemy in enemyList)
        {
            if (Vector3.SqrMagnitude(enemy.transform.position - position) < 1)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveFromList(GameObject enemy)
    {
        enemyList.Remove(enemy);
    }
}
