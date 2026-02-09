using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    void onTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.Instance.powerMode)
        {
            GetComponentInParent<Enemy>().Die();
        }
        else
        {
            GameManager.Instance.LoseLife();
        }
    }
}
