using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : CharacterStatus
{
    private void Update()
    {
        if (currentHealth <= 15)
        {
            transform.GetChild(3).gameObject.SetActive(false);
        }
        if (currentHealth <= 8)
        {
            transform.GetChild(2).gameObject.SetActive(false);
        }
        if (currentHealth <= 3)
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        if (currentHealth <= 0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
