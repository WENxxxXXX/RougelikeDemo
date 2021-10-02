using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[System.Serializable] public class Pool//未继承MonoBehaviour,需要添加[System.Serializable]才能使用[SerializeField]
{
    public GameObject Prefab => prefab;//Lambda表达式表示的属性 
    public int Size => size;
    public int RuntimeSize => queue.Count;

    [SerializeField] GameObject prefab;
    [SerializeField] int size = 1;

    Queue<GameObject> queue;//用队列实现对象池

    Transform parent;

    #region Initialize//初始化
    public void Initialize(Transform parent)
    {//初始化，生成队列大小对应数量的备用对象并入队
        queue = new Queue<GameObject>(size);
        this.parent = parent;

        for (int i = 0; i < size; i++)
        {
            queue.Enqueue(Copy());
        }
    }

    GameObject Copy()
    {//生成并返回一个禁用的备用对象
        var copy = GameObject.Instantiate(prefab, parent);
        copy.SetActive(false);

        return copy;
    }
    #endregion

    GameObject AvailableObject()
    {//从对象池中获取可用对象
        GameObject availableObject = null;
        if (queue.Count > 0 && !queue.Peek().activeSelf)//有可能出现所有对象都被启用的情况，这时队头的对象使启用状态，不是可用对象
        {
            availableObject = queue.Dequeue();
        }
        else
        {
            availableObject = Copy();
        }
        queue.Enqueue(availableObject);//对象出列后立即入列

        return availableObject;
    }

    public GameObject PreparedObject()
    {//启用可用对象，需要在其他脚本中调用以实现创建对象的功能
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);

        return preparedObject;
    }

    public GameObject PreparedObject(Vector3 position)
    {//启用可用对象，需要在其他脚本中调用以实现创建对象的功能
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;

        return preparedObject;
    }

    public GameObject PreparedObject(Vector3 position, Quaternion rotation)
    {//启用可用对象，需要在其他脚本中调用以实现创建对象的功能
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;

        return preparedObject;
    }

    public GameObject PreparedObject(Vector3 position, Quaternion rotation, Vector3 localScale)
    {//启用可用对象，需要在其他脚本中调用以实现创建对象的功能
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        preparedObject.transform.localScale = localScale;

        return preparedObject;
    }
    //可用，但这里使用另一种方法：对象出列后立即入列
    /*public void Return(GameObject gameObject)
    {
        queue.Enqueue(gameObject);
    }*/
}
