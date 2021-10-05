using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] BossHealthBarUI bossHealthBarUI;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Room>(out Room room) && room.roomType == RoomType.BossRoom)
        {
            bossHealthBarUI.GetComponent<Canvas>().enabled = true;
            bossHealthBarUI.bossList = room.bossList;
            bossHealthBarUI.holePosition = room.transform.position;
        }
    }
}
