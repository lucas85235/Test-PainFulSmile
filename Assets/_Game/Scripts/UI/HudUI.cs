using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HudUI : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("GameOver")]
    [SerializeField] private Popup gameOverPopup;
    [SerializeField] private TextMeshProUGUI gameOverTotalScoreText;
    [SerializeField] private Button gameOverRestartButton;
    [SerializeField] private Button gameOverMenuButton;

    public static HudUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameOverRestartButton.onClick.AddListener(RestartButton);
        gameOverMenuButton.onClick.AddListener(MenuButton);
    }

    private void RestartButton()
    {
        SceneManager.LoadScene("Game");
    }

    private void MenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void UpdateScore(int score)
    {
        scoreText.text = $"SCORE: {score}";
    }

    public void GameOverPopup(int score)
    {
        gameOverTotalScoreText.text = $"TOTAL SCORE {score}";
        gameOverPopup.Open();
    }
}
