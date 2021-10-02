using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PoolManager : MonoBehaviour
{
    [SerializeField] Pool[] playerPrejectilePools;
    [SerializeField] Pool[] enemyPrejectilePools;
    [SerializeField] Pool[] enemyPools;

    static Dictionary<GameObject, Pool> dictionary;//对象池的预制体 和 对象池 一一对应的字典。预制体为key

    private void Awake()
    {
        dictionary = new Dictionary<GameObject, Pool>();
        Initialize(playerPrejectilePools);//初始化对象池
        Initialize(enemyPrejectilePools);
        Initialize(enemyPools);
    }

    #if UNITY_EDITOR
    private void OnDestroy()
    {
        CheckPoolSize(playerPrejectilePools);
        CheckPoolSize(enemyPrejectilePools);
        CheckPoolSize(enemyPools);
    }
    #endif

    void CheckPoolSize(Pool[] pools)
    {//计算对象池的合理大小
        foreach (var pool in pools)
        {
            if (pool.RuntimeSize > pool.Size)
            {
                Debug.LogWarning(
                    string.Format("Pool: {0} has a runtime size {1} bigger than its initial size {2}",
                    pool.Prefab.name,
                    pool.RuntimeSize,
                    pool.Size));
            }
        }
    }

    private void Initialize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
#if UNITY_EDITOR //条件编译，只在目标平台上编译
            if (dictionary.ContainsKey(pool.Prefab))
            {
                Debug.LogError("Same prefab in multiple pools! Prefab: " + pool.Prefab.name);
                continue;
            }
#endif

            dictionary.Add(pool.Prefab, pool);//初始化每一个对象池之前，将该对象池及其对应的预制体加入到字典dictionary中

            Transform poolParentTF = new GameObject("Pool: " + pool.Prefab.name).transform;//对象池的父物体

            poolParentTF.parent = transform;//对象池的父物体的父物体时Pool Maneger物体
            pool.Initialize(poolParentTF);
        }
    }

    /// <summary>
    /// 根据传入的<param name="prefab"></param>参数，返回对象池中预备好的游戏对象
    /// </summary>
    /// <param name="prefab">
    /// 指定的游戏对象预制体
    /// </param>
    /// <returns>
    /// 对象池中预备好的游戏对象
    /// </returns>
    public static GameObject Release(GameObject prefab)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool Maneger could not find prefab: " + prefab.name);

            return null;
        }
#endif

        return dictionary[prefab].PreparedObject();
    }

    public static GameObject Release(GameObject prefab, Vector3 position)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool Maneger could not find prefab: " + prefab.name);

            return null;
        }
#endif

        return dictionary[prefab].PreparedObject(position);
    }

    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool Maneger could not find prefab: " + prefab.name);

            return null;
        }
#endif

        return dictionary[prefab].PreparedObject(position,rotation);
    }

    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 localScale)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool Maneger could not find prefab: " + prefab.name);

            return null;
        }
#endif

        return dictionary[prefab].PreparedObject(position, rotation, localScale);
    }
}