using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField] GameObject key, loveBlood, boom;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RandomInstantiate(key);
            RandomInstantiate(loveBlood);
            RandomInstantiate(boom);
            gameObject.SetActive(false);
        }
    }

    void RandomInstantiate(GameObject go)
    {
        int temp = Random.Range(0, 2);
        for (int i = 0; i < temp; i++)
        {
            Instantiate(go, transform.position + new Vector3(Random.Range(0.3f, 1.3f),
                Random.Range(0.3f, 1.3f), 0), Quaternion.identity);
        }
    }
}
