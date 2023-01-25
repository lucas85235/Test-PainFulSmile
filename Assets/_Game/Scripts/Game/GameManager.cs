using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemysPrefab;
    [SerializeField] private float enemySpawnInterval = 1f;

    [Tooltip("Duration of the game in seconds")]
    [SerializeField] private float gameDuration = 180f;
    [SerializeField] private int score = 0;

    private float _gameTimer;
    private float _enemyTimer;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
        _gameTimer = gameDuration;
        _enemyTimer = enemySpawnInterval;
    }

    private void FixedUpdate()
    {
        _gameTimer -= Time.fixedDeltaTime;

        if (_gameTimer <= 0)
        {
            Debug.Log("Game Over! Your final score is: " + score);
            return;
        }

        _enemyTimer -= Time.fixedDeltaTime;
        if (_enemyTimer <= 0)
        {
            var enemy = enemysPrefab[Random.Range(0, enemysPrefab.Length)];
            Instantiate(enemy, new Vector3(Random.Range(-8f, 8f), 0, 0), Quaternion.identity);
            _enemyTimer = enemySpawnInterval;
        }
    }

    public void AddScore()
    {
        score++;
    }
}
