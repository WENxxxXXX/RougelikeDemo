using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject projectile;
    public FSMSystem fsm;
    protected string aiConfigFile;
    protected GameObject prefab;
    [HideInInspector] public GameObject target;
    public Vector3 initialPosition;

    public virtual void Start()
    {
        fsm = new FSMSystem(aiConfigFile, gameObject);
        target = null;
    }

    public virtual void Update()
    {
        fsm.Update();
    }

    public void StartFireCoroutine()
    {
        StartCoroutine(FireCoroutine());
    }

    public void StopFireCoroutine()
    {
        StopAllCoroutines();
    }

    IEnumerator FireCoroutine()
    {
        while (true)
        {
            var temp = PoolManager.Release(projectile,
                firePoint.position, Quaternion.identity);
            temp.GetComponent<Projectile>().SetTarget(target);
            temp.GetComponent<Projectile>().SetOwner(GetComponent<CharacterStatus>());
            yield return new WaitForSeconds(1f);
        }
    }
}
