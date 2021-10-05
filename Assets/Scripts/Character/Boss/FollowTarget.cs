using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float followDistance;
    [SerializeField] float moveSpeed;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, target.position) > followDistance)
        {
            rigid.velocity = (target.position - transform.position).normalized * moveSpeed;
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerStatus>().TakeDamage(1);
        }
    }
}
