using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    Vector3[] targetPositions;
    Vector3 target;
    Rigidbody2D rigid;
    float timeInterval = 0f;
    const float TIME_ = 0.1f;
    [SerializeField] float moveSpeed;
    [HideInInspector] public Vector3 centra;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Initialize(centra);
    }

    private void FixedUpdate()
    {
        if (timeInterval > 0f) timeInterval -= Time.fixedDeltaTime;
        if (timeInterval <= 0f) SetTarget();

        rigid.velocity = moveSpeed * (target - transform.position).normalized;
    }

    private void SetTarget()
    {
        if (Vector3.SqrMagnitude(transform.position - targetPositions[0]) < 0.1f)
        {
            if (Mathf.Abs(rigid.velocity.y) > Mathf.Abs(rigid.velocity.x))
            {
                target = targetPositions[6];
            }
            else
                target = targetPositions[1];
        }
        else if (Vector3.SqrMagnitude(transform.position - targetPositions[1]) < 0.1f)
        {
            if (Mathf.Abs(rigid.velocity.x) > Mathf.Abs(rigid.velocity.y))
                target = Random.Range(0, 2) == 0 ? targetPositions[0] : targetPositions[2];
            else if (rigid.velocity.y > 0)
                target = Random.Range(0, 2) == 0 ? targetPositions[0] : targetPositions[7];
            else
                target = Random.Range(0, 2) == 0 ? targetPositions[7] : targetPositions[2];
        }
        else if (Vector3.SqrMagnitude(transform.position - targetPositions[2]) < 0.1f)
        {
            if (Mathf.Abs(rigid.velocity.y) > Mathf.Abs(rigid.velocity.x))
                target = targetPositions[8];
            else
                target = targetPositions[1];
        }
        else if (Vector3.SqrMagnitude(transform.position - targetPositions[3]) < 0.1f)
        {
            if (Mathf.Abs(rigid.velocity.y) > Mathf.Abs(rigid.velocity.x))
                target = targetPositions[6];
            else
                target = targetPositions[4];
        }
        else if (Vector3.SqrMagnitude(transform.position - targetPositions[4]) < 0.1f)
        {
            if (Mathf.Abs(rigid.velocity.x) > Mathf.Abs(rigid.velocity.y))
                target = Random.Range(0, 2) == 0 ? targetPositions[3] : targetPositions[5];
            else if (rigid.velocity.y > 0)
                target = Random.Range(0, 2) == 0 ? targetPositions[3] : targetPositions[7];
            else
                target = Random.Range(0, 2) == 0 ? targetPositions[7] : targetPositions[5];
        }
        else if (Vector3.SqrMagnitude(transform.position - targetPositions[5]) < 0.1f)
        {
            if (Mathf.Abs(rigid.velocity.y) > Mathf.Abs(rigid.velocity.x))
                target = targetPositions[8];
            else
                target = targetPositions[4];
        }
        else if (Vector3.SqrMagnitude(transform.position - targetPositions[6]) < 0.1f)
        {
            if (Mathf.Abs(rigid.velocity.y) > Mathf.Abs(rigid.velocity.x))
                target = Random.Range(0, 2) == 0 ? targetPositions[3] : targetPositions[0];
            else if (rigid.velocity.x > 0)
                target = Random.Range(0, 2) == 0 ? targetPositions[0] : targetPositions[7];
            else
                target = Random.Range(0, 2) == 0 ? targetPositions[7] : targetPositions[3];
        }
        else if (Vector3.SqrMagnitude(transform.position - targetPositions[7]) < 0.1f)
        {
            if (Mathf.Abs(rigid.velocity.y) > Mathf.Abs(rigid.velocity.x))
            {
                if (rigid.velocity.y > 0)
                {
                    float temp = Random.Range(0, 3);
                    if (temp == 0) target = targetPositions[4];
                    if (temp == 1) target = targetPositions[6];
                    if (temp == 2) target = targetPositions[1];
                }
                else
                {
                    float temp = Random.Range(0, 3);
                    if (temp == 0) target = targetPositions[4];
                    if (temp == 1) target = targetPositions[1];
                    if (temp == 2) target = targetPositions[8];
                }
            }
            else
            {
                if (rigid.velocity.x > 0)
                {
                    float temp = Random.Range(0, 3);
                    if (temp == 0) target = targetPositions[6];
                    if (temp == 1) target = targetPositions[1];
                    if (temp == 2) target = targetPositions[8];
                }
                else
                {
                    float temp = Random.Range(0, 3);
                    if (temp == 0) target = targetPositions[4];
                    if (temp == 1) target = targetPositions[6];
                    if (temp == 2) target = targetPositions[8];
                }
            }
        }
        else if (Vector3.SqrMagnitude(transform.position - targetPositions[8]) < 0.1f)
        {
            if (Mathf.Abs(rigid.velocity.y) > Mathf.Abs(rigid.velocity.x))
                target = Random.Range(0, 2) == 0 ? targetPositions[5] : targetPositions[2];
            else if (rigid.velocity.x > 0)
                target = Random.Range(0, 2) == 0 ? targetPositions[2] : targetPositions[7];
            else
                target = Random.Range(0, 2) == 0 ? targetPositions[7] : targetPositions[5];
        }

        timeInterval = TIME_;
    }

    public void Initialize(Vector3 centra)
    {
        targetPositions = new Vector3[]
        {
            new Vector3(7.5f, 3.2f) + centra,  //0 > 1,6
            new Vector3(7.5f, 0f) + centra,    //1 > 0,2,7
            new Vector3(7.5f, -3.2f) + centra, //2 > 1,8

            new Vector3(-7.5f, 3.2f) + centra, //3 > 6,4
            new Vector3(-7.5f, 0f) + centra,   //4 > 3,5,7
            new Vector3(-7.5f, -3.2f) + centra,//5 > 4,8

            new Vector3(0f, 3.2f) + centra,    //6 > 0,3,7
            new Vector3(0f, 0f) + centra,      //7 > 1,4,6,8
            new Vector3(0f, -3.2f) + centra,   //8 > 2,5,,7
        };
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerStatus>().TakeDamage(1);
        }
    }
}
