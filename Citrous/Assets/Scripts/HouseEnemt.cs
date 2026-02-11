using UnityEngine;

public class HouseEnemy : MonoBehaviour { public EnemyType enemyType; SpriteRenderer spriteRenderer; void Awake() { spriteRenderer = GetComponent<SpriteRenderer>(); } public void Hide() { spriteRenderer.enabled = false; } public void Show() { spriteRenderer.enabled = true; } }



