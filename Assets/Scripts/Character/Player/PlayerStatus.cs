using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatus : CharacterStatus
{
    public event UnityAction onPlayerDie;
    public int keyNumber = 0;
    public int boomNumber = 0;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void Die()
    {
        base.Die();
        animator.SetBool("die", true);
        onPlayerDie.Invoke();
        //TODO:广播
        Time.timeScale = 0;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Door") && keyNumber > 0)
        {
            keyNumber--;
            other.collider.enabled = false;
        }
    }
}
