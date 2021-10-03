using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatus : CharacterStatus
{
    public event UnityAction onPlayerDie;
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
}
