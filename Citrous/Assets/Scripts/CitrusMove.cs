using UnityEngine;
using UnityEngine.InputSystem;

public class CitrusMove : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody2D rb;
    Vector2 moveDirection;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = Vector2.right;
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
        }
        else if (verticalInput != 0)
        { 
            moveDirection = new Vector2(0,verticalInput);
        }
        
    }
    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }
}
