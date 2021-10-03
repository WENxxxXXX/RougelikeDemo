using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Aiming : Projectile
{
    protected override void OnEnable()
    {
        StartCoroutine(nameof(MoveDirectionCoroutine));
        base.OnEnable();
    }

    IEnumerator MoveDirectionCoroutine()//等待一帧，获取精确的位置
    {
        yield return null;

        if (target.activeSelf)
        {
            moveDirection = (target.transform.position - transform.position).normalized;
        }
    }
}
