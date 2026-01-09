using UnityEngine;

public class moving : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    float speed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 newPosition = transform.position;



        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.linearVelocityX = speed;
            rb.linearVelocityY = 0;
            //newPosition.x += 1;
            speed = 5f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.linearVelocityX = -speed;
            rb.linearVelocityY = 0;
            //newPosition.x -= 1;
            speed = 5f;
        }
        if (rb.linearVelocityY == 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.linearVelocityY = speed;
                rb.linearVelocityX = 0;
                speed = 5f;
            }
        }

        //transform.position = newPosition;
    }
}