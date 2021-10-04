using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : CharacterStatus
{
    public float moveSpeed;
    public float maxMoveSpeed;
    public float spottingDistance;
    public float attackRange;
    public string attackTargetTag;
    [SerializeField] float fadeTime;

    protected override void Die()
    {
        base.Die();
        StartCoroutine(FadeCoroutine(fadeTime));
        EnemyManager.Instance.RemoveFromList(gameObject);
    }

    IEnumerator FadeCoroutine(float fadeTime)
    {
        yield return new WaitForSeconds(fadeTime);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TakeDamage(this, other.gameObject.GetComponent<CharacterStatus>());
        }
    }
}
