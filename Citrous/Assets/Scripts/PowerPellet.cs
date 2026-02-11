using UnityEngine;

public class PowerPellet : MonoBehaviour
{
    public int scoreValue = 50;

    void Start()
    {
        GameManager.Instance.RegisterPellet();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        GameManager.Instance.AddScore(scoreValue);
        GameManager.Instance.ActivatePowerMode();
        GameManager.Instance.PelletEaten();
        Destroy(gameObject);
    }
}

