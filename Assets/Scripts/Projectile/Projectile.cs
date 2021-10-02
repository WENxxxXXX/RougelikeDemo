using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public Vector2 ownerVelocity = new Vector2(0, 0);
    [SerializeField] float velocityFactor = 3f;
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected Vector2 moveDirection;

    protected GameObject target;
    protected CharacterStatus owner;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        StartCoroutine(MoveDirectly());
    }

    IEnumerator MoveDirectly()
    {
        yield return null;
        rigid.velocity = moveSpeed * moveDirection + ownerVelocity * velocityFactor;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)//子弹命中时：角色受伤；击中特效；禁用子弹对象
    {
        if(collision.gameObject.TryGetComponent<CharacterStatus>(out CharacterStatus defender))
        {
            defender.TakeDamage(owner, defender);
            // PoolManager.Release(hitVFX, collision.GetContact(0).point, Quaternion.LookRotation(collision.GetContact(0).normal));
            // AudioManager.Instance.PlayRandomSFX(hitSFX);
            gameObject.SetActive(false);
        }
    }

    public virtual void SetTarget(GameObject target)
    {
        this.target = target;
    }

    public virtual void SetOwner(CharacterStatus owner)
    {
        this.owner = owner;
    }
}
