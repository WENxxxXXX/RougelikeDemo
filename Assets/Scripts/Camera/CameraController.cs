using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class CameraController : Singleton<CameraController>
{
    Transform target;
    [SerializeField] float speed;

    private void Update()
    {
        if (target != null)
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(target.position.x, target.position.y, transform.position.z), speed * Time.deltaTime);
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }

}
