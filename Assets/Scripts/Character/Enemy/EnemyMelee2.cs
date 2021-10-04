using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee2 : MonoBehaviour
{
    [SerializeField] float minMoveCD;
    [SerializeField] float maxMoveCD;
    [SerializeField] float minMovePause;
    [SerializeField] float maxMovePause;
    [SerializeField] float moveSpeed = 10;
    [HideInInspector] public List<Vector2> moveDirectionList;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        moveDirectionList = new List<Vector2>();
        Initialize();
        StartCoroutine(nameof(RandomMoveCoroutine));
    }

    public void Initialize()
    {
        moveDirectionList.Clear();
        moveDirectionList.Add(new Vector2(1, 0));
        moveDirectionList.Add(new Vector2(-1, 0));
        moveDirectionList.Add(new Vector2(0, 1));
        moveDirectionList.Add(new Vector2(0, -1));
        moveDirectionList.Add(new Vector2(0.707f, 0.707f));
        moveDirectionList.Add(new Vector2(0.707f, -0.707f));
        moveDirectionList.Add(new Vector2(-0.707f, 0.707f));
        moveDirectionList.Add(new Vector2(-0.707f, -0.707f));
    }

    IEnumerator RandomMoveCoroutine()
    {
        while (isActiveAndEnabled)
        {
            rigid.velocity = moveSpeed * moveDirectionList
                [Random.Range(0, moveDirectionList.Count)];
            yield return new WaitForSeconds(Random.Range(minMoveCD, maxMoveCD));
            rigid.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(Random.Range(minMovePause, maxMovePause));
        }
    }
}
