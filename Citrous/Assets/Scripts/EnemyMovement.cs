using UnityEngine;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;
    public float returnSpeed = 5f;
    public LayerMask wallLayer;
    public float rayDistance = 0.6f;

    [Header("Identity")]
    public EnemyType enemyType;

    [Header("Sprites")]
    public Sprite returnSprite;

    [Header("Animation")]
    public RuntimeAnimatorController normalController;
    public RuntimeAnimatorController scaredController;

    Rigidbody2D rb;
    Collider2D bodyCollider;
    Collider2D triggerCollider;
    Animator animator;
    SpriteRenderer spriteRenderer;

    Vector2 currentDirection;
    Vector2 spawnPosition;

    bool isDead;
    bool isReturning;
    bool isReleased;

    Sprite normalSprite;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bodyCollider = GetComponent<Collider2D>();
        triggerCollider = GetComponentInChildren<Collider2D>();

        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        spawnPosition = transform.position;
        normalSprite = spriteRenderer.sprite;

        GameManager.Instance.RegisterEnemy(this);
        HardReset();
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.UnregisterEnemy(this);
    }

    void FixedUpdate()
    {
        if (isReturning)
        {
            Vector2 dir = (spawnPosition - rb.position).normalized;
            rb.linearVelocity = dir * returnSpeed;

            if (Vector2.Distance(rb.position, spawnPosition) < 0.1f)
                FinishReturn();

            return;
        }

        if (!isReleased)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = currentDirection * speed;

        if (IsBlocked(currentDirection))
            ChooseNewDirection();
    }

    void Update()
    {
        if (animator != null)
            animator.runtimeAnimatorController =
                GameManager.Instance.powerMode ? scaredController : normalController;
    }

    // ======================
    // RESET / RELEASE
    // ======================

    public void HardReset()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = spawnPosition;

        isDead = false;
        isReturning = false;
        isReleased = false;

        spriteRenderer.enabled = false;
        spriteRenderer.sprite = normalSprite;

        currentDirection = Vector2.up;

        GameManager.Instance.QueueRelease(this);
    }

    public void AllowRelease()
    {
        isReleased = true;
        spriteRenderer.enabled = true;

        currentDirection = Vector2.up;
        if (IsBlocked(currentDirection))
            ChooseNewDirection();
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        isReturning = true;

        triggerCollider.enabled = false;
        animator.enabled = false;
        spriteRenderer.sprite = returnSprite;
        spriteRenderer.enabled = true;
    }

    void FinishReturn()
    {
        triggerCollider.enabled = true;
        animator.enabled = true;

        HardReset();
    }

    // ======================
    // MOVEMENT LOGIC
    // ======================

    void ChooseNewDirection()
    {
        List<Vector2> options = new List<Vector2>();
        Vector2[] dirs = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        foreach (Vector2 dir in dirs)
        {
            if (dir == -currentDirection) continue;
            if (!IsBlocked(dir)) options.Add(dir);
        }

        currentDirection = options.Count > 0
            ? options[Random.Range(0, options.Count)]
            : -currentDirection;
    }

    bool IsBlocked(Vector2 dir)
    {
        Physics2D.queriesHitTriggers = false;
        Vector2 origin = bodyCollider.bounds.center;
        RaycastHit2D hit = Physics2D.Raycast(origin, dir, rayDistance, wallLayer);
        return hit.collider != null;
    }
}










