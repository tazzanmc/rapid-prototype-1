using UnityEngine;
using UnityEngine.InputSystem;

public class CitrusMove : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody2D rb;
    Vector2 moveDirection;
    SpriteRenderer spriteRenderer;
    public Sprite pacRight;
    public Sprite pacLeft;
    public Sprite pacDown;
    public Sprite pacUp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = Vector2.right;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = pacRight;
        spriteRenderer.flipX = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            horizontalInput = -1f;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            horizontalInput = 1f;
        }
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
        {
            verticalInput = 1f;
        }
        else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
        {
            verticalInput = -1f;
        }
        if (horizontalInput != 0)
        {
            moveDirection = new Vector2(horizontalInput, 0);

            spriteRenderer.sprite = pacRight;
            spriteRenderer.flipX = horizontalInput < 0;
        }
        else if (verticalInput != 0)
        { 
            moveDirection = new Vector2(0,verticalInput);

            if (verticalInput > 0)
            {
                spriteRenderer.sprite = pacUp;
            }
            else
            {
                spriteRenderer.sprite = pacDown;
            }
            spriteRenderer.flipX = false;
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (GameManager.Instance.powerMode)
            {
                other.GetComponent<Enemy>().Die();
            }
            else
            {
                GameManager.Instance.LoseLife();
            }
        }
    }
    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }
}
