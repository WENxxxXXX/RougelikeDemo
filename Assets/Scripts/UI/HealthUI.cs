using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Text currentHealthText;
    [SerializeField] Text maxHealthText;
    [SerializeField] Text boomMountText;
    [SerializeField] Text keyMountText;
    [SerializeField] PlayerStatus player;

    private void Update()
    {
        currentHealthText.text = Convert.ToString(player.currentHealth);
        maxHealthText.text = Convert.ToString(player.maxHealth);
        boomMountText.text = Convert.ToString(player.boomNumber);
        keyMountText.text = Convert.ToString(player.keyNumber);
    }
}
