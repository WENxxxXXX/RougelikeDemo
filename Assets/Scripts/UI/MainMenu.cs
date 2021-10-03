using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    Canvas mainMenuCanvas;
    [SerializeField] PlayerStatus playerStatus;

    private void Awake()
    {
        mainMenuCanvas = GetComponent<Canvas>();
        mainMenuCanvas.enabled = false;
    }

    private void OnEnable()
    {
        playerStatus.onPlayerDie += EnableMainMenu;
    }

    private void OnDisable()
    {
        playerStatus.onPlayerDie -= EnableMainMenu;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainMenuCanvas.enabled = !mainMenuCanvas.enabled;
        }
    }

    void EnableMainMenu()
    {
        mainMenuCanvas.enabled = true;
    }
}
