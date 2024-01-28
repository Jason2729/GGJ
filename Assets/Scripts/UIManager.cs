using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private bool gamePaused = false;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject effectsCanvas;
    [SerializeField] private int balloons;
    [SerializeField] private TextMeshProUGUI balloonText;

    private void Start()
    {
        Instance = this;
        UnpauseGame();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gamePaused)
            {
                UnpauseGame() ;
            }
            else { PauseGame(); }
        }   
    }
    public void PauseGame()
    {
        gamePaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; 

    }

    public void UnpauseGame()
    {
        gamePaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void UpdateBalloonCounter(int balloons)
    {
        balloonText.text = "Balloons: " + balloons.ToString();
    }

    public void ShowEffectsCanvas()
    {
        effectsCanvas.SetActive(true);
    }

    public void HideEffectsCanvas()
    {
        effectsCanvas.SetActive(false);
    }
}
