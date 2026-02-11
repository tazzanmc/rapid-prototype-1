using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int scoreValue = 10;

    void Start()
    {
        GameManager.Instance.RegisterPellet();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        GameManager.Instance.AddScore(scoreValue);
        GameManager.Instance.PelletEaten();
        Destroy(gameObject);
    }
}

