using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        EnemyMovement enemy = GetComponentInParent<EnemyMovement>();

        if (GameManager.Instance.powerMode)
            enemy.Die();
        else
            GameManager.Instance.PlayerDied();
    }
}

