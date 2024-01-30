using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private bool gamePaused = false;
    [SerializeField] private bool selectingCards = false;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject cardGenerationCanvas;
    [SerializeField] private CustomCardGenerator cardGenerator;

    [SerializeField] private TextMeshProUGUI balloonText;

    [SerializeField] private int balloons = 0;

    private void Start()
    {
        Instance = this;
        UnpauseGame();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!gamePaused )
            {
                PauseGame();
                OpenPauseMenu();
            }
            else if(gamePaused && !selectingCards)
            {
                UnpauseGame();
                ClosePauseMenu();
            }
        }
    }

    public void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0f;

    }
    public void UnpauseGame()
    {
        gamePaused = false;
        Time.timeScale = 1.0f;
    }

    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
    }

    public void OpenCardGenerateUI()
    {
        selectingCards = true;
        PauseGame();
        cardGenerationCanvas.SetActive(true);
    }

    public void CloseCardGenerateUI()
    {
        selectingCards = false;
        cardGenerator.ClearAllCards();
        UnpauseGame();
        cardGenerationCanvas.SetActive(false);
    }


    public int GetBalloons()
    {
        return balloons;
    }

    public void UseBalloons(int balloonsUsed)
    {
        balloons = (balloons >= balloonsUsed) ? balloons-balloonsUsed : balloons;
    }

    public void CollectBalloon()
    {
        balloons++;
        balloonText.text = "Balloons - " + balloons.ToString();

    }
}
