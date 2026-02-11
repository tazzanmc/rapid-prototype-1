using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Level Progression")]
    public int totalPellets = 0;
    public GameObject gameOverScreen;
    public GameObject winScreen;

    [Header("Score")]
    public int score = 0;
    public Text scoreText;

    [Header("Lives")]
    public int lives = 3;
    public SpriteRenderer life1;
    public SpriteRenderer life2;
    public SpriteRenderer life3;

    [Header("Power Mode")]
    public bool powerMode = false;
    public float powerDuration = 8f;

    [Header("Ghost Release")]
    public float releaseInterval = 2f;

    CitrusMove player;
    public int currentLevel = 1;
    public int finalLevel = 2;
    List<EnemyMovement> activeEnemies = new List<EnemyMovement>();
    Queue<EnemyMovement> releaseQueue = new Queue<EnemyMovement>();
    bool releasing = false;
    bool isGameOver = false;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    public void RegisterPellet()
    {
        totalPellets++;
    }
    public void PelletEaten()
    {
        totalPellets--;

        if (totalPellets <= 0)
            LevelComplete();
    }
    void ShowWinScreen()
    {
        if (winScreen != null)
            winScreen.SetActive(true);

        Time.timeScale = 0f;
    }
    void LevelComplete()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int lastIndex = SceneManager.sceneCountInBuildSettings - 1;

        if (currentIndex < lastIndex)
        {
            SceneManager.LoadScene(currentIndex + 1);
        }
        else
        {
            ShowWinScreen();
        }
    }
    void GameOver()
    {
        isGameOver = true;
        Debug.Log("GAME OVER");

        StartCoroutine(RestartRoutine());
    }

    IEnumerator RestartRoutine()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Start()
    {
        player = Object.FindFirstObjectByType<CitrusMove>();
        UpdateScoreUI();
        UpdateLivesUI();

        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        if (winScreen != null) winScreen.SetActive(false);
    }

    // ======================
    // SCORE
    // ======================

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    // ======================
    // LIVES
    // ======================

    public void PlayerDied()
    {
        if (isGameOver) return;

        lives--;
        UpdateLivesUI();

        if (lives <= 0)
        {
            GameOver();
            return;
        }

        ResetRound();
    }
    void ShowGameOverScreen()
    {
        if (gameOverScreen != null)
            gameOverScreen.SetActive(true);

        Time.timeScale = 0f;
    }
    void UpdateLivesUI()
    {
        if (life1 != null) life1.enabled = lives > 0;
        if (life2 != null) life2.enabled = lives > 1;
        if (life3 != null) life3.enabled = lives > 2;
    }

    void ResetRound()
    {
        if (player == null)
            player = Object.FindFirstObjectByType<CitrusMove>();

        player.HardReset();

        releaseQueue.Clear();

        foreach (EnemyMovement enemy in activeEnemies)
        {
            enemy.HardReset();
        }
    }

    // ======================
    // POWER MODE
    // ======================

    public void ActivatePowerMode()
    {
        powerMode = true;
        CancelInvoke(nameof(EndPowerMode));
        Invoke(nameof(EndPowerMode), powerDuration);
    }

    void EndPowerMode()
    {
        powerMode = false;
    }

    // ======================
    // ENEMY REGISTRY
    // ======================

    public void RegisterEnemy(EnemyMovement enemy)
    {
        if (!activeEnemies.Contains(enemy))
            activeEnemies.Add(enemy);
    }

    public void UnregisterEnemy(EnemyMovement enemy)
    {
        activeEnemies.Remove(enemy);
    }

    // ======================
    // RELEASE QUEUE
    // ======================

    public void QueueRelease(EnemyMovement enemy)
    {
        if (!releaseQueue.Contains(enemy))
            releaseQueue.Enqueue(enemy);

        if (!releasing)
            StartCoroutine(ReleaseRoutine());
    }

    IEnumerator ReleaseRoutine()
    {
        releasing = true;

        while (releaseQueue.Count > 0)
        {
            EnemyMovement next = releaseQueue.Dequeue();
            next.AllowRelease();
            yield return new WaitForSeconds(releaseInterval);
        }

        releasing = false;
    }
}







