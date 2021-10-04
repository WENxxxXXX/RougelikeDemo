using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField] float fadeTime = 0.3f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerStatus>().boomNumber++;
            StartCoroutine(FadeCoroutine(fadeTime));
        }
    }

    IEnumerator FadeCoroutine(float fadeTime)
    {
        yield return new WaitForSeconds(fadeTime);
        gameObject.SetActive(false);
    }
}
