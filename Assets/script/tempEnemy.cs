using UnityEngine;

public class tempEnemy : MonoBehaviour
{
    public bool isControlled = false;
    [SerializeField] float speed = 2.0f;
    [SerializeField] float jumpPower = 0.5f;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlled)
        {
            float moveX = 0f;

            if (Input.GetKey(KeyCode.D))
            {
                moveX += 1f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveX -= 1f;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                rb.linearVelocityY += jumpPower;
            }

            rb.linearVelocityX = moveX * speed;

            Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, new Vector3(transform.position.x, transform.position.y, -100), 0.05f);
        }
    }
}