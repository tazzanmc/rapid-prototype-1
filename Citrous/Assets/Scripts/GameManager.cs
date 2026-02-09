using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Text scoretext;
    public int score = 0;
    public int lives = 3;
    public bool powerMode = false;
    public float powerDuration = 8f;
    public static GameManager Instance;
    Vector2 playerStartPostiton;
    public SpriteRenderer Life1;
    public SpriteRenderer Life2;
    public SpriteRenderer Life3;
    public void AddScore(int amount)
    {
        score += amount;
        scoretext.text = "Score: " + score;
    }
    public void LoseLife()
    {
        lives--;
        UpdateLivesUI();
        if(lives <= 0)
        {
            Debug.Log("Game Over");
        }
        else
        {
            RespawnPlayer();
        }
    }
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
    void UpdateLivesUI()
    {
        Life1.enabled = lives > 0;
        Life2.enabled = lives > 1;
        Life3.enabled = lives > 2;
    }
    void RespawnPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = playerStartPostiton;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
    }
    void Awake()
    {
        Instance = this;
        playerStartPostiton = GameObject.FindGameObjectWithTag("Player").transform.position;
        UpdateLivesUI();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
