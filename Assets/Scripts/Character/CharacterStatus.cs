using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int damage;
    bool isInvincible = false;
    [SerializeField] Material material;

    protected void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(CharacterStatus attacker, CharacterStatus defender)
    {
        if (defender.isInvincible) return;

        defender.currentHealth = defender.currentHealth - attacker.damage;
        Animator animator;
        if (defender.TryGetComponent<Animator>(out animator))
        {
            animator.SetTrigger("hit");
        }
        if (defender.CompareTag("Player"))
        {
            defender.StartCoroutine(nameof(ShortInvincibleCoroutine));
        }

        if (defender.currentHealth <= 0)
        {
            defender.Die();
        }
    }

    public virtual void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth = currentHealth - damage;
        Animator animator;
        if (TryGetComponent<Animator>(out animator))
        {
            animator.SetTrigger("hit");
        }
        if (CompareTag("Player"))
        {
            StartCoroutine(nameof(ShortInvincibleCoroutine));
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()//死亡
    {
    }

    IEnumerator ShortInvincibleCoroutine()
    {
        isInvincible = true;
        bool temp = true;
        for (int i = 0; i < 10; i++)
        {
            material.color = temp ? Color.white : Color.red;
            temp = !temp;
            yield return new WaitForSeconds(0.1f);
        }
        material.color = Color.white;
        isInvincible = false;
    }
}
