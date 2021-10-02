using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField] int maxHealth;
    public int currentHealth;
    public float moveSpeed;
    public float spottingDistance;
    public float attackRange;
    public int damage;
    public string attackTargetTag;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(CharacterStatus attacker, CharacterStatus defender)
    {
        defender.currentHealth = defender.currentHealth - attacker.damage;

        if (defender.currentHealth <= 0)
        {
            defender.Die();
        }
    }

    public virtual void Die()//死亡
    {
        //EnemyManager.Instance.RemoveFromList(gameObject);
        gameObject.SetActive(false);
        //TODO:继承
    }
}
