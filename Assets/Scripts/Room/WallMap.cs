using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class WallMap : MonoBehaviour
{
    GameObject mapSprite;

    private void Awake()
    {
        mapSprite = transform.GetChild(0).gameObject;
        mapSprite.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            mapSprite.SetActive(true);
    }

}
