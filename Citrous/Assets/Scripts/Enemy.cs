using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    public RuntimeAnimatorController normalController;
    public RuntimeAnimatorController scaredController;
    bool isScared = false;
    public float tileSize = 1f;
    public float snapThreshold = 0.05f;
    public float speed = 3f;
    Vector2 currentDirection = Vector2.right;
    Rigidbody2D rb;
    public LayerMask wallLayer;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    bool IsDirectionBlocked(Vector2 direction)
    {
        Vector2 origin = GetSnappedPosition(transform.position);
        float distance = tileSize * 0.6f;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, wallLayer);

        return hit.collider != null;
    }
    Vector2 GetSnappedPosition(Vector2 position)
    {
        float x = Mathf.Round(position.x / tileSize) * tileSize;
        float y = Mathf.Round(position.y / tileSize) * tileSize;
        return new Vector2(x, y);
    }
    bool IsCenteredOnTile()
    {
        Vector2 snapped = GetSnappedPosition(transform.position);
        return Vector2.Distance(transform.position, snapped) < snapThreshold;
    }
    void FixedUpdate()
    {
        if (IsCenteredOnTile())
        {
            transform.position = GetSnappedPosition(transform.position);
        }
        rb.linearVelocity = currentDirection * speed;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.powerMode)
        {
            animator.runtimeAnimatorController = scaredController;
            isScared = true;
        }
        else
        {
            animator.runtimeAnimatorController = normalController;
            isScared = false;
        }
    }
    public void Die()
    {
        gameObject.SetActive(false);
    }
}
