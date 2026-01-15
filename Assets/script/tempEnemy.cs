using Unity.VisualScripting;
using UnityEngine;

public class tempEnemy : MonoBehaviour
{
    public bool isControlled = false; //Varje objekt som spelaren kan kontrollera har denna bool
                                      //Om den är true så kontolleras objektet
    [SerializeField] float speed = 2.0f;
    [SerializeField] float jumpPower = 2f;

    Collider2D playerCollider;
    Collider2D triggerRange;
    public LayerMask groundLayerMask;

    Camera cam;

    Rigidbody2D rb;

    float moveX;
    bool jump;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.Log($"error: {transform.name} har ej en rigidbody2D");
            return;
        }

        cam = Camera.main;

        triggerRange = GetComponent<CircleCollider2D>();
        if ( triggerRange == null )
        {
            return;
        }

        playerCollider = GetComponent<CapsuleCollider2D>();
        if (playerCollider == null)
        {
            Debug.Log($"error: {transform.name} har ej en collider2D");
            return;
        }
    }

    void Update() //all input hanteras här
    {
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

            if (IsGrounded()) //om spelaren nuddar marken
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    jump = true;
                }
            }

            cam.transform.position = Vector3.Slerp(cam.transform.position, new Vector3(transform.position.x, transform.position.y, -100), 0.05f);
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