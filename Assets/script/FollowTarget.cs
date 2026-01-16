using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform target;
    [SerializeField] float speed;
    [SerializeField] Vector2 minMaxTorque;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 force = (target.position - transform.position).normalized;
        rb.AddForce(force * speed);
        rb.AddTorque(Random.Range(minMaxTorque.x, minMaxTorque.y));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
