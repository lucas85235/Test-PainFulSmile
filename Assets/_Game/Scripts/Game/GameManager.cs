using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SaveOptions))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemysPrefab;

    [Tooltip("Duration of the game in seconds")]
    [SerializeField] private int score = 0;

    private SaveOptions _saveOptions;
    private float _gameDuration;
    private float _enemySpawnInterval;
    private float _gameTimer;
    private float _enemyTimer;
    public int Score { get => score; }

    public static GameManager Instance;

    private void Awake()
    {
        Time.timeScale = 1;
        Instance = this;
        
        _saveOptions = GetComponent<SaveOptions>();
        _saveOptions.Load();
        _gameDuration = _saveOptions.SessionTimerInSeconds;
        _enemySpawnInterval = _saveOptions.SpawTimerInSeconds;

        _gameTimer = _gameDuration;
        _enemyTimer = _enemySpawnInterval;
        HudUI.Instance.UpdateScore(score);
    }

    private void FixedUpdate()
    {
        _gameTimer -= Time.fixedDeltaTime;
        HudUI.Instance.UpdateTimer((int)_gameTimer);

        if (_gameTimer <= 0)
        {
            GameOver();
            return;
        }

        _enemyTimer -= Time.fixedDeltaTime;
        if (_enemyTimer <= 0)
        {
            var enemy = enemysPrefab[Random.Range(0, enemysPrefab.Length)];
            Instantiate(enemy, new Vector3(Random.Range(-8f, 8f), 0, 0), Quaternion.identity);
            _enemyTimer = _enemySpawnInterval;
        }
    }

    public void AddScore()
    {
        score++;
        HudUI.Instance.UpdateScore(score);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator GameOverRoutine()
    {
        HudUI.Instance.GameOverPopup(score);
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }
}
