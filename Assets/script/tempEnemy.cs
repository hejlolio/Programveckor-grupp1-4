using Unity.VisualScripting;
using UnityEngine;

public class tempEnemy : MonoBehaviour
{
    public bool isControlled = false;
    [SerializeField] float speed = 2.0f;
    [SerializeField] float jumpPower = 2f;

    Collider2D playerCollider;
    public LayerMask groundLayerMask;

    Camera cam;

    Rigidbody2D rb;

    float moveX;
    bool jump;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        cam = Camera.main;

        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (IsGrounded()) {
        if (isControlled)
        {
            moveX = 0f;

            if (Input.GetKey(KeyCode.D))
            {
                moveX += 1f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveX -= 1f;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jump = true;
            }

            cam.transform.position = Vector3.Slerp(cam.transform.position, new Vector3(transform.position.x, transform.position.y, -100), 0.05f);
        }
        } 
    }
    void FixedUpdate()
    {
        rb.linearVelocityX = moveX * speed;

        if (jump)
        {
            rb.linearVelocityY += jumpPower;
            jump = false;
        }
    }
    bool IsGrounded()
    {
        if (groundLayerMask == 0)
        {
            return true;
        }

        RaycastHit2D leftHit = Physics2D.Raycast(playerCollider.bounds.min, Vector2.down, 0.3f, groundLayerMask);
        RaycastHit2D rightHit = Physics2D.Raycast(new Vector2(playerCollider.bounds.max.x, playerCollider.bounds.min.y), Vector2.down, 0.3f, groundLayerMask);

        return leftHit || rightHit;
    }
}