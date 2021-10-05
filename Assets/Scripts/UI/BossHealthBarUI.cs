using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossHealthBarUI : MonoBehaviour
{
    [HideInInspector] public List<GameObject> bossList;
    [SerializeField] Image bossHealthBar;
    [SerializeField] GameObject hole;
    float maxHealth = 0f;
    float currentHealth = 0f;
    [HideInInspector] public Vector3 holePosition;
    bool isOk = false;

    private void OnEnable()
    {
        maxHealth = 0f;
        currentHealth = 0f;
    }

    private void Update()
    {
        if (bossList.Count > 0)
        {
            maxHealth = 0f;
            currentHealth = 0f;
            foreach (var boss in bossList)
            {
                maxHealth += boss.GetComponent<BossStatus>().maxHealth;
                currentHealth += boss.GetComponent<BossStatus>().currentHealth;
            }

            bossHealthBar.fillAmount = currentHealth / maxHealth;
            if (currentHealth == 0 && !isOk)
            {
                Instantiate(hole, holePosition, Quaternion.identity);
                isOk = true;
            }
        }
    }
}
