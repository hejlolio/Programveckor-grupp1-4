using System;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class tempMove : MonoBehaviour
{
    public bool isControlled = true; //Varje objekt som spelaren kan kontrollera har denna bool
                                     //Om den är true så kontolleras objektet, se ln 49
    Rigidbody2D rb;
    Collider2D playerCollider;
    Collider2D triggerCollider;

    [SerializeField] float speed = 2f;
    tempEnemy obj;
    [SerializeField] float jumpPower = 10f;

    tempEnemy enemy; //temporärt innnan ett system för att ta över fiender finns

    Camera cam;

    public LayerMask groundLayerMask;

    float moveX;

    bool jump;

    bool isEnemyNear = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.Log($"error: {transform.name} har ej en rigidbody2D");
            return;
        }

        playerCollider = GetComponent<CapsuleCollider2D>();
        if (playerCollider == null)
        {
            Debug.Log($"error: {transform.name} har ej en collider2D");
            return;
        }

        triggerCollider = GetComponent<CircleCollider2D>();
        if (triggerCollider == null)
        {
            Debug.Log($"{transform.name} har inte en CircleCollider2D");
            return;
        }

        cam = Camera.main;
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

            //flyttar på kameran, Lerp så att kameran inte bara teleporterar
            cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(transform.position.x, transform.position.y, -100), 0.05f);
        }

        if (Input.GetKeyDown(KeyCode.G)) //temporär keybind innan vi kommit på hur man ska ta över fiender
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemyScript = other.GetComponent<tempEnemy>();

        if (enemyScript != null && isEnemyNear == false)
        {
            obj = enemyScript;

            isEnemyNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isEnemyNear = false;
    }

    void FixedUpdate() //allt som involverar fysik hanteras här
    {
        rb.linearVelocityX = moveX * speed;

        if (jump)
        {
            rb.linearVelocityY += jumpPower;
            jump = false;
        }
    }

    bool IsGrounded() //tog detta från någonstans, vet inte hur det funkar lmao
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