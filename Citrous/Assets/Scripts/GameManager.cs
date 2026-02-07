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
    public void AddScore(int amount)
    {
        score += amount;
        scoretext.text = "Score: " + score;
    }
    public void LoseLife()
    {
        lives--;
        Debug.Log("Lives Left: " + lives);
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
    void Awake()
    {
        Instance = this;
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
