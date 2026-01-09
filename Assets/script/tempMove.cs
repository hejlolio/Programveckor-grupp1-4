using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class tempMove : MonoBehaviour
{
    public bool isControlled = true;

    Rigidbody2D rb;
    Collider2D playerCollider;

    [SerializeField] float speed = 2f;
    [SerializeField] GameObject obj;
    [SerializeField] float jumpPower = 0.5f;

    tempEnemy enemy;

    public LayerMask groundLayerMask;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        enemy = obj.GetComponent<tempEnemy>();

        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isControlled)
        {
            if (IsGrounded())
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
            }

            Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, new Vector3(transform.position.x, transform.position.y, -100), 0.05f);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (isControlled)
            {
                isControlled = false;
                enemy.isControlled = true;
            }
            else if (!isControlled)
            {
                isControlled = true;
                enemy.isControlled = false;
            }
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