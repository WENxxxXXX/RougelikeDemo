using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : CharacterStatus
{
    public float moveSpeed;
    public float spottingDistance;
    public float attackRange;
    public string attackTargetTag;
    [SerializeField] float fadeTime;
    
    protected override void Die()
    {
        base.Die();
        StartCoroutine(FadeCoroutine(fadeTime));
    }

    IEnumerator FadeCoroutine(float fadeTime)
    {
        yield return new WaitForSeconds(fadeTime);
        gameObject.SetActive(false);
    }
}
