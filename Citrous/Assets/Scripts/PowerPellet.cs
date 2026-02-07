using UnityEngine;

public class PowerPellet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.Instance.ActivatePowerMode();
        Destroy(gameObject);
    }
}
