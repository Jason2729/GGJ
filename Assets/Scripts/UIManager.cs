using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private bool gamePaused = false;
    [SerializeField] private GameObject pauseMenu;

    private void Start()
    {
        Instance = this;
        UnpauseGame();
    }
    public void PauseGame()
    {
        gamePaused = true;
        pauseMenu.SetActive(true);

    }

    public void UnpauseGame()
    {
        gamePaused = false;
        pauseMenu.SetActive(false);
    }
}
