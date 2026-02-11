using UnityEngine;
using UnityEngine.InputSystem;

public class CitrusMove : MonoBehaviour
{
    public float speed = 5f;

    Rigidbody2D rb;
    Vector2 moveDirection;
    Vector2 spawnPosition;

    SpriteRenderer spriteRenderer;

    public Sprite pacRight;
    public Sprite pacUp;
    public Sprite pacDown;

    bool isDead = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPosition = transform.position;
        moveDirection = Vector2.right;
    }

    void Update()
    {
        if (isDead) return;

        float h = 0;
        float v = 0;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) h = -1;
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) h = 1;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) v = 1;
        else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) v = -1;

        if (h != 0)
        {
            moveDirection = new Vector2(h, 0);
            spriteRenderer.sprite = pacRight;
            spriteRenderer.flipX = h < 0;
        }
        else if (v != 0)
        {
            moveDirection = new Vector2(0, v);
            spriteRenderer.sprite = v > 0 ? pacUp : pacDown;
            spriteRenderer.flipX = false;
        }
    }

    void FixedUpdate()
    {
        if (isDead)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = moveDirection * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;
        if (!other.CompareTag("Enemy")) return;

        if (GameManager.Instance.powerMode)
        {
            other.GetComponent<EnemyMovement>().Die();
            GameManager.Instance.AddScore(200); // 👻 ghost score
        }
        else
        {
            isDead = true; // 🔒 LOCK DEATH
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void HardReset()
    {
        transform.position = spawnPosition;
        rb.linearVelocity = Vector2.zero;

        isDead = false; // 🔓 UNLOCK
        moveDirection = Vector2.right;
        spriteRenderer.sprite = pacRight;
        spriteRenderer.flipX = false;
    }
}






