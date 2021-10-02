using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;

    Rigidbody2D rigid;
    Animator animator;
    Vector2 movement;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        if (movement.magnitude >= 1f)
        {
            movement = movement.normalized;
        }
        SwitchAnimation();

        if (movement.x >= 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        //rigid.MovePosition(rigid.position + movement * speed * Time.fixedDeltaTime);
        rigid.velocity = movement * speed;
    }

    void SwitchAnimation()
    {
        animator.SetFloat("speed", movement.magnitude);
    }
}
