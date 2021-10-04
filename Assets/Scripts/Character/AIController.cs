using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject projectile;
    public FSMSystem fsm;
    protected string aiConfigFile;
    [HideInInspector] public GameObject target;
    [SerializeField] float minAttackCD;
    [SerializeField] float maxAttackCD;
    [SerializeField] float minMoveCD;
    [SerializeField] float maxMoveCD;
    [HideInInspector] public List<Vector2> moveDirectionList;
    EnemyStatus enemyStatus;
    Rigidbody2D rigid;

    private void Awake()
    {
        enemyStatus = GetComponent<EnemyStatus>();
        rigid = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        moveDirectionList = new List<Vector2>();
        Initialize();
        StartCoroutine(nameof(RandomMoveCoroutine));

        fsm = new FSMSystem(aiConfigFile, gameObject);
        target = null;

    }

    public void Initialize()
    {
        moveDirectionList.Clear();
        moveDirectionList.Add(new Vector2(1, 0));
        moveDirectionList.Add(new Vector2(-1, 0));
        moveDirectionList.Add(new Vector2(0, 1));
        moveDirectionList.Add(new Vector2(0, -1));
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
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        while (true)
        {
            var temp = PoolManager.Release(projectile,
                firePoint.position, Quaternion.identity);
            temp.GetComponent<Projectile>().SetTarget(target);
            temp.GetComponent<Projectile>().SetOwner(GetComponent<CharacterStatus>());
            yield return new WaitForSeconds(Random.Range(minAttackCD, maxAttackCD));
        }
    }

    IEnumerator RandomMoveCoroutine()
    {
        while (isActiveAndEnabled)
        {
            rigid.velocity = enemyStatus.moveSpeed * moveDirectionList
                [Random.Range(0, moveDirectionList.Count)];
            yield return new WaitForSeconds(Random.Range(minMoveCD, maxMoveCD));
        }
    }
}
