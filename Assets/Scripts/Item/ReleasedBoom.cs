using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleasedBoom : MonoBehaviour
{
    [SerializeField] float boomRadius;
    [SerializeField] int damage;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, boomRadius);
        foreach (var collider2D in collider2Ds)
        {
            if (collider2D.CompareTag("RoomLayout"))
            {
                collider2D.gameObject.SetActive(false);
            }
            CharacterStatus characterStatus;
            if (collider2D.gameObject.TryGetComponent<CharacterStatus>(out characterStatus))
            {
                characterStatus.TakeDamage(damage);
            }
        }
        gameObject.SetActive(false);
    }
}
