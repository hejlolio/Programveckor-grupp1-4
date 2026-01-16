using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Audio;

public class tempMove : MonoBehaviour
{
    public bool isControlled = true; //Varje objekt som spelaren kan kontrollera har denna bool
                                     //Om den är true så kontolleras objektet, se ln 49
    Rigidbody2D rb;
    Collider2D playerCollider;
    Collider2D triggerCollider;

    public List<AudioClip> audioClips;
    [SerializeField] AudioSource audioSource;

    [SerializeField] float speed = 2f;
    public tempEnemy obj;
    [SerializeField] float jumpPower = 10f;

    //public tempEnemy enemy; //temporärt innnan ett system för att ta över fiender finns

    Camera cam;

    public LayerMask groundLayerMask;

    float moveX;

    bool jump;

    public bool isEnemyNear = false;

    bool isWalking = false;

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
            Debug.Log($"error: {transform.name} har ej en Capsulecollider2D");
            return;
        }

        triggerCollider = GetComponent<CircleCollider2D>();
        if (triggerCollider == null)
        {
            Debug.Log($"{transform.name} har inte en CircleCollider2D");
            return;
        }

        cam = Camera.main;

        StartCoroutine(Footstep());
    }

    void Update() //all input hanteras här
    {
        isWalking = false;

        if (isControlled)
        {
            moveX = 0f;

            if (Input.GetKey(KeyCode.D))
            {
                moveX += 1f;

                isWalking = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveX -= 1f;

                isWalking = true;
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

        if (Input.GetKeyDown(KeyCode.G) && Vector3.Distance(transform.position, obj.gameObject.transform.position) < 5)
        {
            if (isControlled)
            {
                isControlled = false;
                obj.isControlled = true;

                var enemyPath = obj.GetComponent<SliderJoint2D>();
                var enemyThrow = obj.GetComponent<AttackWhenSeeing>();

                enemyThrow.enabled = false;
                enemyPath.enabled = false;

                obj.gameObject.tag = "Enemy1";
                obj.gameObject.layer = 9;
            }
            else if (!isControlled)
            {
                isControlled = true;
                obj.isControlled = false;
            }
        }
    }

    /* private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{other} ENTERED TRIGGER");

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
    } */

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

    IEnumerator Footstep()
    {
        while (true)
        {
            if (isWalking && IsGrounded())
            {
                int r = UnityEngine.Random.Range(0, audioClips.Count);
                AudioClip clip = audioClips[r];

                audioSource.PlayOneShot(clip, 1);

                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                yield return null;
            }
        }
    }
}