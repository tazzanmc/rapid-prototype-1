using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Text scoretext;
    public int score = 0;
    public void AddScore(int amount)
    {
        score += amount;
        scoretext.text = "Score: " + score;
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
