using UnityEngine;

public class killing : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public Collider2D collider2D;
    ParticleSystem particleSystem;

    [SerializeField] float damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        spriteRenderer.enabled = false;
        rb.simulated = false;
        collider2D.enabled = false;
        var emission = particleSystem.emission;
        emission.enabled = false;
        if (collision.gameObject.TryGetComponent<health>(out health enemycompoment))
        {
            enemycompoment.TakeDamage(damage);

        }
        Object.Destroy(gameObject, 1.5f);

        //I'm killing you. I'm killing you.
        //I don't care about anything else, I don't give a shit about anything else, I- My programming is just "GET THAT FUCKING GUY RIGHT NOW".
        //It doesn't- There's no, like, "Oh, he's running? Oh, back off a little!", it's just THUMP THUMP THUMP until I get you. 
    }

}
