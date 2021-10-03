using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField] int maxHealth;
    public int currentHealth;
    public int damage;

    protected void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(CharacterStatus attacker, CharacterStatus defender)
    {
        defender.currentHealth = defender.currentHealth - attacker.damage;
        Animator animator;
        if (defender.TryGetComponent<Animator>(out animator))
        {
            animator.SetTrigger("hit");
        }

        if (defender.currentHealth <= 0)
        {
            defender.Die();
        }
    }

    protected virtual void Die()//死亡
    {
    }
}
